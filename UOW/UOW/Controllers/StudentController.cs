using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UOW.Domain.DTO;
using UOW.Domain.Models;
using UOW.Domain.Repository;
using UOW.Service.Service;

namespace UOW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("getAllStudent")]
        public async Task<IActionResult> getAllStudent()
        {
            var student = await _unitOfWork.Students.getAllAsync();
            var studentDTO = student.Select(stdDTO => new StudentDTO
            {
                studentName = stdDTO.studentName,
                Mobile = stdDTO.Mobile,
                NationalID = stdDTO.NationalID,
                DeptId = stdDTO.DeptId
            }).ToList();
            return Ok(studentDTO);
        }
        [HttpGet("getStudentWithDepartmentName")]
        public async Task<IActionResult> getStudentWithDepartmentName()
        {
            var student = await _unitOfWork.Students.findWithIncludeAsync(d => d.Department);
            var studentWithDepartmentName = student.Select(std => new StudentWithDepartmentName
            {
                studentName=std.studentName,
                phoneNumber=std.Mobile,
                departmentName=std.Department.departmentName
            });
            return Ok(studentWithDepartmentName);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Student
            {
                studentName = studentDTO.studentName,
                NationalID = studentDTO.NationalID,
                Mobile = studentDTO.Mobile,
                DeptId = studentDTO.DeptId
            };

            var addedStudent = await _unitOfWork.Students.addAsync(student);

            _unitOfWork.save();

            return Ok(addedStudent);
        }
        [HttpPut("{stdId:int}")]
        public async Task<IActionResult> editStudent([FromRoute] int stdId,[FromBody] StudentDTO studentDTO)
        {
            var editstudent=await _unitOfWork.Students.getByIdAsync(stdId);
            if (editstudent == null)
            {
                return NotFound();
            }
            editstudent.studentName = studentDTO.studentName;
            editstudent.Mobile=studentDTO.Mobile;
            editstudent.NationalID= studentDTO.NationalID;
            editstudent.DeptId = studentDTO.DeptId;
            _unitOfWork.save();
            return Ok(editstudent);
        }
        [HttpDelete("{stdId:int}")]
        public async Task<IActionResult> deleteStudent(int stdId)
        {
            var std=await _unitOfWork.Students.getByIdAsync(stdId);
            if (std == null)
            {
                return NotFound();
            }
            _unitOfWork.Students.Delete(std);
            _unitOfWork.save();
            return Ok();
        }

    }
}
