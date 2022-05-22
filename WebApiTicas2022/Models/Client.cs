using WebApiTicas2022.DTO;

namespace WebApiTicas2022.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Telephone { get; set; }

        public string email { get; set; }

        public bool IsClient { get; set; }

        public static explicit operator ClientDTO(Client client)
        {
            return new ClientDTO
            {
                Id = client.Id,
                Name = client.Name,
                email = client.email,
            };
        }
    }
}
