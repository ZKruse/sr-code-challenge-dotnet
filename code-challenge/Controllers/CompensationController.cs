using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService, IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationService = compensationService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetCompensations()
        {
            _logger.LogDebug("Received request for all compensations");

            var allCompensations = _compensationService.GetCompensations();

            return Ok(allCompensations);
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] CompensationRequestBody compensation)
        {
            _logger.LogDebug($"Received employee create request for '{compensation.employeeId}'");

            var employee = _employeeService.GetById(compensation.employeeId);

            if(employee != null)
            {
                if (_compensationService.GetByEmployeeId(compensation.employeeId) != null)
                    return Conflict();

                _compensationService.Create(new Compensation()
                {
                    employee = employee,
                    Salary = compensation.Salary,
                    EffectiveDate = new DateTime()
                });


                return CreatedAtRoute("getCompensationByEmployeeId", new { employeeId = compensation.employeeId }, compensation);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{employeeId}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationById(String employeeId)
        {
            _logger.LogDebug($"Received compensation get request for '{employeeId}'");

            var compensation = _compensationService.GetByEmployeeId(employeeId);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateCompensation(String id, [FromBody]Employee newEmployee)
        //{
        //    _logger.LogDebug($"Recieved employee update request for '{id}'");

        //    var existingEmployee = _employeeService.GetById(id);
        //    if (existingEmployee == null)
        //        return NotFound();

        //    _employeeService.Replace(existingEmployee, newEmployee);

        //    return Ok(newEmployee);
        //}
    }

    public class CompensationRequestBody
    {
        public String employeeId { get; set; } = Guid.NewGuid().ToString();
        public int Salary { get; set; } = 0;

    }
}
