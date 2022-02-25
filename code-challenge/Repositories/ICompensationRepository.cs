using challenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        List<Compensation> GetCompensations();
        Compensation? GetByEmployeeId(String id);
        Compensation Add(Compensation employee);
        Compensation Remove(Compensation employee);
        Task SaveAsync();
    }
}