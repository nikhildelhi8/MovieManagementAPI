using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public class CastPostDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
