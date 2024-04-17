using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookingApp;
[Route("authenticate")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<Person> _userManager;
    private readonly PersonService _personService;
    private readonly TokenService _tokenService;
    private readonly SignInManager<Person> _signInManager;

    public AccountController(UserManager<Person> userManager, PersonService personService, TokenService tokenService, SignInManager<Person> signInManager)
    {
        _userManager = userManager;
        _personService = personService;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.UserName.Equals(loginDto.Username));
        if(user == null) return Unauthorized();
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if(!result.Succeeded) return Unauthorized("Username not found or invalid password");

        JWTDto jwtDto = new()
            {
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                UserId = user.Id
            };
        return Ok(jwtDto);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PersonCreateDto registerDto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            Person user = new()
            {
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
            Console.WriteLine("User adddddddeeeeedddd");
            if(createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if(roleResult.Succeeded)
                {
                    JWTDto jwtDto = new(){
                        Token = _tokenService.CreateToken(user),
                        Email = user.Email,
                        UserId = user.Id
                    };
                    return Ok(jwtDto);
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
               return StatusCode(500, createdUser.Errors);
            }
        }
        catch(Exception e)
        {
            Console.Write(e.StackTrace);
            return StatusCode(500, e);
        }
    }
}
