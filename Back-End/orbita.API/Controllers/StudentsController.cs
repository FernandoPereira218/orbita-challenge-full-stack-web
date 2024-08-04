using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using orbita.API.Data;
using orbita.API.Models;
using orbita.API.Handlers;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace orbita.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly APIDbContext _context;
        private List<object> Errors { get; set; } = new List<object>();
        private bool IsValid => !Errors.Any();

        public StudentsController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents(int? page = null, int? pageLength = null, string search = "")
        {
            if (!page.HasValue)
                Errors.Add(new { Propery = "page", Message = "Page must have a value" });
            if (!pageLength.HasValue)
                Errors.Add(new { Propery = "pageLength", Message = "PageLength must have a value" });

            if (IsValid)
            {
                var studentList = await _context.Students.ToListAsync();

                if (!string.IsNullOrEmpty(search)) //Search filter
                {
                    search = search.ToLower();
                    studentList = studentList.Where(x =>
                        x.CPF.ToLower().Contains(search) ||
                        x.Name.ToLower().Contains(search) ||
                        x.StudentID.ToLower().Contains(search) ||
                        x.Email.ToLower().Contains(search))
                   .ToList();
                }

                studentList = studentList.Skip((page.Value - 1) * pageLength.Value)
                    .Take(pageLength.Value)
                    .ToList();

                return Ok(studentList);
            }
            else
                return BadRequest(new { Errors });
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(string id, UpdateStudentData request)
        {
            StudentDataValidation(request, id);

            if (IsValid)
            {
                var student = _context.Students.Where(x => x.StudentID == id).FirstOrDefault();
                student.Name = request.Name;
                student.Email = request.Email;

                _context.Update(student);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(new { message = "Student updated successfully" });
            }
            else
                return BadRequest(new { Errors });
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(AddStudentData request)
        {
            StudentDataValidation(request);

            if (IsValid)
            {
                string studentID = GetNextStudentID();
                var student = new Student
                {
                    CPF = request.CPF,
                    Email = request.Email,
                    Name = request.Name,
                    StudentID = studentID
                };
                _context.Students.Add(student);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (StudentExists(student.StudentID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(new { message = "Student added successfully" });
                
            }
            else
                return BadRequest(new
                {
                    Errors
                });
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }

            var student = _context.Students.Where(x => x.StudentID == id).FirstOrDefault();
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Student deleted successfully" });
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }

        private void StudentDataValidation(AddStudentData data)
        {
            if (data == null)
                Errors.Add(new { Message = "Data is null" });
            else //POST validation
            {
                if (string.IsNullOrEmpty(data.CPF))
                    Errors.Add(new { Property = "CPF", Message = "CPF field is required" });
                else if (data.CPF.Length != 11)
                    Errors.Add(new { Property = "CPF", Message = "CPF must have 11 characters" });

                if (string.IsNullOrEmpty(data.Name))
                    Errors.Add(new { Property = "Name", Message = "Name field is required" });
                else if (data.Name.Length < 3)
                    Errors.Add(new { Property = "Name", Message = "Email must have at least 3 characters" });

                if (string.IsNullOrEmpty(data.Email))
                    Errors.Add(new { Property = "Email", Message = "Email field is required" });
                else if (data.Email.Length < 3)
                    Errors.Add(new { Property = "Email", Message = "Email must have at least 3 characters" });
            }
        }

        private void StudentDataValidation(UpdateStudentData data, string id)
        {
            if (data == null)
                Errors.Add(new { Message = "Data is null" });
            else //PUT validation
            {
                if (!StudentExists(id))
                    Errors.Add(new { Property = "Student", Message = "Student not found" });

                if (string.IsNullOrEmpty(data.Name))
                    Errors.Add(new { Property = "Name", Message = "Name field is required" });
                else if (data.Name.Length < 3)
                    Errors.Add(new { Property = "Name", Message = "Email must have at least 3 characters" });

                if (string.IsNullOrEmpty(data.Email))
                    Errors.Add(new { Property = "Email", Message = "Email field is required" });
                else if (data.Email.Length < 3)
                    Errors.Add(new { Property = "Email", Message = "Email must have at least 3 characters" });
            }
        }

        private string GetNextStudentID()
        {
            string? lastID = _context.Students.OrderByDescending(x => x.StudentID).FirstOrDefault()?.StudentID;
            int year = Convert.ToInt32(lastID.Substring(0, 4));
            int semester = Convert.ToInt32(lastID.Substring(4, 1));
            int digit = Convert.ToInt32(lastID.Substring(5, 4));

            DateTime currentDate = DateTime.Now;
            int currentSemester = currentDate.Month <= 5 ? 1 : 2;

            int nextDigit = (currentDate.Year == year && semester == currentSemester) ? digit + 1 : 1;

            string nextID = string.Format("{0}{1}{2}", currentDate.Year, currentSemester, nextDigit.ToString().PadLeft(4, '0'));
            return nextID;
        }
    }
}
