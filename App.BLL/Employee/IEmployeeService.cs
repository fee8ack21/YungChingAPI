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
        Task<ActionResult<EmployeeDto>> GetEmployee(long id);
        Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employee);
        Task<ActionResult> PutEmployee(long id, Employee employee);
        Task<ActionResult> DeleteEmployee(long id);
    }
}
