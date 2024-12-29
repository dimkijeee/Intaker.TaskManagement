using Microsoft.EntityFrameworkCore;

namespace Intaker.TaskManagement.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Models.Task> Tasks => Set<Models.Task>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\taskmanagement;Database=taskmanagement;Trusted_Connection=True;");
        }
    }
}
