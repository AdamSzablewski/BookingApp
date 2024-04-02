﻿using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class BookingAppContext(DbContextOptions<BookingAppContext> options) 
: DbContext(options)
{
    public DbSet<Person> persons => Set<Person>();
    public DbSet<Customer> customers => Set<Customer>();
    public DbSet<Owner> owners => Set<Owner>();
    public DbSet<Employee> employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(p => p.Employee)
                .HasForeignKey<Employee>(e => e.UserId);

    modelBuilder.Entity<Owner>()
                .HasOne(o => o.User)
                .WithOne(p => p.Owner)
                .HasForeignKey<Owner>(o => o.UserId);
    modelBuilder.Entity<Owner>()
                .HasOne(o => o.Facility)
                .WithOne(p => p.Owner)
                .HasForeignKey<Owner>(o => o.FacilityId);

    modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(p => p.Customer)
                .HasForeignKey<Customer>(c => c.UserId);

    modelBuilder.Entity<Facility>()
                .HasOne(f => f.Owner)
                .WithOne(o => o.Facility)
                .HasForeignKey<Owner>(o => o.FacilityId);
}
}
