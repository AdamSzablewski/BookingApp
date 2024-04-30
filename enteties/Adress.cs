namespace BookingApp;

public class Adress
{
    
    public long Id {get; set;}
    public required string Country {get; set;}
    public required string City {get; set;}
    public string? Street {get; set;}
    public string? HouseNumber {get; set;}

}
