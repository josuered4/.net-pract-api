using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTicas2022.DTO;
using WebApiTicas2022.Models;

namespace WebApiTicas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase  // Controlador de Categoria
    {
        private readonly TicasContext Context; // Se crea la variable de tipo contexto

        public CategoryController(TicasContext context)
        {
            Context = context; //Inyectamos la dependencia contexto
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            Context.Categories.Add(category); //se agrega un producto dentro del listado producto

            await Context.SaveChangesAsync(); //Se usa para trabajar en otro hilo

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
            //Retornamos la accion de crear un producto, retornando el valor id
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await Context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            return await Context.Categories.Select(category => (CategoryDTO)category).ToListAsync();
        }
        [HttpPut]
        public async Task<ActionResult> PutCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest();
            }

            var Category = await Context.Categories.FindAsync(id);
            if (Category == null)
            {
                return NotFound();
            }

            Category.Name = categoryDTO.Name;

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
        public async Task<ActionResult<CategoryDTO>> DeleteCategory(int id)
        {
            var category = await Context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            Context.Categories.Remove(category);
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
