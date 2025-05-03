using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;
using MovieManagementAPI.Services;

namespace MovieManagementAPI.Controllers
{
    [Route("api/movieDetails/{movieId}/details")]
    [ApiController]
    [Authorize]
    public class MovieDetailsController : ControllerBase
    {



        private readonly IMovieDetailsRepository _movieDetailsRepository;

        private readonly IMapper _mapper;


        public MovieDetailsController(IMovieDetailsRepository movieDetailsRepository, IMapper mapper)
        {
            _movieDetailsRepository = movieDetailsRepository ?? throw new ArgumentNullException(nameof(movieDetailsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }




        [HttpGet(Name = "MovieDetailByIdAsync")]

        public async Task<ActionResult<MovieDetailDTO>> MovieDetailByIdAsync(int movieId)
        {
            try
            {

                var movieDetails = await _movieDetailsRepository.MovieDetailByIdAsync(movieId);

                if (movieDetails == null)
                {
                    return NotFound(new {message = $"Movie with {movieId} is not available"});
                }

                var movieDetailsDTO = _mapper.Map<MovieDetailDTO>(movieDetails);

                
                if (movieDetailsDTO == null)
                    return NotFound();

                return Ok(new
                {
                    message = $"Movie details of {movieId} found",
                    movieDetailsDTO
                });


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

        [HttpPost]

        public async Task<ActionResult> AddMovieDetailAsync(int movieId, [FromBody] MovieDetailDTO movieDetails)
        {

            try
            {

                if (movieDetails == null)
                    return NotFound(new { message = "please add input in correct format" });

                var movieDetailEntity = await _movieDetailsRepository.MovieDetailByIdAsync(movieId);


                if (movieDetailEntity != null)
                    return Conflict(new { message = "Movie Detai already present" });

                var movieDetailResult = await _movieDetailsRepository.AddMovieDetailsAsync(movieId, movieDetails);

                var newMovieDetail = _mapper.Map<MovieDetailDTO>(movieDetailResult);

                //return CreatedAtAction(nameof(MovieDetailByIdAsync), newMovieDetail);

                return NoContent();
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

        [HttpPut]

        public async Task<ActionResult<MovieDetailsUpdateDto>> UpdateMovieDetails(int movieId ,[FromBody] MovieDetailsUpdateDto newMovieDetailsDTO)
        {

           try
           { 

                var existingMovieDetailEntity = await _movieDetailsRepository.MovieDetailByIdAsync(movieId);

                if (existingMovieDetailEntity == null)
                    return NotFound(new {message = $"No movie with {movieId} existed"});


                var updatedMovieDetailsEntity = await  _movieDetailsRepository.UpdateMovieDetails(newMovieDetailsDTO , existingMovieDetailEntity);


                if (updatedMovieDetailsEntity != null)
                    await _movieDetailsRepository.SaveChangesAsync();

                var updatedMovieDetailDTO = _mapper.Map<MovieDetailsUpdateDto>(updatedMovieDetailsEntity);


                return Ok(updatedMovieDetailDTO);
          
           }

           catch(Exception e)
           {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Some internal error occured",
                    errorType = e.GetType().Name,
                    details = e.Message
                });

           }
        }


        [HttpDelete]

        public async Task<ActionResult> DeleteMovieDetails(int movieId)
        {

            try
            {
                var movieDetail = await _movieDetailsRepository.MovieDetailByIdAsync(movieId);

                if (movieDetail == null)
                    return NotFound(new {message = $"Movie with {movieId} is not avalable , plase enter a correct one "});


                var movieEntity = _mapper.Map<MovieDetails>(movieDetail);


                _movieDetailsRepository.DeleteMovieById(movieEntity);

                await _movieDetailsRepository.SaveChangesAsync();

                return NoContent();


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
    }
}
