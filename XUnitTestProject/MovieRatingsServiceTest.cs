using Moq;
using MovieRatings.BE;
using MovieRatings.BLL;
using MovieRatings.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingsServiceTest
    {
        IMovieRating mr113 = new MovieRating(1, 1, 3, new DateTime(2018, 1, 3));
        IMovieRating mr125 = new MovieRating(1, 2, 5, new DateTime(2018, 1, 2));
        IMovieRating mr133 = new MovieRating(1, 3, 3, new DateTime(2018, 1, 1));
        IMovieRating mr214 = new MovieRating(2, 1, 4, new DateTime(2018, 1, 3));
        IMovieRating mr225 = new MovieRating(2, 2, 5, new DateTime(2018, 1, 4));
        IMovieRating mr335 = new MovieRating(3, 3, 5, new DateTime(2018, 1, 6));
        IMovieRating mr435 = new MovieRating(4, 3, 5, new DateTime(2018, 1, 5));

        Dictionary<int, List<IMovieRating>> reviewers = new Dictionary<int, List<IMovieRating>>();
        Dictionary<int, List<IMovieRating>> movies = new Dictionary<int, List<IMovieRating>>();
        Dictionary<int, List<IMovieRating>> grades = new Dictionary<int, List<IMovieRating>>();

        Mock<IMovieRatingsRepository> movieRatingsRepositoryMock = new Mock<IMovieRatingsRepository>();

        public MovieRatingsServiceTest()
        {
            reviewers.Add(1, new List<IMovieRating>() { mr113, mr125, mr133 });
            reviewers.Add(2, new List<IMovieRating>() { mr214, mr225 });
            reviewers.Add(3, new List<IMovieRating>() { mr335 });
            reviewers.Add(4, new List<IMovieRating>() { mr435 });

            movies.Add(1, new List<IMovieRating>() { mr113, mr214 });
            movies.Add(2, new List<IMovieRating>() { mr125, mr225 });
            movies.Add(3, new List<IMovieRating>() { mr133, mr335, mr435 });

            grades.Add(3, new List<IMovieRating>() { mr113, mr133 });
            grades.Add(4, new List<IMovieRating>() { mr214 });
            grades.Add(5, new List<IMovieRating>() { mr125, mr225, mr335, mr435 });

            movieRatingsRepositoryMock.SetupGet((x) => x.Reviewers).Returns(() => reviewers);
            movieRatingsRepositoryMock.SetupGet((x) => x.Movies).Returns(() => movies);
            movieRatingsRepositoryMock.SetupGet((x) => x.Grades).Returns(() => grades);
        }

        [Fact]
        public void CreateMovieRatingsServiceValidRepository()
        {
            IMovieRatingsService service = null;
            service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            Assert.NotNull(service);
        }

        [Fact]
        public void CreateMovieRatingsServiceRepositoryIsNullExpectArgumentException()
        {
            IMovieRatingsService service = null;
            var ex = Assert.Throws<ArgumentException>(() => service = new MovieRatingsService(null));
            Assert.Equal("Missing MovieRatingsRepository", ex.Message);
        }

        // Requirement 1
        [Theory]
        [InlineData(1, 3)]
        [InlineData(5, 0)]
        public void GetReviewerNumberOfReviews(int reviewerID, int expected)
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int result = service.GetReviewerNumberOfReviews(reviewerID);
            Assert.Equal(expected, result);
        }

        // Requirement 2
        [Fact]
        public void GetReviewerAverageRatingValidReviewerID()
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int reviewerID = 2;
            double expected = 4.5;
            double result = service.GetReviewerAgerageRating(reviewerID);
            Assert.Equal(expected, result);
        }

        // Requirement 2
        [Fact]
        public void GetreviewerAverageRatingInvalidReviewerIDExpectArgumentException()
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int reviewerID = 5;
            var ex = Assert.Throws<ArgumentException>(() => { double result = service.GetReviewerAgerageRating(reviewerID); });
            Assert.Equal("ReviewerID does not exist", ex.Message);
        }

        // Requirement 3
        [Theory]
        [InlineData(1, 3, 2)]
        [InlineData(1, 4, 0)]
        [InlineData(5, 3, 0)]
        public void GetReviewerNumberOfRating(int reviewerID, int rating, int expected)
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int result = service.GetReviewerNumberOfRating(reviewerID, rating);
            Assert.Equal(expected, result);
        }

        // Requirement 4
        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 0)]
        public void GetMovieNumberOfReviews(int movieID, int expected)
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int result = service.GetMovieNumberOfReviews(movieID);
            Assert.Equal(expected, result);
        }

        // Requirement 5
        [Fact]
        public void GetMovieAverageRatingValidMovieID()
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int movieID = 1;
            double expected = 3.5;
            double result = service.GetMovieAgerageRating(movieID);
            Assert.Equal(expected, result);
        }

        // Requirement 5
        [Fact]
        public void GetMovieAverageRatingInvalidMovieIDExpectArgumentException()
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int movieID = 4;
            var ex = Assert.Throws<ArgumentException>(() => { double result = service.GetMovieAgerageRating(movieID); });
            Assert.Equal("MovieID does not exist", ex.Message);
        }

        // Requirement 6
        [Theory]
        [InlineData(2, 5, 2)]
        [InlineData(2, 4, 0)]
        [InlineData(4, 3, 0)]
        public void GetMovieNumberOfRating(int movieID, int rating, int expected)
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int result = service.GetMovieNumberOfRating(movieID, rating);
            Assert.Equal(expected, result);
        }

        // Requirement 7
        [Fact]
        public void GetTopRatedMovies()
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int[] result = service.GetTopRatedMovies();
            Assert.True(result.Length == 2);
            Assert.Equal(2, result[0]);
            Assert.Equal(3, result[1]);
        }

        // Requirement 8
        [Fact]
        public void GetReviewersMostReviews()
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int[] result = service.GetReviewersMostReviews();
            Assert.True(result.Length == 1);
            Assert.Equal(1, result[0]);
        }

        // Requirement 9
        [Fact]
        public void GetTopNMovies()
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int N = 2;
            int[] result = service.GetTopNMovies(N);
            Assert.True(result.Length == 2);
            Assert.Equal(2, result[0]);
            Assert.Equal(3, result[1]);
        }

        // Requirement 10
        [Fact]
        public void GetReviewerMovies()
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int reviewerID = 1;
            int[] result = service.GetReviewerMovies(reviewerID);
            Assert.True(result.Length == 3);
            Assert.Equal(2, result[0]);
            Assert.Equal(3, result[1]);
            Assert.Equal(1, result[2]);
        }

        // Requirement 11
        [Theory]
        [InlineData(3, new int[] { 4, 3, 1 })]
        [InlineData(5, new int[0])]                 // movie 5 does not exist
        public void GetMovieReviewers(int movieID, int[] expected)
        {
            IMovieRatingsService service = new MovieRatingsService(movieRatingsRepositoryMock.Object);
            int[] result = service.GetMovieReviewers(movieID);
            Assert.True(result.Length == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], result[i]);
            }
        }
    }
}
