using COVID19_API.DTOs.COVID19_API.TopTen;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

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
            //all cases
            var data = await _COVID19_APIService.GetTopTen();

            //sorting by cases
            data.Data.Sort((x, y) => y.confirmed.CompareTo(x.confirmed));

            //getting only the topten
            data.Data = data.Data.Take(10).ToList();

            return data;
        }
    }
}