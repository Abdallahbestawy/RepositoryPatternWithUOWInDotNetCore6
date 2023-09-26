using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOW.Domain.Models;

namespace UOW.Domain.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Student> Students { get; }  
        IBaseRepository<Department> Departments { get; }
        int save();
    }
}
