


namespace BookingApp;

public class FacilityDto
{
    public long Id {get; set;}
    public required string Name {get; set;}
    public int Points{get; set;}
    public required List<ServiceDto> Services {get; set;} = [];
}
