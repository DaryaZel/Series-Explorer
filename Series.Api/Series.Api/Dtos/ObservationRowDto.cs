namespace Series.Api.Dtos
{
    public class ObservationRowDto
    {
        public string Id { get; set; }

        public string Period { get; set; }

        public ObservationCellDto Sales { get; set; }

        public ObservationCellDto Demand { get; set; }

        public ObservationCellDto Supply { get; set; }
    }
}
