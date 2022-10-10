using MovieRatings.Interfaces;
using System;

namespace MovieRatings.BE
{
    public class MovieRating : IMovieRating
    {
        public const int MINIMUM_GRADE = 1;
        public const int MAXIMUM_GRADE = 5;

        public int ReviewerID { get; }

        public int MovieID { get; }

        public int Grade { get; }

        public DateTime Date { get; }

        public MovieRating(int reviewerID, int movieID, int grade, DateTime date)
        {
            if (grade < MINIMUM_GRADE || grade > MAXIMUM_GRADE)
                throw new ArgumentException("Grade is out of range");

            if (date == null)
                throw new ArgumentException("Date is missing");
            
            ReviewerID = reviewerID;
            MovieID = movieID;
            Grade = grade;
            Date = date;
        }

        public override string ToString()
        {
            return string.Format("Reviewer: {0}, Movie: {1}, Grade: {2}, Date: {3}", ReviewerID, MovieID, Grade, Date);
        }
    }
}
