using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class DbContext : IdentityDbContext<Person>
{
    public DbContext(DbContextOptions options) : base(options)
    {

    }
    public DbContext()
    {
        
    }
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Adress> Adresses => Set<Adress>();
    public DbSet<EmploymentRequest> EmploymentRequests => Set<EmploymentRequest>();
    public DbSet<ConversationPerson> ConversationPerson => Set<ConversationPerson>();
    public DbSet<MessagePerson> MessagePerson => Set<MessagePerson>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Conversation> Conversations => Set<Conversation>();





    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    List<IdentityRole> roles =
    [
        new IdentityRole{
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
         new IdentityRole{
            Name = "User",
            NormalizedName = "USER"
        }
    ];
    modelBuilder.Entity<IdentityRole>().HasData(roles);
    modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(p => p.Employee)
                .HasForeignKey<Employee>(e => e.UserId);
    modelBuilder.Entity<Person>()
                .HasOne(e => e.Employee)
                .WithOne(p => p.User)
                .HasForeignKey<Person>(e => e.EmployeeId);

    
    modelBuilder.Entity<Owner>()
                .HasOne(o => o.User)
                .WithOne(p => p.Owner)
                .HasForeignKey<Owner>(o => o.UserId);
    modelBuilder.Entity<Person>()
                .HasOne(e => e.Owner)
                .WithOne(p => p.User)
                .HasForeignKey<Person>(e => e.OwnerId);
    // modelBuilder.Entity<Facility>()
    //             .HasOne(f => f.Owner)
    //             .WithOne(o => o.Facility)
    //             .HasForeignKey<Owner>(o => o.FacilityId);
    // modelBuilder.Entity<Owner>()
    //             .HasOne(o => o.Facility)
    //             .WithOne(p => p.Owner)
    //             .HasForeignKey<Owner>(o => o.FacilityId);

    modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(p => p.Customer)
                .HasForeignKey<Customer>(c => c.UserId);
    
    modelBuilder.Entity<Person>()
                .HasOne(e => e.Customer)
                .WithOne(p => p.User)
                .HasForeignKey<Person>(e => e.CustomerId);

// "BookingApp":  "Data Source=BookingApp.db"
}
}
