using WebApiTicas2022.Models;
namespace WebApiTicas2022.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public static implicit operator User(UserDTO userDTO)
        {
            return new User{
                Id = userDTO.Id,
                Name = userDTO.Name,
                Email = userDTO.Email,
            };
        }

    }
}
