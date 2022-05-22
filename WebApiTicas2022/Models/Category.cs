using WebApiTicas2022.DTO;

namespace WebApiTicas2022.Models
{
    public class Category  // Este es la clase Categoria
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Discontinued { get; set; }

        public static explicit operator CategoryDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
    }
}
