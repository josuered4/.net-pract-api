
namespace WebApiTicas2022.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal UniPrice { get; set; }
        public decimal UnitsInStock { get; set; }
        public bool Discontinued { get; set; }

        public static explicit operator ProductDTO(Product product)
        {
            return new ProductDTO //instancia del DTO
            {
                Id = product.Id, // en lazamos el DTO al modelo
                Name = product.Name, // la propiedades que no se especifican no se muestran
                UnitPrice = product.UniPrice
            };
        }//se hace un operador de operacion 

    }
}
