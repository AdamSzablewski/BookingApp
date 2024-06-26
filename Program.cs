using System.Text;
using BookingApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.Data;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("BookingApp");



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddDbContext<BookingApp.DbContext>(options =>{
    options.UseMySql(conString, new MySqlServerVersion(new Version(8, 0, 23)));
});
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<FacilityService>();
builder.Services.AddScoped<FacilityRepository>();
builder.Services.AddScoped<SecurityService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<FeedService>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IEmploymentRequestRepository, EmploymentRequestRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<ConversationPersonRepository>();
builder.Services.AddScoped<IAdressRepository, AdressRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();


builder.Services.AddScoped<ReviewService>();
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

}).AddEntityFrameworkStores<BookingApp.DbContext>(); 


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORS",
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

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

app.UseCors("CORS");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
