using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;

namespace Series.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            NinjectConfig.Register(config);
            DatabaseConfig.Register();
            ConfigureCors(config);
            ConfigureFormatters(config);

            config.MapHttpAttributeRoutes();
        }

        private static void ConfigureCors(HttpConfiguration config)
        {
            var allowedOrigins = ConfigurationManager.AppSettings["CorsAllowedOrigins"];

            if (string.IsNullOrWhiteSpace(allowedOrigins))
            {
                allowedOrigins = "http://localhost:5173";
            }

            var cors = new EnableCorsAttribute(
                origins: allowedOrigins,
                headers: "*",
                methods: "GET,OPTIONS");

            config.EnableCors(cors);
        }

        private static void ConfigureFormatters(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }
    }
}
