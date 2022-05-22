using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTicas2022.DTO;
using WebApiTicas2022.Models;

namespace WebApiTicas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly TicasContext Context;

        public ClientsController(TicasContext context)
        {
            Context = context;
        }
        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client client)
        {
            Context.Clients.Add(client);

            await Context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await Context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return client;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            return await Context.Clients.Select(client => (ClientDTO)client).ToListAsync();
        }
        [HttpPut]
        public async Task<IActionResult> PutClient(int id, ClientDTO clientDTO)
        {
            if (id != clientDTO.Id)
            {
                return BadRequest();
            }

            var Client = await Context.Clients.FindAsync(id);
            if (Client == null)
            {
                return NotFound();
            }

            Client.Name = clientDTO.Name;
            Client.email = clientDTO.email;

            Client.Name = clientDTO.Name;
            Client.email = clientDTO.email;

            try
            {
                await Context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<ClientDTO>> DeleteClient(int id)
        {
            var client = await Context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            Context.Clients.Remove(client);
            await Context.SaveChangesAsync();
            return NoContent();

        }

    }
}
