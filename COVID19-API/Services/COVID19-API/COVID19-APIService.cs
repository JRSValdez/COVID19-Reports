using COVID19_API.DTOs.COVID19_API;
using COVID19_API.Options.COVID19_API;
using COVID19_API.Responses.COVID19_API;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace COVID19_API.Services.COVID19_API
{
    public class COVID19_APIService : COVID19_APIServiceBase, ICOVID19_APIService
    {
        public async Task<BaseResponse<TopTenDto>> GetTopTen()
        {
            string endpoint = "/reports";
            var result = await Call<BaseResponse<TopTenDto>>(nameof(HttpMethod.Get), endpoint);
            return result;
        }

        public async Task<BaseResponse<RegionDto>> GetRegions()
        {
            string endpoint = "/regions";
            var result = await Call<BaseResponse<RegionDto>>(nameof(HttpMethod.Get), endpoint);
            return result;
        }
    }
}