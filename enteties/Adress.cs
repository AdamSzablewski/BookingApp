namespace BookingApp;

public class Adress
{
    public Adress(string country, string city, string street, string houseNumber){
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }
    public long Id {get; set;}
    public string Country {get; set;}
    public string City {get; set;}
    public string Street {get; set;}
    public string HouseNumber {get; set;}

}
