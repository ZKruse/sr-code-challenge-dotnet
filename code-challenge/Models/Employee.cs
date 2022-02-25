using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Employee
    {
        public String EmployeeId { get; set; } = Guid.NewGuid().ToString();
        public String FirstName { get; set; } = String.Empty;
        public String LastName { get; set; } = String.Empty;
        public String Position { get; set; } = String.Empty;
        public String Department { get; set; } = String.Empty;
        public List<Employee> DirectReports { get; set; } = new List<Employee>();

        //Employee constructor(String employeeId, String firstName, String lastName, String position, String department) {
        //    return new Employee()
        //    {
        //        EmployeeId = employeeId,
        //        FirstName = firstName,
        //        LastName = lastName,
        //        Position = position,
        //        Department = department
        //    };
        //}
    }
}
