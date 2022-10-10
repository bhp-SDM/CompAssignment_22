using MovieRatings.BE;
using MovieRatings.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MovieRatings.Repository
{

    public class MovieRatingsRepository : IMovieRatingsRepository
    {
        public Dictionary<int, List<IMovieRating>> Reviewers { get; }

        public Dictionary<int, List<IMovieRating>> Movies { get; }

        public Dictionary<int, List<IMovieRating>> Grades { get; }

        public MovieRatingsRepository(TextReader reader)
        {
            Reviewers = new Dictionary<int, List<IMovieRating>>();
            Movies = new Dictionary<int, List<IMovieRating>>();
            Grades = new Dictionary<int, List<IMovieRating>>();
            Load(reader);
        }

        private void Load(TextReader textReader)
        {
            using (TextReader streamReader = textReader)
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        IMovieRating mr = ReadOneMovieRating(reader);
                        AddToDictionary(Reviewers, mr.ReviewerID, mr);
                        AddToDictionary(Movies, mr.MovieID, mr);
                        AddToDictionary(Grades, mr.Grade, mr);
                    }
                }
            }
        }

        private void AddToDictionary(Dictionary<int, List<IMovieRating>> dictionary, int key, IMovieRating value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = new List<IMovieRating>();
            dictionary[key].Add(value);
        }

        private IMovieRating ReadOneMovieRating(JsonTextReader reader)
        {
            int reviewer = 0;
            int movie = 0;
            int grade = 0;
            DateTime date = DateTime.Now;

            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                reader.Read();
                switch (reader.Value)
                {
                    case "Reviewer": reviewer = (int)reader.ReadAsInt32(); count += 1; break;
                    case "Movie": movie = (int)reader.ReadAsInt32(); count += 2; break;
                    case "Grade": grade = (int)reader.ReadAsInt32(); count += 4; break;
                    case "Date": date = (DateTime)reader.ReadAsDateTime(); count += 8; break;
                    default: throw new InvalidDataException("Invalid MovieRating");
                }
            }
            if (count != 15)
                throw new InvalidDataException("Invalid MovieRating");
            return new MovieRating(reviewer, movie, grade, date);
        }
    }
}
