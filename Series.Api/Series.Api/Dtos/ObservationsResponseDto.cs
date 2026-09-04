using System.Collections.Generic;

namespace Series.Api.Dtos
{
    public class ObservationsResponseDto
    {
        public ObservationsResponseDto()
        {
            AvailableYears = new List<int>();
            Rows = new List<ObservationRowDto>();
        }

        public int SeriesId { get; set; }

        public List<int> AvailableYears { get; set; }

        public int? SelectedYear { get; set; }

        public List<ObservationRowDto> Rows { get; set; }
    }
}
