using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID19_API.DTOs.COVID19_API
{
    public class RegionDto
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }

        public IList<CityDto> cities { get; set; }
    }
}