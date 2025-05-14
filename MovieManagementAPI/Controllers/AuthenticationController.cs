using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieManagementAPI.Entities;
using MovieManagementAPI.Models;
using MovieManagementAPI.Services;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MovieManagementAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository; 
        private readonly IConfiguration _configuration;

        //public class AuthenticationRequestBody
        //{
        //    public string? UserName { get; set; }
        //    public string? Password { get; set; }

        //}

        //Constructor
        public AuthenticationController(IConfiguration configuration, IUserRepository userRepository)
        {

            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(configuration));

        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDTO userDTO)
        {

            var existingUser = await _userRepository.GetUserByUsernameAsync(userDTO.UserName.ToLower());
            if (existingUser != null) return BadRequest("User already exists");

            var user = new User
            {
                Username = userDTO.UserName.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password)
            };

            await _userRepository.AddUserAsync(user);

            return Ok("user registered");

        }

        [HttpPost("login")]

        public async Task<ActionResult> Login(UserDTO userDTO)
        {

            var user = await _userRepository.GetUserByUsernameAsync(userDTO.UserName.ToLower());

            if(user == null || !BCrypt.Net.BCrypt.Verify(userDTO.Password , user.PasswordHash)){

                return Unauthorized("Invalid credentials");
            }

            var token = GenerateJWtToken(user.Username);


            return Ok(new { token });
        }


        private string GenerateJWtToken(string username)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name , username)
            };

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(

                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentioncation:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }




        //private class MovieInfoUser
        //{

        //    public int UserId   { get; set; }
        //    public string UserName { get; set; }
        //    public string FirstName { get; set; }
        //    public string LastName { get; set; } 

        //    public MovieInfoUser(int userId , string userName , string firstName , string lastName)
        //    {
        //        UserId = userId;
        //        UserName = userName;
        //        FirstName = firstName;
        //        LastName = lastName;
        //    }

        //}

        

        //[HttpPost("authenticate")]

        //public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        //{

        //    //step1 validate username/password

        //    var user  = ValidateUserCredentials(
        //        authenticationRequestBody.UserName,
        //        authenticationRequestBody.Password);

        //    if (user == null)
        //        return Unauthorized();


        //    //step2 create a token 

        //    var securityKey = new SymmetricSecurityKey(
        //        Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));

        //    var signingCredentials = new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256);

        //    var claimsForToken = new List<Claim>();
        //    claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
        //    claimsForToken.Add(new Claim("given_name", user.FirstName));
        //    claimsForToken.Add(new Claim("family_name", user.LastName));
 

        //    var jwtSecurityToken = new JwtSecurityToken(
        //        _configuration["Authentication:Issuer"],
        //        _configuration["Authentication:Audience"],
        //        claimsForToken, 
        //        DateTime.UtcNow, 
        //        DateTime.UtcNow.AddHours(1),
        //        signingCredentials);

        //    var tokenToReturn  = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


        //    return tokenToReturn;
        //}

        //private MovieInfoUser ValidateUserCredentials(string? userName, string? password)
        //{

        //    //we dont have a user DB or table , if you have , check the passed through username / pasword against ways stored in the databse 
        //    //for demo purpos , we assume the credentials are valid 

        //    //return a new MovieInfoUser ( values would normally come from your user DB/table)

        //    return new MovieInfoUser(1, userName ?? "", "nikhil", "singh");
         
        //}
    }
}
