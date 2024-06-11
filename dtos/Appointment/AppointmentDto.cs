using BookingApp;

public class AppointmentDto()
{
    public long Id {get; set;}
    public string ServiceName {get; set;}
    public string Date {get; set;}
    public string Time {get; set;}
    public decimal Price {get; set;}
    public Adress Adress {get; set;}
    public string ImgUrl {get; set;}
    public EmployeeDto employee {get; set;}
}