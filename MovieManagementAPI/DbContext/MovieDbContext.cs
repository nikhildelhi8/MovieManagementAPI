
using MovieManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieManagementAPI.DbContext
{
    public class MovieDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieDetails> MovieDetails { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite key for MovieCast
            modelBuilder.Entity<MovieCast>()
                .HasKey(mc => new { mc.MovieId, mc.CastId });
              
            // Configure relationships
            modelBuilder.Entity<MovieCast>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.movieCast)
                .HasForeignKey(mc => mc.MovieId);

            modelBuilder.Entity<MovieCast>()
                .HasOne(mc => mc.Cast)
                .WithMany(c => c.MovieCast)
                .HasForeignKey(mc => mc.CastId);

            // Configure one-to-one relationship between Movie and MovieDetails
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Detail)
                .WithOne(md => md.Movie)
                .HasForeignKey<MovieDetails>(md => md.MovieId);

            // Seed data
            modelBuilder.Entity<Director>().HasData(
                new Director { Id = 1, Name = "Christopher Nolan", BirthDate = new DateTime(1970, 7, 30) },
                new Director { Id = 2, Name = "Steven Spielberg", BirthDate = new DateTime(1946, 12, 18) },
                new Director { Id = 3, Name = "Quentin Tarantino", BirthDate = new DateTime(1963, 3, 27) }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Inception", DirectorId = 1 },
                new Movie { Id = 2, Title = "Jurassic Park", DirectorId = 2 },
                new Movie { Id = 3, Title = "Pulp Fiction", DirectorId = 3 }
            );

            modelBuilder.Entity<Cast>().HasData(
                new Cast { Id = 1, Name = "Leonardo DiCaprio" },
                new Cast { Id = 2, Name = "Joseph Gordon-Levitt" },
                new Cast { Id = 3, Name = "Sam Neill" },
                new Cast { Id = 4, Name = "Laura Dern" },
                new Cast { Id = 5, Name = "John Travolta" },
                new Cast { Id = 6, Name = "Uma Thurman" }
            );

            modelBuilder.Entity<MovieCast>().HasData(
                new MovieCast { MovieId = 1, CastId = 1 },
                new MovieCast { MovieId = 1, CastId = 2 },
                new MovieCast { MovieId = 2, CastId = 3 },
                new MovieCast { MovieId = 2, CastId = 4 },
                new MovieCast { MovieId = 3, CastId = 5 },
                new MovieCast { MovieId = 3, CastId = 6 }
            );

            modelBuilder.Entity<MovieDetails>().HasData(
                new MovieDetails { Id = 1, Genre = "Sci-Fi", ReleaseDate = new DateTime(2010, 7, 16), MovieId = 1 },
                new MovieDetails { Id = 2, Genre = "Adventure", ReleaseDate = new DateTime(1993, 6, 11), MovieId = 2 },
                new MovieDetails { Id = 3, Genre = "Crime", ReleaseDate = new DateTime(1994, 10, 14), MovieId = 3 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id =1 , Username = "nikhil"  , PasswordHash = "qwerty@123"}

             );
        }

    }
}

