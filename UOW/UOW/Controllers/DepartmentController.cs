using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UOW.Domain.DTO;
using UOW.Domain.Models;
using UOW.Domain.Repository;

namespace UOW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("getAllDepartmentsAsync")]
        public async Task<IActionResult> getAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.Departments.getAllAsync();

            var departmentDtos = departments.Select(dept => new DepartmentDTO
            {
                DepartmentName = dept.departmentName
            });

            return Ok(departmentDtos);
        }
        [HttpGet("getDapartmentWithStudent")]
        public async Task<IActionResult> GetDepartmentWithStudent()
        {
            var departments = await _unitOfWork.Departments.findWithIncludeAsync(d => d.Students);

            var departmentWithStudentsDTOs = departments.Select(department => new DepartmentWithStudentsDTO
            {
                departmentName = department.departmentName,
                studentNames = department.Students.Select(student => student.studentName).ToList()
            }).ToList();

            return Ok(departmentWithStudentsDTOs);
        }


        [HttpPost]
        public IActionResult addDepartment(DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
               Department department = new Department
                {
                    departmentName = departmentDTO.DepartmentName
                };
                var dept = _unitOfWork.Departments.addAsync(department);
                _unitOfWork.save();
                return Ok(dept);
            
        }
        [HttpPut("{deptartmentId:int}")]
        public async Task<IActionResult> editDepartment([FromRoute] int deptartmentId, [FromBody] DepartmentDTO departmentDTO)
        {
            var oldDept=await _unitOfWork.Departments.getByIdAsync(deptartmentId);
            oldDept.departmentName=departmentDTO.DepartmentName;
            _unitOfWork.save();
            return Ok();
        }
        [HttpDelete("{deptartmentId:int}")]
        public async Task<IActionResult> deleteDepartment(int deptartmentId)
        {
            var dept = await _unitOfWork.Departments.getByIdAsync(deptartmentId);
            if(dept == null)
            {
                return NotFound();
            }
            _unitOfWork.Departments.Delete(dept);
            _unitOfWork.save();
            return Ok();
        }
        
    }
}
