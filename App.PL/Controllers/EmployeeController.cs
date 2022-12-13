using App.BLL;
using App.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _service.GetEmployees();
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Employee))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            return await _service.GetEmployee(id);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            return await _service.PostEmployee(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(long id, Employee employee)
        {
            return await _service.PutEmployee(id, employee);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<Employee>> DeleteEmployee(long id)
        {
            return await _service.DeleteEmployee(id);
        }
    }
}
