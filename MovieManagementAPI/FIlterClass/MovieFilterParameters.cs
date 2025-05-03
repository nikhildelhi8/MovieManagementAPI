using Microsoft.AspNetCore.Mvc;

namespace MovieManagementAPI.FIlterClass
{
    public class MovieFilterParameters
    {

        [FromQuery(Name ="title")]
        public string? Title { get; set; }
        [FromQuery(Name = "genre")]
        public string? Genre { get; set; }

    }
}
