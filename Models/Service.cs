using System.Text;

namespace BookingApp;

public class Service : IUserResource
{
    public Service() {
        Employees = [];
    }
    public long Id {get; set;}
    public required string Name {get; set;}
    public required decimal Price {get; set;}
    public List<Employee> Employees {get; set;}
    public long FacilityId {get; set;}
    public required Facility Facility {get; set;}
    public required TimeSpan Length {get; set;}

    public string GetUserId()
    {
        return Facility.Owner.UserId;
    }

    public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Service ID: {Id}");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Price: {Price}");
            sb.AppendLine("Employees:");
            foreach (var employee in Employees)
            {
                sb.AppendLine($"- Employee: {employee.Id}");
                sb.AppendLine($"  Appointments:");
                foreach (var appointment in employee.Appointments)
                {
                    sb.AppendLine($"    - {appointment.StartTime} - {appointment.EndTime}");
                }
            }
            sb.AppendLine($"Facility ID: {FacilityId}");
            sb.AppendLine($"Length: {Length}");
            return sb.ToString();
        }
    }


