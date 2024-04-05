namespace BookingApp;

public class EmploymentRequest
{
    public long Id {get; set;}
    public long SenderId {get; set;}
    public required Owner Sender {get; set;}
    public long ReceiverId {get; set;}
    public required Employee Receiver {get; set;}
    
}
