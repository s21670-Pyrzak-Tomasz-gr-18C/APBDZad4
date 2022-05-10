using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public List<Student> _student = new List<Student>();

        [HttpGet]
        public IActionResult GetStudents()
        {
            CSVService.ReadCSV(_student);
            return Ok(_student);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            CSVService.findStudent(_student, indexNumber);
            if (_student.Count == 0)
            {
                return NoContent();
            }
            else 
            {
                return Ok(_student);
            }
        }

        [HttpPut("{indexNumber}")]
        public IActionResult updateStudent(string indexNumber, [FromBody] Student student)
        {
            _student = CSVService.updateStudent(student, indexNumber);
            CSVService.SaveToCSV(_student, true);
            return Ok(student);
        }

        [HttpPost]
        public IActionResult createStudent(Student student)
        {
            if (!Regex.IsMatch(student.indexNumber, @"s[0-9]+")) 
            { 
                return BadRequest("The indexNumber has invalid format."); 
            }
            _student.Add(student);
            CSVService.SaveToCSV(_student, false);
            return Ok(student);
        }

        [HttpDelete]
        public IActionResult deleteStudent(string indexNumber)
        { 
            _student = CSVService.deleteStudent(indexNumber);
            CSVService.SaveToCSV(_student, true);
            return Ok(_student);
        }
    }
}
