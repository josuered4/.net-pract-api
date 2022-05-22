using WebApiTicas2022.DTO;

namespace WebApiTicas2022.Models
{
    public class Promoción
    {
        public int id { get; set; }
        public string productName { get; set; }
        public int discount { get; set; }

        public static explicit operator PromotionDTO(Promoción promoción)
        {
            return new PromotionDTO{
                productNameDTO = promoción.productName,
                discountDTO = promoción.discount
            };
        }
    }
}
