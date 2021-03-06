using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;


        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }
        public List<Compensation> GetCompensations()
        {
            return _compensationRepository.GetCompensations();
        }

        public Compensation? Create(Compensation Compensation)
        {
            if(Compensation != null)
            {
                _compensationRepository.Add(Compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return Compensation;
        }

        public Compensation? GetByEmployeeId(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetByEmployeeId(id);
            }

            return null;
        }

        public Compensation? Replace(Compensation originalCompensation, Compensation newCompensation)
        {
            if(originalCompensation != null)
            {
                _compensationRepository.Remove(originalCompensation);
                if (newCompensation != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _compensationRepository.SaveAsync().Wait();

                    _compensationRepository.Add(newCompensation);
                    // overwrite the new id with previous Compensation id
                    //newCompensation.CompensationId = originalCompensation.CompensationId;
                }
                _compensationRepository.SaveAsync().Wait();
            }

            return newCompensation;
        }
    }
}
