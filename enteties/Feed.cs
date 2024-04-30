namespace BookingApp;

public class Feed
{
    public required string UserId {get; set;}
    public List<ServiceDto> Services {get; set;} = [];
    public int page = 0;

}
