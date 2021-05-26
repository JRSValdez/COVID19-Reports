using COVID19_API.DTOs.COVID19_API.Provinces;
using COVID19_API.DTOs.COVID19_API.TopTen;
using COVID19_API.Responses.COVID19_API;
using System.Threading.Tasks;
using RegionDto = COVID19_API.DTOs.COVID19_API.Regions.RegionDto;

namespace COVID19_API.Services.COVID19_API
{
    public interface ICOVID19_APIService
    {
        Task<BaseResponse<TopTenDto>> GetTopTen();
        Task<BaseResponse<RegionDto>> GetRegions();
        Task<BaseResponse<ProvincesDto>> GetProvincesByRegion(string regionISO);

    }
}