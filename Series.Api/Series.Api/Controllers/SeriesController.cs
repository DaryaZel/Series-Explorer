using System;
using System.Web.Http;
using Series.Api.Dtos;
using Series.Api.Services;

namespace Series.Api.Controllers
{
    [RoutePrefix("api/series")]
    public class SeriesController : ApiController
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            if (seriesService == null)
            {
                throw new ArgumentNullException("seriesService");
            }

            _seriesService = seriesService;
        }

        [HttpGet]
        [Route("tree")]
        public IHttpActionResult GetTree()
        {
            SeriesTreeResponseDto response = _seriesService.GetTree();

            return Ok(response);
        }
    }
}
