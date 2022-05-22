using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTicas2022.Models;
using WebApiTicas2022.DTO;
using Microsoft.EntityFrameworkCore;


namespace WebApiTicas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly TicasContext Context;

        public RestaurantsController(TicasContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> Restaurant(Restaurant restaurant)
        {
            Context.Restaurants.Add(restaurant);
            await Context.SaveChangesAsync();
            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await Context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return restaurant;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllRestaurants()
        {
            return await Context.Restaurants.ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            Context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRestaurant(int Id)
        {
            var InfoRestaurant = await Context.Restaurants.FindAsync(Id);
            if (InfoRestaurant == null)
            {
                return NotFound();
            }
            Context.Restaurants.Remove(InfoRestaurant);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        /*
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant(int id)
        {
            var restaurant = await Context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return RestaurantDTO(restaurant);
        }*/

        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurantsDTO()
        {
            return await Context.Restaurants.Select(restaurant => (RestaurantDTO)restaurant).ToListAsync();
        }*/
        //En el caso de tener dos metodos con funcionalidad similar, no compilara, en este caso el segundo metodo torna datos con el DTO y elm
        /* primero con el modelo como tal, ambos funcionan siempre y cuando uno no este arriba */
        /*
        [HttpPut]
        public async Task<ActionResult> PutRestaurantDTO(int Id, RestaurantDTO restaurantDTO)
        {
            if(Id != restaurantDTO.RestaurantId)
            {
                return BadRequest();
            }
            var DataRestaurant = await Context.Restaurants.FindAsync(restaurantDTO.RestaurantId);

            if (DataRestaurant == null)
            {
                return NotFound();
            }
            DataRestaurant.RestaurantName = restaurantDTO.RestaurantName;
            DataRestaurant.Address = restaurantDTO.Address;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return NoContent();
        }*/
    }
}
