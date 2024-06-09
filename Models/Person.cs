using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookingApp;

public class Person : IdentityUser
{
   
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public string? ProfilePicture {get; set;}
    public long? OwnerId {get; set;}
    public Owner? Owner {get; set;}
    public long? CustomerId {get; set;}
    public Customer? Customer {get; set;}
    public long? EmployeeId {get; set;}
    public Employee? Employee {get; set;}
    public long? AdressId {get; set;}
    public Adress? Adress {get; set;}
    public override string ToString()
    {
        return $"FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, PhoneNumber: {PhoneNumber}";
    }
}
