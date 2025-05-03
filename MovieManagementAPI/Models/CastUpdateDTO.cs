using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public class CastUpdateDTO
    {

        [Required]
        public string Name { get; set; } = string.Empty;


    }
}
