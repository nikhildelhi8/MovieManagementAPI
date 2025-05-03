namespace MovieManagementAPI.Models
{
    public class DirectorDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public List<string> movies { get; set; } = new List<string>();
    }
}
