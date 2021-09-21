using Dii_TheaterManagement_Bff.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dii_TheaterManagement_Bff.PactProvider.Tests
{
    public class TestStartup: Startup
    {
        public static HttpClient OrderingSvcHttpClient;
        public static HttpClient MovieCatalogSvcSvcHttpClient;
        public TestStartup(IConfiguration configuration): base(configuration)
        {

        }

        public override void AddApplicationServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(OrderingSvcClient), serviceProvider =>
            {
                OrderingSvcHttpClient.DefaultRequestHeaders.Add("dapr-app-id", "diiorderingsvc");
                return new OrderingSvcClient(OrderingSvcHttpClient);
            });
            services.AddSingleton(typeof(MovieCatalogSvcClient), serviceProvider =>
            {
                MovieCatalogSvcSvcHttpClient.DefaultRequestHeaders.Add("dapr-app-id", "diimoviecatalogsvc");
                return new MovieCatalogSvcClient(MovieCatalogSvcSvcHttpClient);
            });
        }
    }
}
