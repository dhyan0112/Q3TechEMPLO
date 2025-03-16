using EmployeeManagmentSystem.Data;
using EmployeeManagmentSystem.Models;

namespace EmployeeManagmentSystem.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context) => _context = context;



        public IEnumerable<Employee> GetAllEmployees()=> _context.Employees.ToList();

        public Employee? GetEmployeeById(int id) => _context.Employees.Find(id);




        public Employee CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee? UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = _context.Employees.Find(id);
            if (existingEmployee == null) return null;

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            existingEmployee.Position = employee.Position;
            existingEmployee.Salary = employee.Salary;

            _context.SaveChanges();
            return existingEmployee;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }

    }
}
