using challenge.Models;
using System;
using System.Collections.Generic;

namespace challenge.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Employee? GetById(String id);
        Employee? Create(Employee employee);
        ReportingStructure? GetReportingStructure(String id);
        Employee? Replace(Employee originalEmployee, Employee newEmployee);
    }
}
