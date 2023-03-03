using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreJwtAuthTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    private string? GetAudience()
    {
        return _configuration.GetValue<string>("Jwt:Audience") ?? "http://localhost:5000";
    }

    private string? GetIssuer()
    {
        return _configuration.GetValue<string>("Jwt:Issuer") ?? "http://localhost:5000";
    }

    private string? GetSigninKey()
    {
        var signinKey = _configuration.GetValue<string>("Jwt:Key") ?? "XofPVeLDNKVbzCVRpvSb";
        return signinKey;
    }

    [HttpGet]
    public string Get(string userName,string email, string password)
    {
        var claims = new[]{
            new Claim(ClaimTypes.Name,userName),
            new Claim(JwtRegisteredClaimNames.Email,email)
        };
        var signinKey = GetSigninKey();
        var issuer = GetIssuer();
        var audience = GetAudience();

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddDays(15),
            notBefore: DateTime.Now,
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }

    [HttpGet("ValidateToken")]
    public bool ValidateToken(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetSigninKey()));
        try
        {
            JwtSecurityTokenHandler handler = new ();
            handler.ValidateToken(token, new TokenValidationParameters(){
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = true,
                ValidateAudience = false,
                //ValidAudience = "" //if ValidateAudience = true
                ValidateIssuer = false,
                //ValidIssuer = "" //if ValidateIssuer = true
            },out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            //var claims = jwtToken.Claims.ToList(); //to get claims
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}
