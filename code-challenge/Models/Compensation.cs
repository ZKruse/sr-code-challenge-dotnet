using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        //A Compensation has the following fields: employee, salary, and effectiveDate
        public Employee employee { get; set; } = new Employee();

        public int Salary { get; set; }
        // DateOnly doesn't serialize/deserialize without custom converters, skipping due to time constraints
        public DateTime EffectiveDate { get; set; }
    }
}
