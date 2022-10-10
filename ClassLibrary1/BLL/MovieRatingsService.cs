using MovieRatings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRatings.BLL
{
    public class MovieRatingsService : IMovieRatingsService
    {
        private readonly IMovieRatingsRepository Repository;

        public MovieRatingsService(IMovieRatingsRepository repository)
        {
            Repository = repository ?? throw new ArgumentException("Missing MovieRatingsRepository");
        }

        /// <summary>
        /// Returns the number of reviews done by reviewer with id <paramref name="reviewerID"/>.
        /// returns 0, if <paramref name="reviewerID"/> does not exist. 
        /// </summary>
        /// <param name="reviewerID">The id of the reviewer</param>
        /// <returns>The number of reviews done by the given reviewer</returns>
        public int GetReviewerNumberOfReviews(int reviewerID)
        {
            return Repository.Reviewers.ContainsKey(reviewerID) ? Repository.Reviewers[reviewerID].Count : 0;
        }

        /// <summary>
        /// Returns the average rating based on all reviews done by reviewer with id <paramref name="reviewerID"/>
        /// Throws ArgumentExcetion if <paramref name="reviewerID"/> does not exist.
        /// </summary>
        /// <param name="reviewerID">The id of the reviewer</param>
        /// <returns>Average rating from reviewer</returns>
        /// <throws>ArgumentException</throws>
        public double GetReviewerAgerageRating(int reviewerID)
        {
            if (!Repository.Reviewers.ContainsKey(reviewerID))
                throw new ArgumentException("ReviewerID does not exist");
            int sum = 0;
            foreach (IMovieRating mr in Repository.Reviewers[reviewerID])
                sum += mr.Grade;
            return sum / (double)Repository.Reviewers[reviewerID].Count;
            //return Repository.Reviewers[reviewerID].Average((x) => x.Grade);
        }


        /// <summary>
        /// Returns the number of reviews from reviewer with id <paramref name="reviewerID"/> matching the given <paramref name="="rating"/>"
        /// </summary>
        /// <param name="reviewerID">The id of the reviewer</param>
        /// <param name="rating">The rating to count</param>
        /// <returns>The number of reviews with the given rating</returns>
        public int GetReviewerNumberOfRating(int reviewerID, int rating)
        {
            int count = 0;
            if (!Repository.Reviewers.ContainsKey(reviewerID))
                return 0;
            foreach (IMovieRating mr in Repository.Reviewers[reviewerID])
                if (mr.Grade == rating)
                    count++;
            return count;
            //return Repository.Reviewers.ContainsKey(reviewerID) ? Repository.Reviewers[reviewerID].Where((x) => x.Grade == rating).Count() : 0;
        }



        /// <summary>
        /// Returns the number of reviews for the movie with id <paramref name="movieID"/>.
        /// returns 0, if <paramref name="movieID"/> does not exist. 
        /// </summary>
        /// <param name="movieID">The id of the movie</param>
        /// <returns>The number of reviews for the given movie</returns>
        public int GetMovieNumberOfReviews(int movieID)
        {
            return Repository.Movies.ContainsKey(movieID) ? Repository.Movies[movieID].Count() : 0;
        }



        /// <summary>
        /// Returns the average rating based on all reviews for the movie with id <paramref name="movieID"/>
        /// Throws ArgumentExcetion if <paramref name="movieID"/> does not exist.
        /// </summary>
        /// <param name="movieID">The id of the movie</param>
        /// <returns>Average rating for the movie</returns>
        /// <throws>ArgumentException</throws>
        public double GetMovieAgerageRating(int movieID)
        {
            if (!Repository.Movies.ContainsKey(movieID))
                throw new ArgumentException("MovieID does not exist");

            int sum = 0;
            foreach (IMovieRating mr in Repository.Movies[movieID])
                sum += mr.Grade;
            return sum / (double)Repository.Movies[movieID].Count;
            //return Repository.Movies[movieID].Average((x) => x.Grade);
        }



        /// <summary>
        /// Returns the number of reviews for the movie with id <paramref name="movieID"/> matching the given <paramref name="="rating"/>"
        /// Returns 0, if the <paramref name="movieID"/> does not exist.
        /// </summary>
        /// <param name="movieID">The id of the movie</param>
        /// <param name="rating">The rating to count</param>
        /// <returns>The number of reviews with the given rating</returns>
        public int GetMovieNumberOfRating(int movieID, int rating)
        {
            if (!Repository.Movies.ContainsKey(movieID))
                return 0;
            int count = 0;
            foreach (IMovieRating mr in Repository.Movies[movieID])
                if (mr.Grade == rating)
                    count++;
            return count;

            //return Repository.Movies.ContainsKey(movieID) ? Repository.Movies[movieID].Where((x) => x.Grade == rating).Count() : 0;
        }



        /// <summary>
        /// Returns the Movie id(s) with the highest number of top ratings.
        /// </summary>
        /// <returns>MovieID(s) most top-rated</returns>
        public int[] GetTopRatedMovies()
        {
            SortedDictionary<int, List<int>> result = new SortedDictionary<int, List<int>>();

            foreach (KeyValuePair<int, List<IMovieRating>> kv in Repository.Movies)
            {
                int count = GetMovieNumberOfRating(kv.Key, 5);
                if (!result.ContainsKey(count))
                    result[count] = new List<int>();
                result[count].Add(kv.Key);
            }

            return result.Last().Value.ToArray();

            //    int max = Repository.Grades[5].GroupBy(x => x.MovieID).Max(y => y.Count());
            //    return Repository.Grades[5].GroupBy(keySelector: x => x.MovieID).Where(y => y.Count() == max).Select(z => z.Key).ToArray();
        }



        /// <summary>
        /// Returns the reviewer id(s), for reviewer with the most reviews.
        /// </summary>
        /// <returns>Reviewer id(s) of reviewer with most reviews</returns>
        public int[] GetReviewersMostReviews()
        {
            SortedDictionary<int, List<int>> result = new SortedDictionary<int, List<int>>();

            foreach (KeyValuePair<int, List<IMovieRating>> kv in Repository.Reviewers)
            {
                int count = GetReviewerNumberOfReviews(kv.Key);
                if (!result.ContainsKey(count))
                    result[count] = new List<int>();
                result[count].Add(kv.Key);
            }

            return result.Last().Value.ToArray();

            //int max = Repository.Reviewers.Values.SelectMany(x => x).GroupBy(y => y.ReviewerID).Max(z => z.Count());
            //return Repository.Reviewers.Values.SelectMany(x => x).GroupBy(y => y.ReviewerID).Where(z => z.Count() == max).Select(h => h.Key).ToArray();
        }



        /// <summary>
        /// Returns the top <paramref name="n"/> movie ids.
        /// The movies are selected based on average ratings.
        /// </summary>
        /// <param name="n">The length of the hit list</param>
        /// <returns>the high-score list of (N) movies</returns>
        public int[] GetTopNMovies(int n)
        {
            List<KeyValuePair<int, double>> moviesByAverageRating = new List<KeyValuePair<int, double>>();
            foreach (int movieID in Repository.Movies.Keys)
                moviesByAverageRating.Add(new KeyValuePair<int, double>(movieID, GetMovieAgerageRating(movieID)));
            
            moviesByAverageRating.Sort((a,b) => 
            {
                return (b.Value > a.Value ? 1 : (b.Value < a.Value)? -1 : 0);
            });
            List<int> movies = new List<int>();
 
            for (int i = 0; i < n; i++)            
                movies.Add(moviesByAverageRating[i].Key);
            return movies.ToArray();

            //return Repository.Movies.Values.SelectMany(a => a).GroupBy(b => b.MovieID).OrderByDescending(c => c.Average(d => d.Grade)).Select(e => e.Key).Take(n).ToArray();
        }



        /// <summary>
        /// Returns a list of movies ids for all movies reveiwed by the reviewer with id <paramref name="reviewerID"/>.
        /// The list is sorted decreasing by rate first, and date secondly.
        /// </summary>
        /// <param name="reviewerID"></param>
        /// <returns>a List of movie ids reviewed by the given reviewer.</returns>
        public int[] GetReviewerMovies(int reviewerID)
        {
            if (!Repository.Reviewers.ContainsKey(reviewerID)) return new int[0];
            
            List<IMovieRating> movies = new List<IMovieRating>(Repository.Reviewers[reviewerID]);
            movies.Sort((a, b) =>
            {
                if (a.Grade == b.Grade)
                    return (DateTime.Compare(a.Date, b.Date));
                return (b.Grade - a.Grade);
            });
            
            List<int> sortedMovieList = new List<int>();
            foreach (IMovieRating mr in movies)
                sortedMovieList.Add(mr.MovieID);
            return sortedMovieList.ToArray();
            //return Repository.Reviewers[reviewerID].OrderByDescending(x => x.Grade).ThenBy(y => y.Date).Select(z => z.MovieID).ToArray();
        }



        /// <summary>
        /// Returns a list of reviewers ids for all reviewers who has reveiwed the movie with id <paramref name="movieID"/>.
        /// The list is sorted decreasing by rate first, and date secondly.
        /// </summary>
        /// <param name="movieID"></param>
        /// <returns>a List of reviewer ids who reviewed the given movie.</returns>
        public int[] GetMovieReviewers(int movieID)
        {
            if (!Repository.Movies.ContainsKey(movieID)) return new int[0];
            
            List<IMovieRating> reviewers = new List<IMovieRating>(Repository.Movies[movieID]);
            reviewers.Sort((a, b) =>
            {
                if (a.Grade == b.Grade)                         // same grade?
                    return (DateTime.Compare(a.Date, b.Date));  // then ascending by date
                return (b.Grade - a.Grade);                     // else Descending by grade
            });

            List<int> sortedReviewerList = new List<int>();  
            foreach (IMovieRating mr in reviewers)
                sortedReviewerList.Add(mr.ReviewerID);
            return sortedReviewerList.ToArray();

            //return Repository.Movies[movieID].OrderByDescending(x => x.Grade).ThenBy(y => y.Date).Select(z => z.ReviewerID).ToArray();
        }
    }
}
