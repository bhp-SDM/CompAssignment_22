using MovieRatings.BLL;
using MovieRatings.Interfaces;
using System.Diagnostics;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingsServicePerformanceTest:IClassFixture<MovieRatingsRepositoryFixture>
    {
        private const double TIME_LIMIT = 1.0;

        private const string FILE_NAME = "../../../ratings.json"; // Use your own filepath
        
        private MovieRatingsRepositoryFixture TestDataFixture;

        public MovieRatingsServicePerformanceTest(MovieRatingsRepositoryFixture testDataFixture)
        {
            TestDataFixture = testDataFixture;
        }
      

        [Fact]
        public void GetReviewerNumberOfReviewsPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);
            int reviewerID = TestDataFixture.ReviewerWithMostReviews;

            Stopwatch sw = Stopwatch.StartNew();
            int result = service.GetReviewerNumberOfReviews(reviewerID);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

        [Fact]
        public void GetReviewerAverageRatingPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);
            int reviewerID = TestDataFixture.ReviewerWithMostReviews;

            Stopwatch sw = Stopwatch.StartNew();
            double result = service.GetReviewerAgerageRating(reviewerID);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

        [Fact]
        public void GetReviewerNumberOfRatingPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);
            int reviewerID = TestDataFixture.ReviewerWithMostReviews;
            int rating = 3;

            Stopwatch sw = Stopwatch.StartNew();
            int result = service.GetReviewerNumberOfRating(reviewerID, rating);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

        [Fact]
        public void GetMovieNumberOfReviewsPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);
            int movieID = TestDataFixture.MovieWithMostReviews;

            Stopwatch sw = Stopwatch.StartNew();
            int result = service.GetMovieNumberOfReviews(movieID);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

        [Fact]
        public void GetMovieAgerageRatingPerformanceTest()
        {
           IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);
            int movieID = TestDataFixture.MovieWithMostReviews;

            Stopwatch sw = Stopwatch.StartNew();
            double result = service.GetMovieAgerageRating(movieID);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

        [Fact]
        public void GetMovieNumberOfRatingPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);
            int reviewerID = TestDataFixture.MovieWithMostReviews;
            int rating = 3;

            Stopwatch sw = Stopwatch.StartNew();
            int result = service.GetMovieNumberOfRating(reviewerID, rating);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

        [Fact]
        public void GetTopRatedMoviesPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);

            Stopwatch sw = Stopwatch.StartNew();
            int[] result = service.GetTopRatedMovies();
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

        [Fact]
        public void GetReviewerMostReviewsPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);

            Stopwatch sw = Stopwatch.StartNew();
            int[] result = service.GetReviewersMostReviews();
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }
        [Fact]
        public void GetTopNMoviesPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);

            Stopwatch sw = Stopwatch.StartNew();
            int[] result = service.GetTopNMovies(10);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

       [Fact]
        public void GetReviewerMoviesPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);
            int reviewerID = TestDataFixture.ReviewerWithMostReviews;

            Stopwatch sw = Stopwatch.StartNew();
            int[] result = service.GetReviewerMovies(reviewerID);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }

       [Fact]
        public void GetMovieReviewersPerformanceTest()
        {
            IMovieRatingsService service = new MovieRatingsService(TestDataFixture.Repository);
            int reviewerID = TestDataFixture.MovieWithMostReviews;

            Stopwatch sw = Stopwatch.StartNew();
            int[] result = service.GetReviewerMovies(reviewerID);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds / 1000d <= TIME_LIMIT);
        }
    }
}
