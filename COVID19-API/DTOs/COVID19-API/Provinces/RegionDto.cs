
using System.Collections.Generic;

namespace COVID19_API.DTOs.COVID19_API.Provinces
{
    public class RegionDto
    {
        public string name { get; set; }
        public string iso { get; set; }
        public string province { get; set; }

        public List<CityDto> cities { get; set; }
    }
}