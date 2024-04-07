namespace BookingApp;

public class Owner
{
  public long Id {get; set;} 
  public long? FacilityId {get; set;}  
  public List<Facility> Facilities {get; set;}
  public long? UserId {get; set;}
  public Person? User {get; set;}
  public List<EmploymentRequest> ActiveRequests {get; set;}

}
