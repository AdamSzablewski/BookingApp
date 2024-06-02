using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class Startup
{
 public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddCors(options =>
            // {
            //     options.AddPolicy("AllowLocalhost3000",
            //         builder => builder.WithOrigins("http://localhost:3000")
            //                         .AllowAnyMethod()
            //                         .AllowAnyHeader());
            // });
            // services.AddControllers();
            services.AddScoped<PersonService>();
        }
}
