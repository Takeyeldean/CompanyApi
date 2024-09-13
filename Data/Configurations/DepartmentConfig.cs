using Employee.Data.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Data.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.Dep_Id);
            builder.Property(e=>e.Dep_Id).IsRequired().ValueGeneratedNever();


            builder.Property(e => e.DepName)
              .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();
         

            //builder.Property(e => e.Employee);
            builder.ToTable("Departments");
        }
    }
}
