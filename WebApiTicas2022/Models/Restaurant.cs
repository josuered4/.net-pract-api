using WebApiTicas2022.DTO;
namespace WebApiTicas2022.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string? RestaurantName { get; set; }
        public string? Address { get; set; }
        public string? Owner  { get; set; }

        public static explicit operator RestaurantDTO(Restaurant restaurant)
        {
            return new RestaurantDTO
            {
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.RestaurantName,
                Address = restaurant.Address,
            };
        }
    }
}
