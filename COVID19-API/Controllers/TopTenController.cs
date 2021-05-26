using COVID19_API.DTOs.COVID19_API;
using COVID19_API.Models;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace COVID19_API.Controllers
{
    public class TopTenController : ApiController
    {
        private readonly ICOVID19_APIService _COVID19_APIService;

        public TopTenController(ICOVID19_APIService COVID19_APIService)
        {
            this._COVID19_APIService = COVID19_APIService;
        }

        // GET: TopTen
        public async Task<BaseResponse<TopTenDto>> Get()
        {
            var data = await _COVID19_APIService.GetTopTen();
            return data;
        }
    }
}