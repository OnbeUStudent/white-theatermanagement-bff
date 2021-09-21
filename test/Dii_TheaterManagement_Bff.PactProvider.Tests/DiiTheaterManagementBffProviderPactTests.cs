using Microsoft.AspNetCore.Mvc.Testing;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactTestingTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Dii_TheaterManagement_Bff.PactProvider;

namespace Dii_TheaterManagement_Bff.PactProvider.Tests
{
    public class DiiTheaterManagementBffProviderPactTests
         : BaseProviderPactTests
    {
    
        private const string providerId = "white-theatermanagement-bff";
        public DiiTheaterManagementBffProviderPactTests(ITestOutputHelper testOutputHelper,
            CustomWebApplicationFactory<TestStartup> factory
            , WebApplicationFactory<Providers.Fake.Startup> providerFakeFactory):base(testOutputHelper, factory, providerFakeFactory)
        {
           
        }

        [Fact(Skip = "To do Later")]
        public void HonorPactWithMvc()
        {

            // Arrange
            string consumerId = "white-theatermanagement-web";

            //Act / Assert
            var httpClientForInMemoryInstanceOfApp = _factory.CreateClient();
            using (var inMemoryReverseProxy = new InMemoryReverseProxy(httpClientForInMemoryInstanceOfApp))
            {
                string providerBase = inMemoryReverseProxy.LocalhostAddress;

                IPactVerifier pactVerifier = new PactVerifier(pactVerifierConfig);
                pactVerifier.ProviderState($"{providerBase}/provider-states")
                    .ServiceProvider(providerId, providerBase)
                    .HonoursPactWith(consumerId)
                    //.PactUri(absolutePathToPactFile)
                    .PactBroker(
                        "https://onbe.pactflow.io",
                        consumerVersionSelectors: new List<VersionTagSelector>
                        {
                            new VersionTagSelector("main", latest: true),
                            new VersionTagSelector("production", latest: true)
                        })
                    .Verify();
            }
        }

        [Fact]
        public void HonorPactWithSpa()
        {
            string consumerId = "white-theatermanagement-spa";


            var httpClientForInMemoryInstanceOfApp = _factory.CreateClient();
            

            using (var inMemoryReverseProxy = new InMemoryReverseProxy(httpClientForInMemoryInstanceOfApp))
            {
                string providerBase = inMemoryReverseProxy.LocalhostAddress;
            
                IPactVerifier pactVerifier = new PactVerifier(pactVerifierConfig);
                pactVerifier//.ProviderState($"{providerBase}/provider-states")
                    .ServiceProvider(providerId, providerBase)
                    .HonoursPactWith(consumerId)
                    .PactUri($"https://onbe.pactflow.io/pacts/provider/{providerId}/consumer/{consumerId}/latest", new PactUriOptions(Environment.GetEnvironmentVariable("PACT_BROKER_TOKEN")))
                    .Verify();
            }
        }
    }
}
