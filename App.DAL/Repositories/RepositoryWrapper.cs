using App.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly NorthwindContext _context;
        private IEmployeeRepository _employee;

        public RepositoryWrapper(NorthwindContext context)
        {
            _context = context;
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_context);
                }

                return _employee;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
