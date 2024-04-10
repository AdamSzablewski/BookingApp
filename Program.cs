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
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<FacilityService>();
builder.Services.AddScoped<FacilityRepository>();
// builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
// builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
// builder.Services.AddScoped<IEmploymentRepository, EmploymentRepository>();
// builder.Services.AddScoped<IEmploymentRequestRepository, EmploymentRequestRepository>();
// builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<AppointmentRepository>();
builder.Services.AddScoped<ServiceRepository>();
builder.Services.AddScoped<EmploymentRepository>();
builder.Services.AddScoped<EmploymentRequestRepository>();
builder.Services.AddScoped<EmployeeRepository>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmploymentService>();



var app = builder.Build();


// app.MapPersonEndpoints();
app.MigrateDB();
app.MapControllers();
app.Run();
