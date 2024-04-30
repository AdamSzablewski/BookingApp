﻿using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public class RegisterDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public required string Email {get; set;}
    [Required(ErrorMessage = "Password is required.")]
    public required string Password {get; set;}
    [Required(ErrorMessage = "PhoneNumber is required.")]
    public required string PhoneNumber {get; set;}
    [Required(ErrorMessage = "First name is required.")]
    public required string FirstName {get; set;}
    [Required(ErrorMessage = "Last name is required.")]
    public required string LastName {get; set;}

    [Required(ErrorMessage = "Country is required.")]
    public required string Country {get; set;}
    
    [Required (ErrorMessage = "City is required.")]
    public required string City {get; set;}


}
