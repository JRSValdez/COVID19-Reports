using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID19_API.DTOs.COVID19_API
{

    public class TopTenDto
    {
        public string date { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public int recovered_diff { get; set; }
        public string last_update { get; set; }
        public int active { get; set; }
        public int active_diff { get; set; }
        public double fatality_rate { get; set; }
        public Region region { get; set; }
    }

    public class Region
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }

        public IList<City> cities { get; set; }
    }

    public class City
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

    /*
     "name":"Autauga",
    "date":"2021-05-24",
    "fips":1001,
    "lat":"32.53952745",
    "long":"-86.64408227",
    "confirmed":7118,
    "deaths":110,
    "confirmed_diff":0,
    "deaths_diff":0,
    "last_update":"2021-05-25 04:21:08"
     */
}