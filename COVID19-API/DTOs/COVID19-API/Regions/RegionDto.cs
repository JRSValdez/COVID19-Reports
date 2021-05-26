
using System.Collections.Generic;

namespace COVID19_API.DTOs.COVID19_API.Regions
{
    public class RegionDto
    {
        public string iso { get; set; }
        public string name { get; set; }
        public IList<CityDto> cities { get; set; }
    }
}