using System.ComponentModel.DataAnnotations;

namespace BookingApp;
public class FacilityPoint
{
    public int Id { get; set; }
    public int Key { get; set; }
    public int Value { get; set; }
    public int FacilityId { get; set; }
    public Facility Facility { get; set; }
}
