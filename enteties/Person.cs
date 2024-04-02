using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public class Person
{
    public long Id {get; set;}
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public required string Email {get; set;}
    public string PhoneNumber {get; set;}
    public string Password {get; set;}
    public long? OwnerId {get; set;}
    public Owner? Owner {get; set;}
    public long? CustomerId {get; set;}
    public Customer? Customer {get; set;}
    public long? EmployeeId {get; set;}
    public Employee? Employee {get; set;}
}
