namespace BookingApp;

public static class FacilityMapper
{
    public static FacilityDto MapToDto(this Facility facility)
    {
        return new FacilityDto()
        {
            Id = facility.Id,
            Name = facility.Name,
            Services = facility.Services.Select(s => s.MapToDto()).ToList(),
            Adress = facility.Adress,
            Points = PointsUtil.GetScore(facility.Reviews),
            Category = facility.Category,
            ReviewAmmount = facility.Reviews.Count,
            ImgUrl = facility.ImgUrl
        };
    }
    public static List<FacilityDto>? MapToDto(this List<Facility> facilities)
    {
        if(facilities == null) return null;
        return facilities.Select(MapToDto).ToList();
    }
}
