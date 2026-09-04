using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Series.Api.Data;
using Series.Api.Dtos;

namespace Series.Api.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly SeriesDbContext _context;

        public SeriesService(SeriesDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public SeriesTreeResponseDto GetTree()
        {
            var series = _context.Series
                .AsNoTracking()
                .OrderBy(item => item.Description)
                .ToList();

            var hierarchy = _context.Hierarchy
                .AsNoTracking()
                .OrderBy(item => item.ParentSeriesId)
                .ThenBy(item => item.SeriesId)
                .ToList();

            var nodesById = series.ToDictionary(
                item => item.Id,
                item => new SeriesTreeNodeDto
                {
                    Id = item.Id,
                    Label = item.Description
                });

            var roots = new List<SeriesTreeNodeDto>();

            foreach (var relation in hierarchy)
            {
                SeriesTreeNodeDto childNode;
                if (!nodesById.TryGetValue(relation.SeriesId, out childNode))
                {
                    continue;
                }

                if (relation.ParentSeriesId == 0)
                {
                    roots.Add(childNode);
                    continue;
                }

                SeriesTreeNodeDto parentNode;
                if (nodesById.TryGetValue(relation.ParentSeriesId, out parentNode))
                {
                    parentNode.Children.Add(childNode);
                }
                else
                {
                    roots.Add(childNode);
                }
            }

            return new SeriesTreeResponseDto
            {
                Nodes = SortNodesAndBuildLabels(roots, null)
            };
        }

        private static List<SeriesTreeNodeDto> SortNodesAndBuildLabels(
            IEnumerable<SeriesTreeNodeDto> nodes,
            string parentLabel)
        {
            return nodes
                .OrderBy(node => node.Label)
                .Select(node =>
                {
                    node.Label = BuildDisplayLabel(parentLabel, node.Label);
                    node.Children = SortNodesAndBuildLabels(node.Children, node.Label);
                    return node;
                })
                .ToList();
        }

        private static string BuildDisplayLabel(string parentLabel, string description)
        {
            string segment = FormatDescriptionSegment(description);

            if (string.IsNullOrEmpty(parentLabel))
            {
                return segment;
            }

            return parentLabel + " " + segment;
        }

        private static string FormatDescriptionSegment(string description)
        {
            string value = (description ?? string.Empty).Trim();
            int lastOpenBracketIndex = value.LastIndexOf('[');
            int lastCloseBracketIndex = value.LastIndexOf(']');

            if (lastOpenBracketIndex >= 0 && lastCloseBracketIndex > lastOpenBracketIndex)
            {
                value = value.Substring(
                    lastOpenBracketIndex + 1,
                    lastCloseBracketIndex - lastOpenBracketIndex - 1).Trim();
            }

            return "[" + value + "]";
        }
    }
}
