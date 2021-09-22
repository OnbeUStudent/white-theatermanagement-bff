using Dii_OrderingSvc.Fake.Data;
using Dii_TheaterManagement_Bff.Acceptance.Tests.Drivers;
using Dii_TheaterManagement_Bff.Acceptance.Tests.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Dii_TheaterManagement_Bff.Acceptance.Tests.Steps
{
    [Binding]
    public class ViewBookingsSteps
    {
        protected HttpClient _client;
        List<string> names = new List<string>();
        IEnumerable<CreateCurrentBookingsView> createCurrentBookingsView;
        private IEnumerable<Booking> deserialized;

        public ViewBookingsSteps(Driver driver)
        {
            _client = driver._client;
        }

        //Arange
        [Given(@"list of CurrentBookingsView")]
        public async void GivenListOfCurrentBookingsView(Table table)
        {
            // Obtain the list of available movies
            var getMoviesResponse = await _client.GetAsync("/api/movies");
            var moviesAsJson = await getMoviesResponse.Content.ReadAsStringAsync();
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(moviesAsJson);

            createCurrentBookingsView = table.CreateSet<CreateCurrentBookingsView>();
            foreach (CreateCurrentBookingsView descriptor in createCurrentBookingsView)
            {
                Movie movie = movies.SingleOrDefault(m => m.Title == descriptor.title);
                movie.Should().NotBeNull($"because there should be a movie with title {descriptor.title}");

                // Ensure that the booking exists
                var booking = new Booking()
                {
                    MonthId = descriptor.date,
                    MovieId = movie.MovieId
                };
                var bodyForPutBooking = JsonConvert.SerializeObject(booking);
                await _client.PutAsync($"api/bookings/{descriptor.date}", new System.Net.Http.StringContent(bodyForPutBooking, Encoding.UTF8, "application/json"));
                names.Add(descriptor.title);
            }
        }

        //Act
        [When(@"I view bookings as an admin user on the admin page")]
        public async void WhenIViewBookingsAsAnAdminUserOnTheAdminPage()
        {
            // Act
            var response = await _client.GetAsync("/api/bookings");

            // Get the response
            var bookingsJsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Console.WriteLine("Your response data is: " + bookingsJsonString);

            // Deserialise the data (include the Newtonsoft JSON Nuget package if you don't already have it)
            deserialized = JsonConvert.DeserializeObject<IEnumerable<Booking>>(bookingsJsonString);

        }

        //Assert
        [Then(@"I am able to see all CurrentBookings")]
        public void ThenIAmAbleToSeeAllCurrentBookings()
        {
            foreach (Booking i in deserialized)
            {
                i.Movie.Title.Should().BeOneOf(names);
            }
            // Assert.True(true);

        }

        //[Then(@"I cannot see ViewBooking link")]
        //public void ThenICannotSeeViewBookingLink()
        //{
        //    ScenarioContext.Current.Pending();
        //}
    }
}