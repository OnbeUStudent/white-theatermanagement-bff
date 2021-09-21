using Microsoft.AspNetCore.Mvc.Testing;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactTestingTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dii_TheaterManagement_Bff.PactProvider.Tests
{
    public class BaseProviderPactTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>,
        IClassFixture<WebApplicationFactory<Providers.Fake.Startup>>
    {
        public readonly PactVerifierConfig pactVerifierConfig;
        public readonly ITestOutputHelper _outputHelper;
        public readonly CustomWebApplicationFactory<TestStartup> _factory;
      
        public BaseProviderPactTests(ITestOutputHelper testOutputHelper,
             CustomWebApplicationFactory<TestStartup> factory
             , WebApplicationFactory<Providers.Fake.Startup> providerApplicationFactory)
        {
            _outputHelper = testOutputHelper;
            _factory = factory;
            if(TestStartup.OrderingSvcHttpClient == null)
            TestStartup.OrderingSvcHttpClient = providerApplicationFactory.CreateClient();

            if (TestStartup.MovieCatalogSvcSvcHttpClient == null)
                TestStartup.MovieCatalogSvcSvcHttpClient = providerApplicationFactory.CreateClient();
            // Arrange
            pactVerifierConfig = new PactVerifierConfig
            {
                Outputters = new List<IOutput> { new XUnitOutput(_outputHelper) },
                Verbose = true, // Output verbose verification logs to the test output
                ProviderVersion = Environment.GetEnvironmentVariable("GIT_COMMIT"),
                PublishVerificationResults = "true".Equals(Environment.GetEnvironmentVariable("PACT_PUBLISH_VERIFICATION"))

            };
        }
    }
}
