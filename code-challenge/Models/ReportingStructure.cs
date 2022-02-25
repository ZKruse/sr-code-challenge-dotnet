using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class ReportingStructure
    {
        public Employee employee { get; set; } = new Employee();
        
        public int NumberOfReports
        {
            get => getNumberOfReports(employee);
        }

        private int getNumberOfReports(Employee employee)
        {
            int childReportCount = 0;
            if(employee.DirectReports != null)
            {
                childReportCount = employee.DirectReports.Count;
                foreach (var report in employee.DirectReports)
                {
                    childReportCount += getNumberOfReports(report);
                }
            }
            return childReportCount;
        }
    }
}
