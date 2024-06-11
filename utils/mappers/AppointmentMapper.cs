using System.Runtime.CompilerServices;
using BookingApp;

public static class AppointmentMapper
{
    public static AppointmentDto MapToDto(this Appointment appointment)
    {
        return new()
        {
            ServiceName = appointment.Service.Name,
            Date = $"{appointment.StartTime.Year}/{appointment.StartTime.Month}/{appointment.StartTime.Day}",
            Time = $"{appointment.StartTime.Hour}:{appointment.StartTime.Minute}:{appointment.StartTime.Second}",
            Price = appointment.Service.Price,
            Adress = appointment.Service.Facility.Adress,
            ImgUrl =appointment.Service.Facility.ImgUrl
        };
    }
    public static List<AppointmentDto> MapToDto(this ICollection<Appointment> appointments)
    {
        return appointments.Select(MapToDto).ToList();
    }
}