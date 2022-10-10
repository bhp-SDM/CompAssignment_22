using MovieRatings.BE;
using MovieRatings.Interfaces;
using System;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingTest
    {
        [Theory]
        [InlineData(1,2,1,"2018-01-01")]
        [InlineData(1,2,3,"2018-01-01")]
        [InlineData(1,2,5,"2018-01-01")]
        public void CreateValidMovieRating(int reviewerID, int movieID, int grade, string dateStr)
        {
            DateTime.TryParse(dateStr, out DateTime date);
            IMovieRating rating = new MovieRating(reviewerID, movieID, grade, date);

            Assert.Equal(reviewerID, rating.ReviewerID);
            Assert.Equal(movieID, rating.MovieID);
            Assert.Equal(grade, rating.Grade);
            Assert.Equal(date, rating.Date);
        }

        [Theory]
        [InlineData(1,2,0, "2018-01-01")]   // invalid rating: 0
        [InlineData(1,2,6,"2018-01-01")]    // invalid rating: 6
        [InlineData(1,2,6,null)]            // invalid date: null
        public void CreateInvalidMovieRatingExpectArgumentException(int reviewerID, int movieID, int grade, string dateStr)
        {
            DateTime.TryParse(dateStr, out DateTime date);
            Assert.Throws<ArgumentException>(() => {IMovieRating rating = new MovieRating(reviewerID, movieID, grade, date); });
        }

        [Fact]
        public void ToStringReturnsValidString()
        {
            int reviewerID = 1;
            int movieID = 2;
            int grade = 3;
            DateTime date = new DateTime(2018,1, 1);
            string expected = string.Format("Reviewer: {0}, Movie: {1}, Grade: {2}, Date: {3}", reviewerID, movieID, grade, date);
            
            IMovieRating mr = new MovieRating(reviewerID, movieID, grade, date);
            string result = mr.ToString();
            Assert.Equal(expected, result);
        }
    }
}
