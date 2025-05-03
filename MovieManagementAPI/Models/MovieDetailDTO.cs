using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public class MovieDetailDTO
    {

        
        public int Id { get; set; } 
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MovieId { get; set; }
    }
}
