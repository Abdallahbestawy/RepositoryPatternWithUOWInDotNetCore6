using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOW.Domain.DTO
{
    public class DepartmentWithStudentsDTO
    {
        public String departmentName { get; set; }
        public List<string> studentNames { get; set; }=new List<string>();
    }
}
