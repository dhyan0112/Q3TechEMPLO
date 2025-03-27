﻿using Assignment_Q3_2.DTOs;
using Assignment_Q3_2.Models;

namespace Assignment_Q3_2.Services
{
   
        public interface IEmployeeService
        {
            Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
            Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
            Task<EmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO);
            Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDTO updateEmployeeDTO);
            Task<bool> DeleteEmployeeAsync(int id);
            //Task<Employee> UpdateEmployeeAsync(Employee employee);
    }
    
}
