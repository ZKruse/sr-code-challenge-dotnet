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
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }

        public List<Compensation> GetCompensations()
        {
            // ToList forces an evaluation of the DBContext elements so it properly populates the child field DirectReport
            return _compensationContext.Compensations.ToList();
        }

        public Compensation Add(Compensation compensation)
        {
            //compensation.EmployeeId = Guid.NewGuid().ToString();
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation? GetByEmployeeId(string id)
        {
            // Note the use of .ToList() to force loading of the sub-property DirectReports.
            // There's something eluding me with Entity Framework's configuration where it's not populating child fields,
            // just returning their defaults.
            // There's a global lazy loading flag but there were some library version conflicts preventing me from easily enabling it. 
            // There's a call for this, .Include, but that doesn't recurse
            // There's some other methods but it gets insane real quick and not worth it for a coding exercise
            // https://stackoverflow.com/questions/1308158/how-does-entity-framework-work-with-recursive-hierarchies-include-seems-not-t
            var employee = _compensationContext.Compensations.ToList().SingleOrDefault(c => c.employee.EmployeeId == id);
            return employee;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }
    }
}
