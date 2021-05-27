using COVID19_API.Controllers;
using COVID19_API.Services.COVID19_API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace COVID19_Reports.Tests.Controllers
{
    [TestClass]
    public class TopTenTests
    {
        [TestMethod]
        public async Task TestTopTenAsync()
        {
            var apiService = new COVID19_APIService();
            var controller = new TopTenController(apiService);

            var response = await controller.Get();

        }
    }
}
