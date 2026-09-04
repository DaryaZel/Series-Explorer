using System.Collections.Generic;

namespace Series.Api.Dtos
{
    public class SeriesTreeResponseDto
    {
        public SeriesTreeResponseDto()
        {
            Nodes = new List<SeriesTreeNodeDto>();
        }

        public IReadOnlyList<SeriesTreeNodeDto> Nodes { get; set; }
    }
}
