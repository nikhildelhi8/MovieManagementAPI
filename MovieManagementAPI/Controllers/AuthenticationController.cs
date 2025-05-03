using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MovieManagementAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;

        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }

        }

        private class MovieInfoUser
        {

            public int UserId   { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; } 

            public MovieInfoUser(int userId , string userName , string firstName , string lastName)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
            }

        }

        public AuthenticationController(IConfiguration configuration)
        {

            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        }


        [HttpPost("authenticate")]

        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {

            //step1 validate username/password

            var user  = ValidateUserCredentials(
                authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            if (user == null)
                return Unauthorized();


            //step2 create a token 

            var securityKey = new SymmetricSecurityKey(
                Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
 

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken, 
                DateTime.UtcNow, 
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn  = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            return tokenToReturn;






        }

        private MovieInfoUser ValidateUserCredentials(string? userName, string? password)
        {

            //we dont have a user DB or table , if you have , check the passed through username / pasword againset wahys stored in the databse 
            //for demo purpos , we asime the credentials are valid 

            //return a new MovieInfoUser ( values would normally come from your user DB/table)

            return new MovieInfoUser(1, userName ?? "", "Nikhil", "singh");
         
        }
    }
}
