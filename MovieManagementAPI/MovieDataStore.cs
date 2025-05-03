using System;
using System.Collections.Generic;
using MovieManagementAPI.Models;

namespace MovieManagementAPI
{
    public class MovieDataStore
    {
        //private static readonly MovieDataStore _instance = new MovieDataStore();
        //public static MovieDataStore Instance => _instance; // Singleton Accessor

        public List<MovieDetailDTO> MoviesDetails { get; set; }
        public List<MovieDTO> Movies { get; set; }
        public List<DirectorDTO> Directors { get; set; }
        public List<CastDTO> Casts { get; set; }

        public MovieDataStore()
        {
            // Initialize with sample data
            Directors = new List<DirectorDTO>
            {
                new DirectorDTO { Id = 1, Name = "Steven Spielberg", BirthDate = new DateTime(1946, 12, 18) },
                new DirectorDTO { Id = 2, Name = "Christopher Nolan", BirthDate = new DateTime(1970, 7, 30) },
                new DirectorDTO { Id = 3, Name = "Quentin Tarantino", BirthDate = new DateTime(1963, 3, 27) }
            };

            Movies = new List<MovieDTO>
            {
                new MovieDTO { Id = 1, Title = "Jurassic Park", DirectorName = "Steven Spielberg" },
                new MovieDTO { Id = 2, Title = "Inception" , DirectorName = "Christopher Nolan" },
                new MovieDTO { Id = 3, Title = "Pulp Fiction", DirectorName = "Quentin Tarantino"}
            };

            MoviesDetails = new List<MovieDetailDTO>
            {
                new MovieDetailDTO { Id = 1, Genre = "Adventure", ReleaseDate = new DateTime(1993, 6, 11), MovieId = 1 },
                new MovieDetailDTO { Id = 2, Genre = "Sci-Fi", ReleaseDate = new DateTime(2010, 7, 16), MovieId = 2 },
                new MovieDetailDTO { Id = 3, Genre = "Crime", ReleaseDate = new DateTime(1994, 10, 14), MovieId = 3 }
            };

            Casts = new List<CastDTO>
            {
                new CastDTO { Id = 1, Name = "Sam Neill" },
                new CastDTO { Id = 2, Name = "Leonardo DiCaprio" },
                new CastDTO { Id = 3, Name = "John Travolta" },
                new CastDTO { Id = 4, Name = "Laura Dern" },
                new CastDTO { Id = 5, Name = "Joseph Gordon-Levitt" },
                new CastDTO { Id = 6, Name = "Uma Thurman" }
            };
        }
    }
}


