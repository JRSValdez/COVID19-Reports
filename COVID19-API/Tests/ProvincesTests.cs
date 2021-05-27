using COVID19_API.Controllers;
using COVID19_API.DTOs.COVID19_API.Provinces;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace COVID19_API.Tests
{ 
    [TestClass]
    public class ProvincesTests
    {

    private ICOVID19_APIService _COVID19_APIService;

    public ProvincesTests()
    {
        this._COVID19_APIService = new COVID19_APIService();
    }

        [TestMethod]
        public async Task TestUSARegionsCountAsync()
        {
            var controller = new ProvincesController(_COVID19_APIService);
            int regionsCount = 10;
            var response = await controller.Get("USA");

            Assert.IsNotNull(response);
            Assert.AreEqual(regionsCount, response.Data.Count);
        }

        [TestMethod]
        public async Task TestProvincesTypeAsync()
        {
            var controller = new ProvincesController(_COVID19_APIService);
            var response = await controller.Get("USA");
            Assert.IsInstanceOfType(response, typeof(BaseResponse<ProvincesTopTen>));
        }
    }
}
