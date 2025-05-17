using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementAPI.Models;
using MovieManagementAPI.Services;

namespace MovieManagementAPI.Controllers
{
    [Route("api/director")]
    [ApiController]
    public class DirectorController : ControllerBase
    {

        private readonly ILogger<DirectorController> _logger;
        private readonly IMapper _mapper; 
        private readonly IDirectorRepository _directorRepository;



        public DirectorController( ILogger<DirectorController> logger , IDirectorRepository director , IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _directorRepository = director;
        }



        [HttpGet]

        public async Task<ActionResult<IEnumerable<DirectorDTO>>> getAllDirectorList()
        {
            try
            {
                var director = await _directorRepository.GetAllDirectorDetailsAsync();

                if (director == null)
                    return NotFound(new
                    {
                        message = "No director is added"
                    });

                //var directorDetailsDTO = _mapper.Map<IEnumerable<DirectorDTO>>(director);


                var directorDetailsDTO = director.Select(d => new DirectorDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    BirthDate = d.BirthDate,
                    movies = d.Movies.Select(m => m.Title).ToList()


                }).ToList();

                return Ok(directorDetailsDTO);

            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving changes.", ex);

            }
            


        }



    }
}
