using COVID19_API.DTOs.COVID19_API.Provinces;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace COVID19_API.Controllers
{
    public class ProvincesController : ApiController
    {
        private readonly ICOVID19_APIService _COVID19_APIService;

        public ProvincesController(ICOVID19_APIService COVID19_APIService)
        {
            this._COVID19_APIService = COVID19_APIService;
        }

        public async Task<BaseResponse<ProvincesTopTen>> Get(string regionISO)
        {
            var data = await _COVID19_APIService.GetProvincesByRegion(regionISO);

            //grouping data by province
            List<ProvincesTopTen> provincesWithTotals = new List<ProvincesTopTen>();

            foreach(var region in data.Data)
            {
                string provinceName = region.region.province;
                int totalConfirmed = 0;
                int totalDeaths = 0;

                //summing the city data
                foreach (var city in region.region.cities)
                {
                    totalConfirmed += city.confirmed;
                    totalDeaths += city.deaths;
                }

                ProvincesTopTen newProvince = new ProvincesTopTen();
                newProvince.province = provinceName;
                newProvince.confirmed = totalConfirmed;
                newProvince.deaths = totalDeaths;

                //adding the final provice
                provincesWithTotals.Add(newProvince);
            }

            //ordering the new list
            provincesWithTotals.Sort((x, y) => y.confirmed.CompareTo(x.confirmed));

            //setting the response
            var response = new BaseResponse<ProvincesTopTen>();
            response.Data = provincesWithTotals.Take(10).ToList(); ;

            return response;
        }
    }
}
