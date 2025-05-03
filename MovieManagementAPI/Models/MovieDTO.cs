using MovieManagementAPI.Entities;

namespace MovieManagementAPI.Models
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string DirectorName { get; set; } = string.Empty;

        public MovieDetailsResponseDTO MovieDetails { get; set; } = new MovieDetailsResponseDTO();

        public List<string> CastNames { get; set; } = new List<string>();   

    }
}
