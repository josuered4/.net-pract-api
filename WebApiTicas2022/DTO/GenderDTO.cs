using WebApiTicas2022.Models;
namespace WebApiTicas2022.Models

{
    public class GenderDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public String? Type { get; set; }

        public static implicit operator Gender(GenderDTO genderDTO)
        {
            return new Gender
            {
                Id = genderDTO.Id,
                Name = genderDTO.Name,
                Type = genderDTO.Type,
            };
        }
    }
}
