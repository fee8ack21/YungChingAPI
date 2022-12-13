using App.DAL.Models;
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
        Task<ActionResult<IEnumerable<Employee>>> GetEmployees();
        Task<ActionResult<Employee>> GetEmployee(long id);
        Task<ActionResult<Employee>> PostEmployee(Employee employee);
        Task<ActionResult<Employee>> DeleteEmployee(long id);
        Task<ActionResult> PutEmployee(long id, Employee employee);
    }
}
