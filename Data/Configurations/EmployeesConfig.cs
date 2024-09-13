using Employee.Data.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Data.Configurations
{

    public class EmployeesConfig : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.HasKey(e => e.Emp_Id);

            builder.HasOne(e => e.Department)
                .WithMany(e => e.employees)
                .HasForeignKey(e => e.FDep_Id).IsRequired();
      
            builder.Property(e => e.Name)
                .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();

            builder.Property(e => e.Salary);
            builder.Property(e => e.IsDeleted);
            builder.Property(e => e.BirthDate).IsRequired();
            builder.ToTable("Employees");
        }

    }
}
