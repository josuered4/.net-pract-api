using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTicas2022.DA;
using WebApiTicas2022.Models;

namespace WebApiTicas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly TicasContext Context; //creacion de variable

        public GenderController(TicasContext context) //constructor
        {
            Context = context; //inyeccion de la dependencia
        }
        [HttpPost]
        public async Task<ActionResult<Gender>> Post(Gender gender)
        {
            Context.Genders.Add(gender); //se agrega un genero dentro del listado de 
            await Context.SaveChangesAsync();
            return CreatedAtAction("GetGender", new {id=gender.Id }, gender ); //retorna http 201 que indica que se creo un objeto 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGender(int id)
        {
            var gender = await Context.Genders.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return gender;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderDTO>>> GetGenders()
        {
            return await Context.Genders.Select(gender => (GenderDTO)gender).ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult> PutGender(int id, GenderDTO genderDTO)
        {
            if (id != genderDTO.Id)
            {
                return BadRequest();
            }

            var Gender = await Context.Genders.FindAsync(genderDTO.Id);

            if (Gender == null)
            {
                return NotFound();
            }

            Gender.Name = genderDTO.Name;
            Gender.Type = genderDTO.Type;

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
        public async Task<ActionResult> DeleteGender(int id)
        {
            var gender = await Context.Genders.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            Context.Genders.Remove(gender);
            await Context.SaveChangesAsync();
            return NoContent();
        }

    }
}
