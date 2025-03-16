using EmployeeManagmentSystem.Models;

namespace EmployeeManagmentSystem.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee? GetEmployeeById(int id);
        Employee CreateEmployee(Employee employee);
        Employee? UpdateEmployee(int id, Employee employee);
        bool DeleteEmployee(int id);
    }
}
