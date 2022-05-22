using WebApiTicas2022.Models;
namespace WebApiTicas2022.Models
{
    public class Gender
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }


        public static explicit operator GenderDTO(Gender gender)
        {
            return new GenderDTO
            {
                Id = gender.Id,
                Name = gender.Name,
                Type = gender.Type,
            };

        }
 
    }
}
