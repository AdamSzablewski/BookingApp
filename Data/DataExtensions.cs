using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public static class DataExtensions
{
    public static void MigrateDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookingAppContext>();
        dbContext.Database.Migrate();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();


    }

}
