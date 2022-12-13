using App.DAL.Models;
using App.Model;
using App.Model.Common;
using Microsoft.AspNetCore.JsonPatch;
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
        Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto);
        Task<ActionResult> PutEmployee(int id, EmployeeDto employeeDto);
        Task<ActionResult> PatchEmployee(int id, IEnumerable<PatchDatum> patchData);
        Task<ActionResult> DeleteEmployee(int id);
    }
}
