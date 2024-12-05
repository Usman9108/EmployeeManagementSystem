
namespace CenterpriseAllProject.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context; 

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee= context.Employees.Find(id)!;
            if(employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee!;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id)!;
        }

        public Employee Update(Employee employee)
        {
            var emp = context.Employees.Attach(employee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employee;
        }
    }
}
