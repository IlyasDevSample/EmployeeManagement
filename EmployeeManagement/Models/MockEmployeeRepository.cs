namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        List<Employee> _employees;
        

        public MockEmployeeRepository()
        {
            _employees = new List<Employee>() 
            {
                new Employee() {Id = 1, Name ="iliass",Email="elaisis@gm.com",Departement = "IT"},
                new Employee() {Id = 2, Name ="ilyas",Email="elaisis2@gm.com",Departement = "IT"},
                new Employee() {Id = 3, Name ="elaissi",Email="elaisis3@gm.com",Departement = "IT"}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employees.Max(x => x.Id + 1);
            _employees.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
           return _employees;
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employees.FirstOrDefault(x => x.Id == employeeChanges.Id);
            if (employee != null)
            {
               employee.Name = employeeChanges.Name;
               employee.Email = employeeChanges.Email;
               employee.Departement = employeeChanges.Departement;
            }
            
            return employee;
        }
    }
}
