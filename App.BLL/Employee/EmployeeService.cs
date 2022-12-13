using App.DAL.Models;
using App.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public EmployeeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var entries = await _repositoryWrapper.Employee.GetAll().ToListAsync();

                return new OkObjectResult(entries);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            try
            {
                if (id <= 0) { return new BadRequestResult(); }

                var entry = await _repositoryWrapper.Employee.GetByCondition(x => x.EmployeeId == id).FirstOrDefaultAsync();

                if (entry == null) { return new NotFoundResult(); }

                return new OkObjectResult(entry);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                _repositoryWrapper.Employee.Add(employee);
                await _repositoryWrapper.SaveAsync();

                return new CreatedAtActionResult("GetEmployee", "Employee", new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult> PutEmployee(long id, Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Employee>> DeleteEmployee(long id)
        {
            try
            {
                var employee = await _repositoryWrapper.Employee.GetByCondition(x => x.EmployeeId == id).FirstOrDefaultAsync();

                if (employee == null) { return new NotFoundResult(); }

                _repositoryWrapper.Employee.Remove(employee);
                await _repositoryWrapper.SaveAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
