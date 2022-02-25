using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public List<Employee> GetEmployees()
        {
            // ToList forces an evaluation of the DBContext elements so it properly populates the child field DirectReport
            return _employeeContext.Employees.ToList();
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee? GetById(string id)
        {
            // Note the use of .ToList() to force loading of the sub-property DirectReports.
            // There's something eluding me with Entity Framework's configuration where it's not populating child fields,
            // just returning their defaults.
            // There's a global lazy loading flag but there were some library version conflicts preventing me from easily enabling it. 
            // There's a call for this, .Include, but that doesn't recurse
            // There's some other methods but it gets insane real quick and not worth it for a coding exercise
            // https://stackoverflow.com/questions/1308158/how-does-entity-framework-work-with-recursive-hierarchies-include-seems-not-t
            var employee = _employeeContext.Employees.ToList().SingleOrDefault(e => e.EmployeeId == id);
            return employee;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
