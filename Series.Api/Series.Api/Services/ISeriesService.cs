using Series.Api.Dtos;

namespace Series.Api.Services
{
    public interface ISeriesService
    {
        SeriesTreeResponseDto GetTree();
    }
}
