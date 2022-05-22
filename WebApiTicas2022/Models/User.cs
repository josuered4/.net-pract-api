using WebApiTicas2022.DTO;
namespace WebApiTicas2022.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }

        public static explicit operator UserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        } 

    }
}
