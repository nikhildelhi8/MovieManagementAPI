using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieManagementAPI.Entities;
using MovieManagementAPI.FIlterClass;
using MovieManagementAPI.Models;
using MovieManagementAPI.Services;

using SQLitePCL;
using System.Security.Claims;
using System.Text.Json;

namespace MovieManagementAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {


        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        const int maxMoviesPageSize = 20; 

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {

            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {

            try
            {



                var userName = User.FindFirst("sub")?.Value;

                if (userName != "nikhil")
                    return Forbid();
               
                
                    var moviesEntity = await _movieRepository.GetAllMoviesAsync();

                    if (moviesEntity == null)
                        return NoContent();

                    var allMovieDTO = _mapper.Map<IEnumerable<MovieDTO>>(moviesEntity);

                    return Ok(allMovieDTO);

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while retrieving movies.",
                    errorType = ex.GetType().Name, // Include the exception type
                    details = ex.Message // Include the exception message
                });
            }

        }

        [HttpGet("search")]

        public async Task<ActionResult<IEnumerable<MovieDTO>>> SearchMovies([FromQuery] MovieFilterParameters filterParams  , int pageNumber = 1 , int pageSize = 10 )
        {
            try
            {

                if(pageSize > maxMoviesPageSize)
                    pageSize = maxMoviesPageSize;


                var (movies , paginationMetaData) = await _movieRepository.SearchMoviesAsync(filterParams , pageNumber , pageSize);

                if (!movies.Any())
                    return NotFound(new {message = "No movies found matching the given criteria" });


                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

                var movieDTOs = _mapper.Map<IEnumerable<MovieDTO>>(movies);

                return Ok(movieDTOs);
            }

            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occured while searching movies",
                    errorType = ex.GetType().Name,
                    details = ex.Message
                });

            }
        }




        [HttpGet("{id}")]

        public async Task<ActionResult<MovieDTO>> GetMoviesById(int id) {


            var movie = await _movieRepository.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound(new
                {
                    message = $"Movie not found with {id}",

                });
            }

            var movieDTOResult = _mapper.Map<MovieDTO>(movie);
            return Ok(movieDTOResult);
        }


        [HttpGet("{id}/cast")]

        public async Task<ActionResult<IEnumerable<CastNameDTO>>> GetCastDetail(int id)
        {

            try
            {
                var castDetails = await _movieRepository.GetCastDetails(id);

                if (castDetails == null)
                    return NotFound(new { message = "No movie is present with id " });



                var castName = castDetails.movieCast.Select(mc => mc.Cast).ToList();


                //var castDetailsDTO = _mapper.Map<CastDTO>(castDetails);

                var castDetailsDTO = _mapper.Map<List<CastNameDTO>>(castName);

                return Ok(castDetailsDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while retrieving movies.",
                    errorType = ex.GetType().Name, // Include the exception type
                    details = ex.Message // Include the exception message
                });
            }
        }





        //[HttpPost]

        //public ActionResult<MovieDTO> AddMovies ([FromBody] CreatingNewMovieDTO movie)
        //{

        //    if (movie == null)
        //    {
        //        return BadRequest();
        //    }

        //    var newId = _movieDataStore.Movies.Any() ? _movieDataStore.Movies.Max(m => m.Id) : 1;


        //    var newMovie = new MovieDTO
        //    {
        //        Id = ++newId,
        //        Title = movie.Title,
        //        DirectorName = movie.DirectorName,
        //        MovieDetails = movie.MovieDetails,
        //        CastNames = movie.CastNames
        //    };

        //    _movieDataStore.Movies.Add(newMovie);

        //    return CreatedAtAction(nameof(GetMoviesById), new { id = newMovie.Id }, newMovie);



        //}

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> AddMovies([FromBody] CreatingNewMovieDTO newMovie)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                    return BadRequest(new
                    {
                        message = "Invalid data provided.",
                        errors = errorList
                    });
                }

                var movie = await _movieRepository.AddMoviesAsync(newMovie);

                await _movieRepository.SaveChangesAsync();

                var movieDTO = _mapper.Map<MovieDTO>(movie);

                return CreatedAtAction(nameof(GetMoviesById), new { id = movie.Id }, movieDTO);
            }



            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while adding the movie.",
                    errorType = ex.GetType().Name,
                    details = ex.Message
                });
            }
        }

        [HttpPost("{id}/cast")]


        public async Task<ActionResult<CastPostDTO>> AddCastToMovie(int id , [FromBody] CastPostDTO castDetails)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new
                    {
                        message = "Invalid data provided.",
                        errors = errorList
                    });
                }

                var castDetailEntity = _mapper.Map<Cast>(castDetails);


                var castDetailsEntity = await _movieRepository.PostCastDetails(id, castDetailEntity);


                var castDetailsDTO = _mapper.Map<CastDTO>(castDetailsEntity);



                return Ok(castDetailsDTO);
            }

            catch(Exception ex)

            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while adding the movie.",
                    errorType = ex.GetType().Name,
                    details = ex.Message
                });

            }


        }





        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateExistingMovies(int id, [FromBody] UpdateMovieDetailDTO updateMovieDetail)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Message = "Invalid data provided.",
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                    });
                }

                var updateMovie = await _movieRepository.UpdateMovie(updateMovieDetail, id);

                if (updateMovie == null)
                {
                    return NotFound(new
                    {
                        message = "Movie is not available , enter the correct movie ID"
                    });
                }



                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while updating the movie.",
                    errorType = e.GetType().Name,
                    details = e.Message
                });

            }


        }

        //[HttpPatch("{id}")]

        //public async Task<ActionResult<MovieUpdateDto>> PatchMovie(int id, [FromBody] JsonPatchDocument<MovieUpdateDto> patchDoc)
        //{


        //    try
        //    {
        //        if (patchDoc == null)
        //        {
        //            return BadRequest(new
        //            {
        //                message = "Enter proper details for patch data"
        //            });
        //        }


        //        var movieEntity = await _movieRepository.GetMovieByIdAsync(id);

        //        if (movieEntity == null)
        //            return NotFound();


        //        var moviePatch = _mapper.Map<MovieUpdateDto>(movieEntity);

        //        patchDoc.ApplyTo(moviePatch, ModelState);

        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);
        //    }
        //    catch (Exception e) { }

        //}


        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteExistingMovie(int id)
        {

            var movieDetails = await  _movieRepository.GetMovieByIdAsync(id);

            if(movieDetails== null)
            {
                return NotFound(new
                {
                    message = "No movie with id exist"
                });
            }

            _movieRepository.DeleteMovieByIdAsync(movieDetails);

            await _movieRepository.SaveChangesAsync();

            return NoContent();

        }

       

    }
}
