using COVID19_API.DTOs.COVID19_API.Regions;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using System.Threading.Tasks;
using System.Web.Http;

namespace COVID19_API.Controllers
{
    public class RegionsController : ApiController
    {
        private readonly ICOVID19_APIService _COVID19_APIService;

        public RegionsController(ICOVID19_APIService COVID19_APIService)
        {
            this._COVID19_APIService = COVID19_APIService;
        }


        // GET: Regions
        public async Task<BaseResponse<RegionDto>> Get()
        {
            var data = await _COVID19_APIService.GetRegions();
            return data;
        }


    }
}
