using System;
using System.Web.Http;
using Series.Api.Dtos;
using Series.Api.Services;

namespace Series.Api.Controllers
{
    [RoutePrefix("api/observations")]
    public class ObservationsController : ApiController
    {
        private readonly IObservationsService _observationsService;

        public ObservationsController(IObservationsService observationsService)
        {
            if (observationsService == null)
            {
                throw new ArgumentNullException("observationsService");
            }

            _observationsService = observationsService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int? seriesId = null, int? year = null)
        {
            if (!seriesId.HasValue || seriesId.Value <= 0)
            {
                return BadRequest("A valid seriesId query parameter is required.");
            }

            if (year.HasValue && (year.Value < 1000 || year.Value > 9999))
            {
                return BadRequest("Year must be a four-digit value.");
            }

            ObservationsResponseDto response = _observationsService.GetObservations(seriesId.Value, year);

            return Ok(response);
        }
    }
}
