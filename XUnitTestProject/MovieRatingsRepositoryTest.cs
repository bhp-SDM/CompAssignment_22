using MovieRatings.BE;
using MovieRatings.Interfaces;
using MovieRatings.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingsRepositoryTest
    {
        public static IEnumerable<Object[]> ValidJson =>
            new List<object[]>()
            {
                new object[]
                {
                    "[" +
                    "   {Reviewer:1, Movie:1, Grade:5, Date:'2018-01-01'}," +
                    "   {Reviewer:1, Movie:2, Grade:3, Date:'2018-01-02'}," +
                    "   {Reviewer:2, Movie:1, Grade:3, Date:'2018-01-01'}" +
                    "]",
                    new List<IMovieRating>()
                    {
                        new MovieRating(1,1,5,new DateTime(2018,1,1)),
                        new MovieRating(1,2,3,new DateTime(2018,1,2)),
                        new MovieRating(2,1,3,new DateTime(2018,1,1))
                    }
                }
            };

        public static IEnumerable<Object[]> InvalidJson =>
            new List<object[]>()
            {
                new object[]{"[{Movie:1, Grade:5, Date:'2018-01-01'}]" },
                new object[]{"[{Reviewer:1, Grade:5, Date:'2018-01-01'}]" },
                new object[]{"[{Reviewer:1, Movie:1, Date:'2018-01-01'}]" },
                new object[]{"[{Reviewer:1, Movie:1, Grade:5}]" },
                new object[]{"[{Reviewer:1, Film:1, Grade:5, Date:'2018-01-01'}]" },
                new object[]{"[{Reviewer:1, Reviewer:2, Grade:5, Date:'2018-01-01'}]" }
            };

        [Theory]
        [MemberData(nameof(ValidJson))]
        public void CreateMovieRatingsRepositoryValidJson(string json, List<IMovieRating> expected)
        {
            TextReader jsonStream = new StringReader(json);
            IMovieRatingsRepository repository = new MovieRatingsRepository(jsonStream);

            foreach (IMovieRating mr in expected)
            {
                Assert.True(repository.Reviewers.ContainsKey(mr.ReviewerID));
                Assert.Equal(expected.Where(x => x.ReviewerID == mr.ReviewerID).Count(), repository.Reviewers[mr.ReviewerID].Count);

                Assert.True(repository.Movies.ContainsKey(mr.MovieID));
                Assert.Equal(expected.Where(x => x.MovieID == mr.MovieID).Count(), repository.Movies[mr.MovieID].Count);

                Assert.True(repository.Grades.ContainsKey(mr.Grade));
                Assert.Equal(expected.Where(x => x.Grade == mr.Grade).Count(), repository.Grades[mr.Grade].Count);
            }
        }

        [Theory]
        [MemberData(nameof(InvalidJson))]
        public void CreateMovieRatingsRepositoryInvalidJsonExpectInvalidDataException(string json)
        {
            TextReader jsonStream = new StringReader(json);
            IMovieRatingsRepository repository = null;
            
            var ex = Assert.Throws<InvalidDataException>(() => repository = new MovieRatingsRepository(jsonStream));
            Assert.Equal("Invalid MovieRating", ex.Message);
        }

        [Fact]
        public void CreateMovieRatingsRepositoryReaderIsNullExpectException()
        {
            TextReader jsonStream = null;
            IMovieRatingsRepository repository = null;
            Assert.Throws<ArgumentNullException>(() => repository = new MovieRatingsRepository(jsonStream));
        }

    }
}
