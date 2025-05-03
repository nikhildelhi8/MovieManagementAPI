using MovieManagementAPI.Enum;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public class MovieDetailsUpdateDto
    {

        [Required]
        [EnumDataType(typeof(GenreType))]
        public string? Genre { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
    }

}
