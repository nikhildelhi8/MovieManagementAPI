using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public class UpdateMovieDetailDTO
    {



        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;


        [Required]
        public string DirectorName { get; set; } = string.Empty;

        [Required]
        public MovieDetailsResponseDTO MovieDetails { get; set; } = new MovieDetailsResponseDTO();

        [Required]
        public List<string> CastNames { get; set; } = new List<string>();
    }
}
