using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : ControllerBase
   {
      private readonly IAuthRepo _repo;
      private readonly IConfiguration _config;
      public AuthController(IAuthRepo repo, IConfiguration config)
      {
         _config = config;
         _repo = repo;
      }

      [HttpPost("register")]
      public async Task<IActionResult> Register(UserToRegisterDto userDto)
      {
         //validate 
         userDto.UserName = userDto.UserName.ToLower();
         if (await _repo.UserExists(userDto.UserName))
            return BadRequest(userDto.UserName + " already exist");

         //create user
         var userToCreate = new User
         {
            UserName = userDto.UserName
         };

         var createUser = await _repo.Register(userToCreate, userDto.password);
         return StatusCode(201);
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login(UserToLoginDto userDto)
      {
         var userFromRepo = await _repo.Login(userDto.UserName.ToLower(), userDto.password);
         if (userFromRepo == null)
            return Unauthorized();

         // create token
         var claims = new[]
         {
            new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, userFromRepo.UserName)
        };

         var key = new SymmetricSecurityKey(Encoding.UTF8
         .GetBytes(_config.GetSection("AppSettings:Token").Value));

         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

         var tokenDescripor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
         };
         
         var tokenHandler = new JwtSecurityTokenHandler();

         var token = tokenHandler.CreateToken(tokenDescripor);

         return Ok(new {
            token = tokenHandler.WriteToken(token)
         });
      }

      
   }
}