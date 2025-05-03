using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.DbContext;
using MovieManagementAPI.Entities;
using MovieManagementAPI.FIlterClass;
using MovieManagementAPI.Models;
using MovieManagementAPI.Shared;

namespace MovieManagementAPI.Services
{
    public class MovieRepository : IMovieRepository
    {

        private readonly MovieDbContext _movieContext;



        public MovieRepository(MovieDbContext movieContext)
        {
            _movieContext = movieContext ?? throw new ArgumentNullException(nameof(movieContext));
        }



        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {

            try
            {

                var movies  = await _movieContext.Movies.OrderBy(m => m.Id)
                    .Include(m => m.Director)
                    .Include(m=> m.Detail)
                    .Include(m => m.movieCast)
                        .ThenInclude(mc => mc.Cast)
                    .ToListAsync();

                if(!movies.Any())
                    return Enumerable.Empty<Movie>();

                return movies; 

            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving movies.", ex);
            }

        }

        //public async Task<IEnumerable<Movie>> GetAllMoviesAsync(string? movieName, string? searchQuery)
        //{

        //    try
        //    {

        //        if (string.IsNullOrEmpty(movieName) && string.IsNullOrWhiteSpace(searchQuery))
        //            return await GetAllMoviesAsync();



        //        //collection to start from ( we are using IQueryable which supports deffered execution 

        //        var collection = _movieContext.Movies as IQueryable<Movie>;




        //        movieName = movieName.Trim();


        //        var movies = await _movieContext.Movies.OrderBy(m => m.Id)
        //                   .Where(m => m.Title.ToLower() == movieName.ToLower())
        //                   .Include(m => m.Director)
        //                   .Include(m => m.Detail)
        //                   .Include(m => m.movieCast)
        //                       .ThenInclude(mc => mc.Cast)
        //                       .ToListAsync();

        //        if (movies == null)
        //            return Enumerable.Empty<Movie>();


        //        return movies;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new Exception("An error occured while retrieving movie", ex);
        //    }

        //}

        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            try
            {
                var movieWithId = await _movieContext.Movies
                    .Include(m => m.Director)
                    .Include(m => m.Detail)
                    .Include(m => m.movieCast)
                        .ThenInclude(mc => mc.Cast)
                    .FirstOrDefaultAsync(m => m.Id == movieId);

                if (movieWithId == null)
                    return null;

                return movieWithId;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the movie.", ex);
            }

        }

        public async Task<Movie> AddMoviesAsync(CreatingNewMovieDTO newMovie)
        {
            if(newMovie == null)
            {
                throw new ArgumentNullException(nameof(newMovie));
            }

            //Resolve DirectorId from DirectorName

            var director = await _movieContext.Directors.FirstOrDefaultAsync(d => d.Name == newMovie.DirectorName);
            if (director == null)
            {
                director = new Director
                {
                    Name = newMovie.DirectorName,
                    BirthDate = new DateTime(),// Default birth date, you can change this as needed
                    Movies = new List<Movie>()
                };
                await _movieContext.Directors.AddAsync(director);
                await SaveChangesAsync();
            }

            

            var movie = new Movie
            {
                Title = newMovie.Title,
                DirectorId = director.Id,
                Detail = new MovieDetails
                {
                    Genre = newMovie.MovieDetails.Genre,
                    ReleaseDate = newMovie.MovieDetails.ReleaseDate
                }
            };

            await _movieContext.Movies.AddAsync(movie);
            await SaveChangesAsync();

            // Handle CastName 

            foreach (var castName in newMovie.CastNames)
            {
                // Resolve CastId from CastName
                var cast = await _movieContext.Casts.FirstOrDefaultAsync(c => c.Name == castName);
                if (cast == null)
                {
                    // Add new cast if it doesn't exist
                    cast = new Cast { Name = castName };
                    await _movieContext.Casts.AddAsync(cast);
                    await SaveChangesAsync();
                }

                // Add entry to MovieCast table
                var movieCast = new MovieCast
                {
                    MovieId = movie.Id,
                    CastId = cast.Id
                };
                await _movieContext.MovieCasts.AddAsync(movieCast);
                await SaveChangesAsync();
            }

            //await _movieContext.SaveChangesAsync();


            return movie; 
           

        }

