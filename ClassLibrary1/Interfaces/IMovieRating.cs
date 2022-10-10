using System;

namespace MovieRatings.Interfaces
{
    public interface IMovieRating
    {
        int ReviewerID { get; }
        int MovieID { get; }
        int Grade { get; }
        DateTime Date { get; }
    }
}
