using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID19_API.DTOs.COVID19_API
{
    public class CityDto
    {
        public string date { get; set; }
        public string name { get; set; }
        public int? fips { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public string last_update { get; set; }
    }
}