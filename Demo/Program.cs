using MovieRatings.BLL;
using MovieRatings.Interfaces;
using MovieRatings.Repository;
using System;
using System.Diagnostics;
using System.IO;

namespace Demo
{
    class Program
    {
        const string FILE_NAME = "../../../ratings.json";

        static void Main(string[] args)
        {
            IMovieRatingsService service = new MovieRatingsService(new MovieRatingsRepository(new StreamReader(FILE_NAME)));
            Stopwatch sw = Stopwatch.StartNew();
            var result = service.GetTopNMovies(100);
            sw.Stop();
            Console.WriteLine("Result = " + result);
            foreach(int x in result)
                Console.WriteLine("{0,10}  {1, 6:f4}", x, service.GetMovieAgerageRating(x));
            Console.WriteLine("Time = {0:f10} sec.", sw.Elapsed.TotalSeconds);
        }
    }
}
