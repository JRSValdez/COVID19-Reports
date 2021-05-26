using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID19_API.DTOs.COVID19_API.Provinces
{
    public class ProvincesTopTen
    {
        public string province { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
    }
}