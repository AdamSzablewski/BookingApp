﻿using System.Collections;

namespace BookingApp;

public class Facility
{
    public Facility(){
        Services = [];
        Employees = [];
    }
    public long Id {get; set;}
    public required string Name {get; set;}
    public required Adress Adress {get; set;}
    public List<Service> Services {get; set;}
    public List<Employee> Employees {get; set;}
    public long OwnerId {get; set;}
    public required Owner Owner {get; set;}
    public TimeOnly StartTime {get; set;}
    public TimeOnly EndTime {get; set;}

}