using System.Collections.Generic;

namespace MovieRatings.Interfaces
{
    public interface IMovieRatingsService
    {
        /// <summary>
        /// Returns the number of reviews done by reviewer with id <paramref name="reviewerID"/>.
        /// returns 0, if <paramref name="reviewerID"/> does not exist. 
        /// </summary>
        /// <param name="reviewerID">The id of the reviewer</param>
        /// <returns>The number of reviews done by the given reviewer</returns>
        int GetReviewerNumberOfReviews(int reviewerID);

        /// <summary>
        /// Returns the average rating based on all reviews done by reviewer with id <paramref name="reviewerID"/>
        /// Throws ArgumentExcetion if <paramref name="reviewerID"/> does not exist.
        /// </summary>
        /// <param name="reviewerID">The id of the reviewer</param>
        /// <returns>Average rating from reviewer</returns>
        /// <throws>ArgumentException</throws>
        double GetReviewerAgerageRating(int reviewerID);

        /// <summary>
        /// Returns the number of reviews from reviewer with id <paramref name="reviewerID"/> matching the given <paramref name="="rating"/>"
        /// </summary>
        /// <param name="reviewerID">The id of the reviewer</param>
        /// <param name="rating">The rating to count</param>
        /// <returns>The number of reviews with the given rating</returns>
        int GetReviewerNumberOfRating(int reviewerID, int rating);

        /// <summary>
        /// Returns the number of reviews for the movie with id <paramref name="movieID"/>.
        /// returns 0, if <paramref name="movieID"/> does not exist. 
        /// </summary>
        /// <param name="movieID">The id of the movie</param>
        /// <returns>The number of reviews for the given movie</returns>
        int GetMovieNumberOfReviews(int movieID);

        /// <summary>
        /// Returns the average rating based on all reviews for the movie with id <paramref name="movieID"/>
        /// Throws ArgumentExcetion if <paramref name="movieID"/> does not exist.
        /// </summary>
        /// <param name="movieID">The id of the movie</param>
        /// <returns>Average rating for the movie</returns>
        /// <throws>ArgumentException</throws>
        double GetMovieAgerageRating(int movieID);

        /// <summary>
        /// Returns the number of reviews for the movie with id <paramref name="movieID"/> matching the given <paramref name="="rating"/>"
        /// Returns 0, if the <paramref name="movieID"/> does not exist.
        /// </summary>
        /// <param name="movieID">The id of the movie</param>
        /// <param name="rating">The rating to count</param>
        /// <returns>The number of reviews with the given rating</returns>
        int GetMovieNumberOfRating(int reviewerID, int rating);

        /// <summary>
        /// Returns the Movie id(s) with the highest number of top ratings.
        /// </summary>
        /// <returns>MovieID(s) most top-rated</returns>
        int[] GetTopRatedMovies();

        /// <summary>
        /// Returns the reviewer id(s), for reviewer with the most reviews.
        /// </summary>
        /// <returns>Reviewer id(s) of reviewer with most reviews</returns>
        int[] GetReviewersMostReviews();

        /// <summary>
        /// Returns the top <paramref name="n"/> movie ids.
        /// The movies are seledted based on average ratings.
        /// </summary>
        /// <param name="n">The length of the hit list</param>
        /// <returns>the high-score list of (N) movies</returns>
        int[] GetTopNMovies(int n);

        /// <summary>
        /// Returns a list of movies ids for all movies reveiwed by the reviewer with id <paramref name="reviewerID"/>.
        /// The list is sorted decreasing by rate first, and date secondly.
        /// </summary>
        /// <param name="reviewerID"></param>
        /// <returns>a List of movie ids reviewed by the given reviewer.</returns>
        int[] GetReviewerMovies(int reviewerID);

        /// <summary>
        /// Returns a list of reviewers ids for all reviewers who has reveiwed the movie with id <paramref name="movieID"/>.
        /// The list is sorted decreasing by rate first, and date secondly.
        /// </summary>
        /// <param name="movieID"></param>
        /// <returns>a List of reviewer ids who reviewed the given movie.</returns>
        int[] GetMovieReviewers(int movieID);
    }
}
