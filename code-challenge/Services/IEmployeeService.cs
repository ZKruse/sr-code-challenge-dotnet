using challenge.Models;
using System;
using System.Collections.Generic;

namespace challenge.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Employee GetById(String id);
        Employee Create(Employee employee);
        Employee Replace(Employee originalEmployee, Employee newEmployee);
    }
}
