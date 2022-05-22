using WebApiTicas2022.Models;
namespace WebApiTicas2022.Models
{
    public class ProductDTO // Debe de tener el nombre del modelo a aplicar 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }

        //para tranformar un modelo a modeto DTO, debemos hacer algunso cambios a modelo

        public static implicit operator Product(ProductDTO productDTO) //es como un constructor 
        {

            return new Product //Instanciamos un product, 
            {
                Id = productDTO.Id, 
                Name = productDTO.Name,
                UniPrice = productDTO.UnitPrice,
            };
        }
    }
}
//Primero se ejecuta el modelo, seguido de esto se ejecuta una funcnion que crea una instancia de DTO,
//En este documento podemos una clase DTO con sus propiedades y su constructor