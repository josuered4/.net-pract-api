using WebApiTicas2022.Models;
namespace WebApiTicas2022.DTO
{
    public class PromotionDTO
    {
        public string? productNameDTO { get; set; }
        public int discountDTO { get; set; }

        public static implicit operator Promoción(PromotionDTO promotionDTO)
        {
            return new Promoción
            {
                productName = promotionDTO.productNameDTO!,
                discount = promotionDTO.discountDTO,
            };
        }
    }
}
