using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public class CreatingNewMovieDTO
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty; // Movie title

        [Required]
        public string DirectorName { get; set; } = string.Empty; // Director's name (user input)

        [Required]
        public MovieDetailsResponseDTO MovieDetails { get; set; }  // Movie details (genre, release date)

        [Required]
        public List<string> CastNames { get; set; } = new List<string>(); // List of cast member names
    }
}

