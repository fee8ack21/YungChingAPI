using App.DAL.Models;
using App.DAL.Repositories;
using App.Model;
using AutoMapper;
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
        private IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public EmployeeService(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            try
            {
                var _entries = await _repositoryWrapper.Employee.GetAll().ToListAsync();

                var entries = _mapper.Map<List<EmployeeDto>>(_entries);

                return new OkObjectResult(entries);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult<EmployeeDto>> GetEmployee(long id)
        {
            try
            {
                if (id <= 0) { return new BadRequestResult(); }

                var _entry = await _repositoryWrapper.Employee.GetByCondition(x => x.EmployeeId == id).FirstOrDefaultAsync();
                
                if (_entry == null) { return new NotFoundResult(); }
                
                var entry = _mapper.Map<EmployeeDto>(_entry);

                return new OkObjectResult(entry);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employee)
        {
            try
            {
                var _employee = _mapper.Map<Employee>(employee);
                
                _repositoryWrapper.Employee.Add(_employee);
                await _repositoryWrapper.SaveAsync();

                return new CreatedAtActionResult("GetEmployee", "Employee", new { id = _employee.EmployeeId }, _employee);
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

        public async Task<ActionResult> DeleteEmployee(long id)
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
