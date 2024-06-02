


namespace BookingApp;

public class FacilityDto
{
    public long Id {get; set;}
    public required string Name {get; set;}
    public required Adress Adress {get; set;}
    public required int Points {get; set;}
    public required int ReviewAmmount {get; set;}
    public required List<ServiceDto> Services {get; set;} = [];
}
