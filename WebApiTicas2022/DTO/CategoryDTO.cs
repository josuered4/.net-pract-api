using WebApiTicas2022.Models;

namespace WebApiTicas2022.DTO
{
    public class CategoryDTO  // Este es el DTO de Categoria
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public static implicit operator Category(CategoryDTO categoryDTO)
        {
            return new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
            };
        }
    }
}
