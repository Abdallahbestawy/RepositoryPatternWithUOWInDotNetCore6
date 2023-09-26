using repo.ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOW.Domain.Models;
using UOW.Domain.Repository;

namespace UOW.Service.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Student> Students { get; private set; }
        public IBaseRepository<Department> Departments { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Students = new BaseRepository<Student>(context);
            Departments = new BaseRepository<Department>(context);
        }

        public int save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
