using App.DAL.Models;
using App.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface IEmployeeService
    {
        Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees();
        Task<ActionResult<EmployeeDto>> GetEmployee(int id);
        Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employee);
        Task<ActionResult> PutEmployee(int id, EmployeeDto employee);
        Task<ActionResult> DeleteEmployee(int id);
    }
}
