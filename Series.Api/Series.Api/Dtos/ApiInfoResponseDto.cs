using System.Collections.Generic;

namespace Series.Api.Dtos
{
    public class ApiInfoResponseDto
    {
        public ApiInfoResponseDto()
        {
            Endpoints = new List<string>();
        }

        public string Name { get; set; }

        public string Status { get; set; }

        public List<string> Endpoints { get; set; }
    }
}
