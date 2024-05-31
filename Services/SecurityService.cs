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
    private readonly IFacilityRepository _facilityRepository;
    public SecurityService(IConfiguration configuration, IFacilityRepository facilityRepository)
    {
        _configuration = configuration;
        _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        _facilityRepository = facilityRepository;
    }

    public bool OwnsResource(HttpContext http, IUserResource userResource)
    {
        string userIdFromResource = userResource.GetUserId();
        if(userIdFromResource == null)
        {
            return false;
        }
        string userIdFromRequest = GetUserIdFromRequest(http);
        return userIdFromRequest.Equals(userIdFromResource);
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
        if(!userIdFromToken.Equals(userId))
        {
            throw new NotAuthorizedException();
        }
        return true;

    }

    public async Task<bool> IsOwner(HttpContext http, long facilityId, string userId)
    {   
        if(!IsUser(http, userId))
        {
            throw new NotAuthorizedException();
        }
        Facility facility = await _facilityRepository.GetByIdAsync(facilityId) ?? throw new FacilityNotFoundException();
        Owner owner = facility.Owner;
        return owner.UserId.Equals(userId);
    }
    public async Task<bool> IsOwner(HttpContext http, long facilityId)
    {   
        string userId = GetUserIdFromRequest(http);
        if(userId == null)
        {
            return false;
        }
        Facility facility = await _facilityRepository.GetByIdAsync(facilityId)
                            ?? throw new FacilityNotFoundException();
        Owner owner = facility.Owner;
        return owner.UserId.Equals(userId);
    }
    public string GetUserIdFromRequest(HttpContext http)
    {
        string token = http.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        return GetAuthorizationClaims(token);
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

    public bool IsMemberOfConversation(HttpContext httpContext, Conversation conversation)
    {
        string userId = GetUserIdFromRequest(httpContext);
        if(userId == null)
        {
            return false;
        }
        return conversation.Participants.Any(participant => participant.PersonId.Equals(userId));
    }

    internal bool OwnsResource(Message message)
    {
        throw new NotImplementedException();
    }
}
