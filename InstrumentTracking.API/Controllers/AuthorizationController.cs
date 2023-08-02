using InstrumentsTracking.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using InstrumentsTracking.Exception;
using InstrumentTracking.Services;
using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Infrastructure;

namespace InstrumentsTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterDTO dto)
        {
            var existUser = _userService.GetByLogin(dto.Login);

            if (existUser == null)
            {
                var user = new User
                {
                    Login = dto.Login,
                    EngineerID = dto.EngineerID,
                    Role = dto.Role,
                    Password = dto.Password
                };

                return Created("Success", _userService.Create(user));
            }

            else
            {
                return new CustomException(System.Net.HttpStatusCode.Conflict, "Пользователь с таким именем уже существует");
            }
        }

        [HttpPost("/login")]
        public IActionResult Token(LoginDTO user)
        {
            var login = user.Login;
            var password = user.Password;

            var identity = GetIdentity(login, password);

            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                engineerID = identity.Name
            };

            return new JsonResult(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var person = _userService.GetByLogin(username);

            if (person != null)
            {
                if (person.Password != password)
                {
                    return null;
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.EngineerID.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
