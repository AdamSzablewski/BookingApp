using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BookingApp;

public class SecurityService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _symmetricSecurityKey;
    public SecurityService(IConfiguration configuration)
    {
        _configuration = configuration;
        _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
    }

    public string CreateToken(Person user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("Email", user.Email),
            new Claim("UserId", user.Id),
        };
        var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = signingCredentials,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public bool IsUser(HttpContext http, string userId)
    {
        string token = http.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        string userIdFromToken = GetAuthorizationClaims(token);
        Console.WriteLine("From token --- "+userIdFromToken+"   from dt --- "+userId);
        if(!userIdFromToken.Equals(userId))
        {
            throw new NotAuthorizedException();
        }
        return true;

    }
    public string GetAuthorizationClaims(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var parsedJwt = handler.ReadJwtToken(token);
        var userIdClaim = parsedJwt.Claims.FirstOrDefault(c => c.Type == "UserId");
        if(userIdClaim != null)
        {
            return userIdClaim.Value;
        }
        throw new NotAuthorizedException();
    }

}
