
using Microsoft.EntityFrameworkCore;


namespace webike.Models
{

    public class WebikeContext : DbContext
    {
        public DbSet<Cyclist> Cyclists {get;set;}
        public DbSet<Contact> Contacts {get;set;}
        public DbSet<Admin> Admins{get;set;}
        public DbSet<Event> Events {get;set;}
        public DbSet<EventActivity> EventActivities {get;set;}
        public DbSet<Excercise> Excercises {get;set;}
        public DbSet<Rating> Ratings {get;set;}
        public DbSet<Route> Routes {get;set;}
        public DbSet<Workout> Workouts {get;set;}
        
        public WebikeContext(DbContextOptions options) : base(options)
        {
        }

    }



}