        public async Task<Movie?> UpdateMovie(UpdateMovieDetailDTO newMovie  , int id)
        {

            var movie  = await _movieContext.Movies
                .Include(m => m.Director)
                .Include(m => m.Detail)
                .Include(m => m.movieCast)
                    .ThenInclude(mc => mc.Cast)
                .FirstOrDefaultAsync(m => m.Id == id);


            if(movie == null)
                throw new Exception($"Movie with id {id} not found.");

            movie.Title = newMovie.Title;

            var director = await _movieContext.Directors.FirstOrDefaultAsync(d => d.Name == newMovie.DirectorName);

            if (director == null)
            {
                director = new Director
                {
                    Name = newMovie.DirectorName,
                    BirthDate = new DateTime(),
                };

                await _movieContext.Directors.AddAsync(director);
                await SaveChangesAsync();

            }
            movie.DirectorId = director.Id; 

            //Update the movieDetails 

            movie.Detail.Genre = newMovie.MovieDetails.Genre;
            movie.Detail.ReleaseDate = newMovie.MovieDetails.ReleaseDate;

            //update the cast 

            movie.movieCast.Clear();
            foreach(var castName in newMovie.CastNames)
            {
                var cast = await _movieContext.Casts.FirstOrDefaultAsync(c => c.Name == castName);
                
                if(cast == null)
                {
                    cast = new Cast { Name = castName };
                    await _movieContext.Casts.AddAsync(cast);
                    await SaveChangesAsync();
                }
                movie.movieCast.Add(new MovieCast
                {
                    MovieId = movie.Id,
                    CastId = cast.Id
                });
            }

            await SaveChangesAsync();

            return movie; 

        }

        //public async Task<Movie?> PartialMovieUpdate(JsonPatchDocument<MovieUpdateDto> patchDoc, int id)
        //{
           
        //}

       


        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _movieContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving changes.", ex);
            }
        }

        public void  DeleteMovieByIdAsync(Movie movieDetails)
        {

            _movieContext.Movies.Remove(movieDetails);
            
        }

        public async Task<Movie?> GetCastDetails(int id)
        {
           

            try
            {
                if (!_movieContext.Movies.Any(c => c.Id == id))
                    return null; 


                var castDetails = await _movieContext.Movies.Include(m => m.movieCast).ThenInclude(mc => mc.Cast).FirstOrDefaultAsync(m => m.Id == id);

                

                if (castDetails == null)
                    return null;

                return castDetails; 
                
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving changes.", ex);

            }
        }

        public async Task<Cast?> PostCastDetails(int movieId, Cast cast)
        {
            try
            {

                var movieDetail = _movieContext.Movies.Where(m => m.Id == movieId).FirstOrDefault();    

                if (movieDetail == null) return null;


                await _movieContext.Casts.AddAsync(cast);

                await SaveChangesAsync();



                MovieCast movieCastNew = new MovieCast()
                {
                    CastId = cast.Id,
                    MovieId = movieId
                };


                await _movieContext.MovieCasts.AddAsync(movieCastNew);
                await SaveChangesAsync();

                return cast; 

            }

            catch(Exception ex)
            {
                throw new Exception("An error occurred while saving changes.", ex);

            }

        }

        public async Task<(IEnumerable<Movie>, PaginationMetaData)> SearchMoviesAsync(MovieFilterParameters filter , int pageNumber  , int pageSize)
        {

            IQueryable<Movie> query = _movieContext.Movies
                                       .Include(m => m.Director)
                                       .Include(m => m.Detail)
                                       .Include(m => m.movieCast)
                                           .ThenInclude(mc => mc.Cast);



            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(m => m.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Genre))
            {
                query = query.Where(m => m.Detail.Genre.ToLower().Contains(filter.Genre.ToLower()));
               
            }

            var totalItemCount = await query.CountAsync();

            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);



            var collectionToReturn  =  await query.OrderBy(m => m.Id).ApplyPagingAsync(pageNumber, pageSize);

            return (collectionToReturn, paginationMetaData);
        }

       
    }
}
