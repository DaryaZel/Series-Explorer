using System.Collections.Generic;
using System.Web.Http;
using Series.Api.Dtos;

namespace Series.Api.Controllers
{
    [RoutePrefix("")]
    public class HealthController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetRoot()
        {
            return Ok(CreateResponse());
        }

        [HttpGet]
        [Route("api/health")]
        public IHttpActionResult GetHealth()
        {
            return Ok(CreateResponse());
        }

        private static ApiInfoResponseDto CreateResponse()
        {
            return new ApiInfoResponseDto
            {
                Name = "Series API",
                Status = "ok",
                Endpoints = new List<string>
                {
                    "/api/series/tree",
                    "/api/observations?seriesId={seriesId}",
                    "/api/observations?seriesId={seriesId}&year={year}"
                }
            };
        }
    }
}
