using COVID19_API.DTOs.COVID19_API.Provinces;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;

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
            var response = new BaseResponse<ProvincesTopTen>();
            response.Data = GetProvincesTopTen(data.Data);
            return response;
        }

        [HttpGet]
        [Route("api/Provinces/CsvReport")]
        public async Task<HttpResponseMessage> CsvReport(string regionISO)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            var data = await _COVID19_APIService.GetProvincesByRegion(regionISO);

            var regions = GetProvincesTopTen(data.Data);

            writer.WriteLine("Provinces,Cases,Deaths");


            foreach (var region in regions)
            {
                writer.WriteLine($"{region.region.name},{region.confirmed}, {region.deaths}");
            }    

            writer.Flush();
            stream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };
            return result;
        }


        [HttpGet]
        [Route("api/Provinces/XmlReport")]
        public async Task<HttpResponseMessage> XmlReport(string regionISO)
        {
           

            var data = await _COVID19_APIService.GetProvincesByRegion(regionISO);

            var regions = GetProvincesTopTen(data.Data);

            var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("ProvincesReport",
            from reg in regions
            select new XElement("Province",
                                new XElement("Name", reg.region.name),
                                new XElement("Cases", reg.confirmed),
                                new XElement("Deaths", reg.deaths)
                            )));

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            xmlDoc.Save(writer);
            writer.Flush();
            stream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.xml" };
            return result;
        }

        [HttpGet]
        [Route("api/Provinces/JsonReport")]
        public async Task<HttpResponseMessage> JsonReport(string regionISO)
        {


            var data = await _COVID19_APIService.GetProvincesByRegion(regionISO);

            var regions = GetProvincesTopTen(data.Data);

            var Json = JsonConvert.SerializeObject(regions);

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(Json.ToString());

            writer.Flush();
            stream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.json" };
            return result;
        }

        private List<ProvincesTopTen> GetProvincesTopTen(List<ProvincesDto> data)
        {
            //grouping data by province
            List<ProvincesTopTen> provincesWithTotals = new List<ProvincesTopTen>();

            foreach (var region in data)
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
                newProvince.region = new ProvinceNameDto();
                newProvince.region.name = provinceName;
                newProvince.confirmed = totalConfirmed;
                newProvince.deaths = totalDeaths;

                //adding the final provice
                provincesWithTotals.Add(newProvince);
            }

            //ordering the new list
            provincesWithTotals.Sort((x, y) => y.confirmed.CompareTo(x.confirmed));

            return provincesWithTotals.Take(10).ToList(); ;
        }
    }
}
