using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtantions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "Mary", Departement = "IT", Email = "mary@gmail.com",PhotoPath="ff" },
                new Employee() { Id = 2, Name = "John", Departement = "HR", Email = "john@gmail.com", PhotoPath = "dd" },
                new Employee() { Id = 4, Name = "John", Departement = "HR", Email = "john@gmail.com", PhotoPath = "" }
                );
        }
    }
}
