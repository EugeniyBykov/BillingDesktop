namespace Server.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataContex : DbContext
    {
        public DataContex()
            : base("name=DataContex")
        {
        }

        public DbSet<Client> Clients { get; set; }    
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
    }

}