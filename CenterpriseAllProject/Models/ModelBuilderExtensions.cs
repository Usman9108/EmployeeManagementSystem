using Microsoft.EntityFrameworkCore;

namespace CenterpriseAllProject.Models
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                Name = "Mary",
                Email = "mary@pragimTech.com",
                Department = Dept.IT
            },
            new Employee
            {
                Id = 2,
                Name = "John",
                Email = "John@pragimTech.com",
                Department = Dept.IT
            });
        }
    }
}
