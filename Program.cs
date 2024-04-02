using BookingApp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("BookingApp");

builder.Services.AddSqlite<BookingAppContext>(conString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddDbContext<BookingAppContext>(options =>{
    options.UseSqlite(conString);
});
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<FacilityService>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();

var app = builder.Build();


// app.MapPersonEndpoints();
app.MigrateDB();
app.MapControllers();
app.Run();
