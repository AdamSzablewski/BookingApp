namespace BookingApp;

public class Owner
{
  public Owner(){
    Facilities = [];
    ActiveRequests = [];
  }
  public long Id {get; set;} 
  public List<Facility> Facilities {get; set;}
  public long? UserId {get; set;}
  public Person? User {get; set;}
  public List<EmploymentRequest> ActiveRequests {get; set;}

}
