using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO со сведениями о проданном товаре.
    /// </summary>
    public class SaleDataBaseDto
    {
        /// <summary>
        /// Получает или задает идентификатор проданного продукта.
        /// </summary>
        [JsonRequired]
        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// Получает или задает число проданных товаров.
        /// </summary>
        [JsonRequired]
        [JsonProperty("quantity")]
        public int ProductQuantity { get; set; }

    }
}