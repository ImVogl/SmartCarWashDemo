using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// DTO обновлений сведений о продукте.
    /// </summary>
    public class UpdateProductDto
    {
        /// <summary>
        /// Получает или задает идентификатор пользователя.
        /// </summary>
        [JsonRequired]
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Получает или задает имя пользователя.
        /// </summary>
        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает стоимость продукта в рублях.
        /// </summary>
        [JsonRequired]
        [JsonProperty("price")]
        public float Price { get; set; }
    }
}