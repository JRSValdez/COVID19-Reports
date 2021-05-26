using COVID19_API.Services.COVID19_API;
using System.Web.Http;

public class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        config.DependencyResolver = new NinjectResolver();

        var json = config.Formatters.JsonFormatter;
        json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
        config.Formatters.Remove(config.Formatters.XmlFormatter);

        config.MapHttpAttributeRoutes();

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
    }
}