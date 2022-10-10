using MovieRatings.BLL;
using MovieRatings.Interfaces;
using MovieRatings.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XUnitTestProject
{
    public class MovieRatingsRepositoryFixture
    {
        private const string FILE_NAME = "../../../ratings.json";

        public IMovieRatingsRepository Repository { get; private set; }
        public int ReviewerWithMostReviews { get; private set; }
        public int MovieWithMostReviews { get; private set; }
        
        public MovieRatingsRepositoryFixture()
        {
            Repository = new MovieRatingsRepository(new StreamReader(FILE_NAME));
            ReviewerWithMostReviews = GetMaxReviews(Repository.Reviewers);
            MovieWithMostReviews = GetMaxReviews(Repository.Movies);
        }

        private int GetMaxReviews(Dictionary<int, List<IMovieRating>> dictionary)
        {
            int max = 0;
            int result = 0;
            foreach (KeyValuePair<int, List<IMovieRating>> kv in dictionary)
            {
                if (kv.Value.Count > max)
                {
                    max = kv.Value.Count;
                    result = kv.Key;
                }
            }
            return result;
        }
    }
}
