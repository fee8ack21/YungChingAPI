using App.DAL.Models;
using App.DAL.Repositories;
using App.Model;
using App.Model.Common;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
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
                var employees = await _repositoryWrapper.Employee.GetAll().ToListAsync();
                var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

                return new OkObjectResult(employeeDtos);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new BadRequestResult();
                }

                var employee = await _repositoryWrapper.Employee.GetAll().FindAsync(id);

                if (employee == null)
                {
                    return new NotFoundResult();
                }

                var employeeDto = _mapper.Map<EmployeeDto>(employee);

                return new OkObjectResult(employeeDto);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto.EmployeeId != 0)
                {
                    return new BadRequestResult();
                }

                var employee = _mapper.Map<Employee>(employeeDto);

                _repositoryWrapper.Employee.Add(employee);
                await _repositoryWrapper.SaveAsync();

                return new CreatedAtActionResult("GetEmployee", "Employee", new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            try
            {
                if (id <= 0 || id != employeeDto.EmployeeId)
                {
                    return new BadRequestResult();
                }

                var employee = await _repositoryWrapper.Employee.GetAll().FindAsync(id);

                if (employee == null)
                {
                    return new NotFoundResult();
                }

                _mapper.Map(employeeDto, employee);

                await _repositoryWrapper.SaveAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult> PatchEmployee(int id, IEnumerable<PatchDatum> patchData)
        {
            if (patchData == null || patchData.Count() == 0)
            {
                return new BadRequestResult();
            }

            var employee = await _repositoryWrapper.Employee.GetAll().FindAsync(id);

            if (employee == null)
            {
                return new BadRequestResult();
            }

            var document = new JsonPatchDocument();

            foreach (var datum in patchData)
            {
                var operation = new Operation(op: "replace", path: datum.Path, value: datum.Value, from: "");
                document.Operations.Add(operation);
            }

            document.ApplyTo(employee);

            await _repositoryWrapper.SaveAsync();

            return new NoContentResult();
        }

        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _repositoryWrapper.Employee.GetAll().FindAsync(id);

                if (employee == null)
                {
                    return new NotFoundResult();
                }

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
