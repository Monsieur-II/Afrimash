using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Afrimash.Api.Services;
using Afrimash.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Afrimash.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(string email)
    {
        if (email.EndsWith("@afrimash.com"))
        {
            var staffToken = new JwtSecurityToken
            (
                CommonConstants.Issuer,
                CommonConstants.Audience,
                claims:
                [
                    new Claim(ClaimTypes.Role, "Staff")
                ],
            expires: DateTime.UtcNow.AddHours(48),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CommonConstants.SigningKey)),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenKey = new JwtSecurityTokenHandler().WriteToken(staffToken);
           

            var staffResponse =  tokenKey.ToApiResponse("succcess", StatusCodes.Status200OK);

            return StatusCode(StatusCodes.Status200OK, staffResponse);
        }
        
        var jwtToken = new JwtSecurityToken
        (
            CommonConstants.Issuer,
            CommonConstants.Audience,
            claims:
            [
                new Claim(ClaimTypes.Role, "Customer")
            ],
            expires: DateTime.UtcNow.AddHours(48),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CommonConstants.SigningKey)),
                SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
           

        var response =  token.ToApiResponse("succcess", StatusCodes.Status200OK);

        return StatusCode(StatusCodes.Status200OK, response);

        
    }
}
