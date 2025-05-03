using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementAPI.Models;
using MovieManagementAPI.Services;
using System.Runtime.InteropServices;

namespace MovieManagementAPI.Controllers
{
    [Route("api/cast")]
    [ApiController]
    [Authorize(Policy = "MustBeNikhil")]
    public class MovieCastController : ControllerBase
    {

        private readonly IMovieCastRepository _movieCastRepository;

        private readonly IMapper _mapper;


        public MovieCastController(IMovieCastRepository movieCastRepository, IMapper mapper)
        {
            _movieCastRepository = movieCastRepository ?? throw new ArgumentNullException(nameof(movieCastRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet("{id}" , Name = "GetCastDetailsById")]

        public async Task<ActionResult<CastDTO>> GetCastDetailsById(int id)
        {

            try
            {

                var castDetails = await _movieCastRepository.GetCastDetailsByIdAsync(id);

                if (castDetails == null)
                    return NotFound(new { message = $"No cast details is available for id {id}" });


                var castDetailsDTO = _mapper.Map<CastDTO>(castDetails);


                return Ok(castDetailsDTO);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Some error occured while fetching castdetails",
                    errorType = ex.GetType().Name,
                    details = ex.Message
                });
            }
        }


        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateCastDetailsById(int id, [FromBody] CastUpdateDTO updateCast)
        {
            try
            {
                if (updateCast == null || !ModelState.IsValid)
                    return BadRequest(new { message = "Please add valid details which is to be updaed" });


                var updateCastDetails = await _movieCastRepository.UpdateCastDetailsByIdAsync(id, updateCast);


                if (updateCastDetails == null) return NotFound(new { message = "cast info is not present check the id " });


                var castUpdatedResult = _mapper.Map<CastUpdateDTO>(updateCast);

                await _movieCastRepository.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCastDetailsById),
                                        new { id = updateCastDetails.Id },
                                        castUpdatedResult


                );
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Some error occured , see details",
                    Details = ex.Message,
                    errorType = ex.GetType().Name,
                });
            }


        }


        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCastById (int id)
        {
            try
            {
                var castDetails = await _movieCastRepository.GetCastDetailsByIdAsync(id);

                if (castDetails == null) return BadRequest(new { message = "cast with id is not available" });


                _movieCastRepository.DeleteCastDetailsByIdAsync(id , castDetails);

                await _movieCastRepository.SaveChangesAsync();


                return NoContent();

            }

            catch(Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Some error occured , see details",
                    Details = ex.Message,
                    errorType = ex.GetType().Name
                });

            }
        }

        

    }
}
