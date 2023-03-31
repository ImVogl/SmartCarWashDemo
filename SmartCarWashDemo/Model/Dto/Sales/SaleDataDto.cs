using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO со сведениями о проданном товаре.
    /// </summary>
    public class SaleDataDto
    {
        /// <summary>
        /// Получает или задает идентификатор проданного продукта.
        /// </summary>
        [JsonRequired]
        [JsonProperty("id")]
        public long ProductId { get; set; }

        /// <summary>
        /// Получает или задает число проданных товаров.
        /// </summary>
        [JsonRequired]
        [JsonProperty("quantity")]
        public int ProductQuantity { get; set; }

        /// <summary>
        /// Получает или задает общую стоимость проданного товара.
        /// </summary>
        [JsonRequired]
        [JsonProperty("amount")]
        public float ProductAmount { get; set; }
    }
}