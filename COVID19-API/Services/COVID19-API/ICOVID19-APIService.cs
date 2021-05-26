using COVID19_API.DTOs.COVID19_API;
using COVID19_API.Responses.COVID19_API;
using System.Threading.Tasks;

namespace COVID19_API.Services.COVID19_API
{
    public interface ICOVID19_APIService
    {
        Task<BaseResponse<TopTenDto>> GetTopTen();

    }
}