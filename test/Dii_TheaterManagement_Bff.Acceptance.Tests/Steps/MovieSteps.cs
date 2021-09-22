using Dii_TheaterManagement_Bff.Acceptance.Tests.Drivers;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Dii_TheaterManagement_Bff.Acceptance.Tests.Steps
{
    [Binding]
    public class MovieSteps
    {
        private readonly Driver _driver;

        public MovieSteps(Driver driver)
        {
            _driver = driver;
        }

        [Given(@"the following movies")]
        public async Task GivenTheFollowingMovies(Table table)
        {
            await _driver.AddMoviesToDatabase(table);
        }
        
        [When(@"I view the list of movies")]
        public void WhenIViewTheListOfMovies()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the movie list should show")]
        public void ThenTheMovieListShouldShow(Table table)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
