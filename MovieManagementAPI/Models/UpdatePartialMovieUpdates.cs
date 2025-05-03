using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public class UpdatePartialMovieUpdates
    {

        
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;
        
        public string DirectorName { get; set; } = string.Empty;

        public MovieDetailsResponseDTO MovieDetails { get; set; } = new MovieDetailsResponseDTO();

        public List<string> CastNames { get; set; } = new List<string>();
    }
}
