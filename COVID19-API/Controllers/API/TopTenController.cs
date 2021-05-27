using COVID19_API.DTOs.COVID19_API.TopTen;
using COVID19_API.Responses.COVID19_API;
using COVID19_API.Services.COVID19_API;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;

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

            var response = new BaseResponse<TopTenDto>();

            response.Data = GetSortedTopTen(data.Data);

            return response;
        }


        [HttpGet]
        [Route("api/TopTen/CsvReport")]
        public async Task<HttpResponseMessage> CsvReport()
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            var data = await _COVID19_APIService.GetTopTen();

            var regions = GetSortedTopTen(data.Data);

            writer.WriteLine("Region,Cases,Deaths");


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
        [Route("api/TopTen/XmlReport")]
        public async Task<HttpResponseMessage> XmlReport()
        {

            var data = await _COVID19_APIService.GetTopTen();

            var regions = GetSortedTopTen(data.Data);

            var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Top10COVID19Cases",
            from reg in regions
            select new XElement("Region",
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
        [Route("api/TopTen/JsonReport")]
        public async Task<HttpResponseMessage> JsonReport()
        {
            var data = await _COVID19_APIService.GetTopTen();

            var regions = GetSortedTopTen(data.Data);

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


        private List<TopTenDto> GetSortedTopTen(List<TopTenDto> data)
        {
            //sorting by cases
            data.Sort((x, y) => y.confirmed.CompareTo(x.confirmed));

            //getting only the topten
            return data.Take(10).ToList();
        }
    }
}