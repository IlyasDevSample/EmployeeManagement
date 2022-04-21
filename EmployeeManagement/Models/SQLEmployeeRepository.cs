namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger<SQLEmployeeRepository> logger;
        private AppDbContext Context { get; }

        public SQLEmployeeRepository(AppDbContext context,ILogger<SQLEmployeeRepository> logger)
        {
            Context = context;
            this.logger = logger;
        }
        

        public Employee Add(Employee employee)
        {
            Context.Employees.Add(employee);
            Context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = Context.Employees.Find(id);
            if (employee != null)
            {
                Context.Employees.Remove(employee);
                Context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return Context.Employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");


            return Context.Employees.Find(id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = Context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return employeeChanges;
        }
    }
}
