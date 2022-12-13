using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repositories
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employee { get; }

        void Save();
        Task SaveAsync();
    }
}
