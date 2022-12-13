using App.DAL.Models;
using App.DAL.Repositories;
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

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _repositoryWrapper.Employee.GetAll().ToListAsync();
        }
    }
}
