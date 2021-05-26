using COVID19_API.Exceptions;
using COVID19_API.Options.COVID19_API;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace COVID19_API.Services.COVID19_API
{
    public class COVID19_APIServiceBase
    {

        protected async Task<T> Call<T>(string method, string endpoint, object body = null) where T : new()
        {
            var ret = new T();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseURL"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("x-rapidapi-key", ConfigurationManager.AppSettings["apiKey"].ToString());
                    client.DefaultRequestHeaders.Add("x-rapidapi-host", ConfigurationManager.AppSettings["apiHost"].ToString());
                    HttpResponseMessage response = new HttpResponseMessage();

                    switch (method)
                    {
                        case nameof(HttpMethod.Get):
                        default:
                            response = await client.GetAsync(endpoint).ConfigureAwait(false);
                            break;
                    }

                    //TODO: Raise exeptions
                    string result = response.Content.ReadAsStringAsync().Result;
                    ret = JsonConvert.DeserializeObject<T>(result);

                }
            }
            catch (Exception e)
            {
                new NotFoundException(endpoint,e.Message);
            }
            return ret;
        }

    }
}