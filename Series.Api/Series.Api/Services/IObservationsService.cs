using Series.Api.Dtos;

namespace Series.Api.Services
{
    public interface IObservationsService
    {
        ObservationsResponseDto GetObservations(int seriesId, int? year);
    }
}
