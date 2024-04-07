namespace BookingApp;

public class EmploymentRequest
{
    public long Id {get; set;}
    public long SenderId {get; set;}
    public required Owner Sender {get; set;}
    public long ReceiverId {get; set;}
    public required Employee Receiver {get; set;}
    public long FacilityId {get; set;}
    public required Facility Facility {get; set;}
    public bool Closed {get; set;}
    public bool Decision {get; set;}
    
}
