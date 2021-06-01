using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SamayaElectronics.Api.Configuration;
using SamayaElectronics.Api.DTO.Requests;
using SamayaElectronics.Api.DTO.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagmentController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _optionMonitor;

        public AuthManagmentController(UserManager<IdentityUser> userManager,
                                       IOptionsMonitor<JwtConfig> optionMonitor)
        {
            this._userManager = userManager;
            this._optionMonitor = optionMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] UserRegestrationDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser is not null)
                {
                    return BadRequest(new RegistrationResponse
                    {
                        Errors = new List<string> {
                    "Email Already In Use"
                    },
                        Success = false
                    });
                }
                var newUser = new IdentityUser
                {
                    Email = user.Email,
                    UserName = user.UserName
                };
                var createNewUser = await _userManager.CreateAsync(newUser, user.Password);
                if (createNewUser.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);
                    return Ok(new RegistrationResponse
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = createNewUser.Errors.Select(X => X.Description).ToList(),
                        Success = false
                    });
                }
            }
            return BadRequest(new RegistrationResponse
            {
                Errors = new List<string> {
                    "Invalid PayLoad"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser is null)
                {
                    return BadRequest(new RegistrationResponse
                    {
                        Errors = new List<string> {
                    "Invalid Login Request"
                    },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponse
                    {
                        Errors = new List<string> {
                    "Invalid Login Request"
                    },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);
                return Ok(new RegistrationResponse
                {
                    Success = true,
                    Token = jwtToken
                });

            }
            return BadRequest(new RegistrationResponse
            {
                Errors = new List<string> {
                    "Invalid PayLoad"
                },
                Success = false
            });
        }



        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_optionMonitor.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                                                             , SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
