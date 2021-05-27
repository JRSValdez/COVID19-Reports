using COVID19_API.Controllers;
using COVID19_API.DTOs.COVID19_API.TopTen;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace COVID19_API.Tests
{ 
    [TestClass]
    public class TopTenTests
    {

    private ICOVID19_APIService _COVID19_APIService;

    public TopTenTests()
    {
        this._COVID19_APIService = new COVID19_APIService();
    }

        [TestMethod]
        public async Task TestTopTenCountAsync()
        {
            var controller = new TopTenController(_COVID19_APIService);
            int toptenCount = 10;
            var response = await controller.Get();

            Assert.IsNotNull(response);
            Assert.AreEqual(toptenCount, response.Data.Count);
        }

        [TestMethod]
        public async Task TestTopTenTypeAsync()
        {
            var controller = new TopTenController(_COVID19_APIService);
            var response = await controller.Get();
            Assert.IsInstanceOfType(response, typeof(BaseResponse<TopTenDto>));
        }
    }
}
