using Employee.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        public override int SaveChanges()
        {
            HandleSoftDeleteEmployee();
            HandleSoftDeleteDepartment();
            return base.SaveChanges();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionstring = config.GetSection("constr").Value;
            optionsBuilder.UseSqlServer(connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //   modelBuilder.ApplyConfiguration(new CourseConfiguration()); Not best practice
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }


        private void HandleSoftDeleteEmployee()
        {
            // Intercept entities marked for deletion
            var deletedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is Employees)
                .ToList();

            foreach (var entityEntry in deletedEntities)
            {
                // Instead of deleting, mark IsDeleted as true
                entityEntry.State = EntityState.Modified;
                ((Employees)entityEntry.Entity).IsDeleted = true;
            }
        }
        private void HandleSoftDeleteDepartment()
        {
            // Intercept entities marked for deletion
            var deletedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is Department)
                .ToList();

            foreach (var entityEntry in deletedEntities)
            {
                // Instead of deleting, mark IsDeleted as true
                entityEntry.State = EntityState.Modified;
                ((Department)entityEntry.Entity).IsDeleted = true;
            }
        }

    }
}
