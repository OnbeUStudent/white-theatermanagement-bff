using Dii_TheaterManagement_Bff.Acceptance.Tests.Steps;
using Microsoft.AspNetCore.Mvc.Testing;
using PactTestingTools;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Dii_TheaterManagement_Bff.Acceptance.Tests.Drivers
{
    public class Driver : IClassFixture<CustomWebApplicationFactory<Startup>>,
        IClassFixture<WebApplicationFactory<Dii_OrderingSvc.Fake.Startup>>
    {


        public HttpClient _client;

        internal async Task AddMoviesToDatabase(Table table)
        {
            // In a real implementation, this is where you'd add your list of movies to the database.

            // For this demo implementation we're just going to use the movies that already exist in the seed data.

            // But to play it safe, we will make sure that the specified movies are in the list of seed data movies.

            // Obtain the list of available movies
            var getMoviesResponse = await _client.GetAsync("/api/movies");
            var moviesAsJson = await getMoviesResponse.Content.ReadAsStringAsync();
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(moviesAsJson);
        }

        public Driver(CustomWebApplicationFactory<Startup> factory
            , WebApplicationFactory<Dii_OrderingSvc.Fake.Startup> orderServiceFakeFactory
            )
        {
            _client = factory.CreateClient();
            var httpClientForInMemoryInstanceOfOrderingSvcApp = orderServiceFakeFactory.CreateClient();
            var inMemoryReverseProxy_OrderingSvc = new InMemoryReverseProxy(httpClientForInMemoryInstanceOfOrderingSvcApp);
            Startup.OrderingHttpClientBaseAddress = inMemoryReverseProxy_OrderingSvc.LocalhostAddress;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "e30.eyJzdWIiOiJBNzFDRTAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJuYW1laWQiOiJib3VsZXZhcmRfYWxpY2UiLCJlbWFpbCI6IkFsaWNlQnJvb2tzQGJvdWxldmFyZC50aGUiLCJ1bmlxdWVfbmFtZSI6IkFsaWNlIEJyb29rcyIsIngtdXNlcnR5cGUiOiJyZWFsIiwieC10aGVhdGVyY29kZSI6ImJvdWxldmFyZCIsIm5iZiI6MTYzMTg0ODQ0MSwiZXhwIjozMzE2Nzg0ODQ0MSwiaWF0IjoxNjMxODQ4NDQxLCJpc3MiOiJNeUJhY2tlbmQiLCJhdWQiOiJNeUJhY2tlbmQifQ.");

        }

    }
}
