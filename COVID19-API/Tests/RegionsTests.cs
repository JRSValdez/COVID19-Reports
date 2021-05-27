using COVID19_API.Controllers;
using COVID19_API.DTOs.COVID19_API.Regions;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace COVID19_API.Tests
{ 
    [TestClass]
    public class RegionsTests
    {

    private ICOVID19_APIService _COVID19_APIService;

    public RegionsTests()
    {
        this._COVID19_APIService = new COVID19_APIService();
    }

        [TestMethod]
        public async Task TestRegionsCountAsync()
        {
            var controller = new RegionsController(_COVID19_APIService);
            int regionsCount = 214;
            var response = await controller.Get();

            Assert.IsNotNull(response);
            Assert.AreEqual(regionsCount, response.Data.Count);
        }

        [TestMethod]
        public async Task TestRegionsTypeAsync()
        {
            var controller = new RegionsController(_COVID19_APIService);
            var response = await controller.Get();
            Assert.IsInstanceOfType(response, typeof(BaseResponse<RegionDto>));
        }
    }
}
