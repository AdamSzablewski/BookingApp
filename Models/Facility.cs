using System.Collections;

namespace BookingApp;

public class Facility : IUserResource
{
    public Facility(){
        Services = [];
        Employees = [];
    }
    public long Id {get; set;}
    public required string Name {get; set;}
    public required Adress Adress {get; set;}
    public List<Service> Services {get; set;}
    public List<Employee> Employees {get; set;}
    public long OwnerId {get; set;}
    public required Owner Owner {get; set;}
    public TimeOnly StartTime {get; set;}
    public TimeOnly EndTime {get; set;}
    public List<Review> Reviews {get; set;}
    public Dictionary<int, int> Points {get; set;} = new Dictionary<int, int>
    {
        {1, 0},
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},
    };

    public string? GetUserId()
    {
        return Owner?.UserId;
    }
}
