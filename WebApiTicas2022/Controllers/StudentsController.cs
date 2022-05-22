using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTicas2022.DTO;
using WebApiTicas2022.Models;

namespace WebApiTicas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly TicasContext Context;
        public StudentController(TicasContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student student)
        {
            Context.Students.Add(student);
            await Context.SaveChangesAsync();
            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }


        //[HttpGet]
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await Context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            //var Products = await Context.Products.Select(product =>
            //(ProductDTO)Products toListAsyc
            //);
            //return Products;

            //return await Context.Products.Select(product => (ProductDTO)product).ToListAsync();
            return await Context.Students.Select(student => (StudentDTO)student).ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult> PutStudent(int Id, StudentDTO studentDTO)
        {
            if (Id != studentDTO.Id)
            {
                return BadRequest();
            }

            var student = await Context.Students.FindAsync(studentDTO.Id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = studentDTO.Name;
            student.SemesterPrice = studentDTO.SemesterPrice;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return NoContent();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var student = await Context.Students.FindAsync(Id);
            if (student == null)
            {
                return NotFound();
            }
            Context.Students.Remove(student);
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }


}
