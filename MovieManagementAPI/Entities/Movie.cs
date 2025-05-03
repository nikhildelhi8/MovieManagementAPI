using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagementAPI.Entities
{
    public class Movie
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;

        // Navigation property

        [Required]
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        
        //navigation property for movie details 
        public MovieDetails Detail { get; set; }

        //navigation property for cast
        public ICollection<MovieCast> movieCast { get; set; } = new List<MovieCast>();
    }
}
