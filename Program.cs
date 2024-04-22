using System.Text;
using BookingApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.Data;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("BookingApp");


// builder.Services.AddAuthentication(options => {
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// })
//     .AddJwtBearer(options => {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidIssuer = builder.Configuration["JWT:Issuer"],
//             ValidAudience = builder.Configuration["JWT:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
//             )
//         };
//     });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
//builder.Services.AddMySql<BookingAppContext>(conString);
builder.Services.AddDbContext<BookingAppContext>(options =>{
    options.UseMySql(conString, new MySqlServerVersion(new Version(8, 0, 23)));
});
// }, ServiceLifetime.Transient);
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<FacilityService>();
builder.Services.AddScoped<FacilityRepository>();
builder.Services.AddScoped<SecurityService>();
builder.Services.AddScoped<CustomerService>();

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
builder.Services.AddScoped<MessageRepository>();
builder.Services.AddScoped<ConversationRepository>();
builder.Services.AddScoped<ConversationPersonRepository>();
builder.Services.AddScoped<AdressRepository>();




//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmploymentService>();
builder.Services.AddScoped<MessagingService>();
builder.Services.AddScoped<ConversationService>();




builder.Services.AddIdentity<Person, IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 4;

}).AddEntityFrameworkStores<BookingAppContext>(); 

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultScheme = 
    options.DefaultSignInScheme = 
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
});


var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
