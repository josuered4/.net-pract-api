using WebApiTicas2022.Models;
namespace WebApiTicas2022.DTO
{
    public class RestaurantDTO
    {
        public int RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? Address { get; set;  }

        public static implicit operator Restaurant(RestaurantDTO restaurantDTO)
        {
            return new Restaurant
            {
                Id = restaurantDTO.RestaurantId,
                RestaurantName = restaurantDTO.RestaurantName,
                Address = restaurantDTO.Address,

            };
        }

    }
}
