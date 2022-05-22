using WebApiTicas2022.Models;

namespace WebApiTicas2022.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string email { get; set; }

        public static implicit operator Client(ClientDTO clientDTO)
        {
            return new Client
            {
                Id = clientDTO.Id,
                Name = clientDTO.Name,
                email = clientDTO.email,
            };
        }
    }
}
