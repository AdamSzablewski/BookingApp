﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookingApp;
[Route("authenticate")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<Person> _userManager;
    private readonly PersonService _personService;
    private readonly SecurityService _tokenService;
    private readonly SignInManager<Person> _signInManager;
    private readonly DbContext _dbContext;

    public AccountController(UserManager<Person> userManager, PersonService personService, SecurityService tokenService, SignInManager<Person> signInManager, DbContext dbContext)
    {
        _userManager = userManager;
        _personService = personService;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _dbContext = dbContext;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if(!ModelState.IsValid){ return BadRequest(ModelState);};

        var user = _userManager.Users
        .Include(u => u.Customer)
        .Include(u => u.Owner)
        .Include(u => u.Employee)
        .FirstOrDefault(u => u.UserName.Equals(loginDto.Username));
        if(user == null) return Unauthorized();
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if(!result.Succeeded) return Unauthorized("Username not found or invalid password");
        string role = "";
        if(user.Customer != null){
            role = "Customer";
        }
        else if( user.Employee != null){
            role = "Employee";
        }
        else if( user.Owner != null){
            role = "Owner";
        }
        JWTDto jwtDto = new()
            {
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                UserId = user.Id,
                Role = role
            };
        return Ok(jwtDto);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
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
            Console.WriteLine(user);

            var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
            Console.WriteLine(createdUser);

            Adress adress = new(){
                Country = registerDto.Country,
                City = registerDto.City
            };
            await _dbContext.Adresses.AddAsync(adress);
            user.Adress = adress;
            await _dbContext.SaveChangesAsync();
            if(createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if(roleResult.Succeeded)
                {
                    JWTDto jwtDto = new(){
                        Token = _tokenService.CreateToken(user),
                        Email = user.Email,
                        UserId = user.Id,
                        Role = null
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
