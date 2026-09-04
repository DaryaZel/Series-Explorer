namespace Series.Api.Models
{
    public class ObservationRecord
    {
        public int SeriesId { get; set; }

        public string Period { get; set; }

        public double? Sales { get; set; }

        public double? Demand { get; set; }

        public double? Supply { get; set; }
    }
}
