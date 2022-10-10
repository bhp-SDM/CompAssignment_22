using System.Collections.Generic;

namespace MovieRatings.Interfaces
{
    public interface IMovieRatingsRepository
    {
        Dictionary<int, List<IMovieRating>> Reviewers { get; }
        Dictionary<int, List<IMovieRating>> Movies { get; }
        Dictionary<int, List<IMovieRating>> Grades { get; }
    }
}
