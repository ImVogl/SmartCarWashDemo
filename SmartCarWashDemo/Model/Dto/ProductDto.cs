using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// DTO обновлений сведений о продукте.
    /// </summary>
    public class ProductDto : BaseDto
    {
        /// <summary>
        /// Получает или задает стоимость продукта в рублях.
        /// </summary>
        [JsonRequired]
        [JsonProperty("price")]
        public float Price { get; set; }
    }
}