using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookingApp;

public class Person : IdentityUser
{
    //public override long Id {get; set;}
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    //public required string Email {get; set;}
    //public string PhoneNumber {get; set;}
    //public string Password {get; set;}
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
        return $"FirstName: {FirstName}, LastName: {LastName}, Employee: {Employee}";
    }
}
