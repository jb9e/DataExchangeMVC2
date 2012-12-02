using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DataExchangeMVC.Models
{
    public class DataExchangeDBContext: DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}