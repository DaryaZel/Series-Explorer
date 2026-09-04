using System.Collections.Generic;

namespace Series.Api.Dtos
{
    public class SeriesTreeNodeDto
    {
        public SeriesTreeNodeDto()
        {
            Children = new List<SeriesTreeNodeDto>();
        }

        public int Id { get; set; }

        public string Label { get; set; }

        public List<SeriesTreeNodeDto> Children { get; set; }
    }
}
