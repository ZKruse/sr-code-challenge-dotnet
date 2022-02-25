using challenge.Models;
using System;
using System.Collections.Generic;

namespace challenge.Services
{
    public interface ICompensationService
    {
        List<Compensation> GetCompensations();
        Compensation? GetByEmployeeId(String employeeId);
        Compensation? Create(Compensation compensation);
        Compensation? Replace(Compensation originalCompensation, Compensation newCompensation);
    }
}
