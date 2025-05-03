namespace MovieManagementAPI.Models
{
    public class CastDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<string> movies { get; set; } = new List<string>();

    }
}
