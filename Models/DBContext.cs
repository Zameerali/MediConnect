using Microsoft.EntityFrameworkCore;

namespace MediConnectwithAuthentication.Models

{
    public class DBContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MediConnect;Integrated Security=True");
        }
    }
}
