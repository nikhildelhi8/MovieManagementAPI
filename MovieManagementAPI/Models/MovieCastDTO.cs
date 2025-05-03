namespace MovieManagementAPI.Models
{
    public class MovieCastDTO
    {

        public int MovieId { get; set; }
        public int CastId { get; set; }

        public string MovieName { get; set; } = string.Empty;

        public string CastName { get; set; } = string.Empty;    
    }
}
