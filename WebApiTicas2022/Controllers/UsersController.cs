using Microsoft.AspNetCore.Mvc;
using WebApiTicas2022.Models;
using Microsoft.EntityFrameworkCore;
using WebApiTicas2022.DTO;


namespace WebApiTicas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TicasContext Context;

        public UsersController(TicasContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            //return await Context.Users.ToListAsync();
            return await Context.Users
                .Select(user => (UserDTO)user)
                .ToListAsync();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await Context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }


        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete]
        public async Task<ActionResult<UserDTO>> DeleteUser(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            Context.Users.Remove(user);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutUser(int id, UserDTO userDTO)
        {
            if(id != userDTO.Id)
            {
                return BadRequest();
            }

            var user = await Context.Users.FindAsync(userDTO.Id);
            if(user == null)
            {
                return NotFound();
            }

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return NoContent();
        }
    }
}
