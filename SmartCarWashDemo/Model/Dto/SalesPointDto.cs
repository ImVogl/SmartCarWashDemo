using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// DTO со сведениями о точке продажи.
    /// </summary>
    public class SalesPointDto : BaseDto
    {
        /// <summary>
        /// Получает или задает словарь, где ключ - идентификатор продукта, а значение - число товаров доступных к продаже.
        /// </summary>
        [JsonRequired]
        [JsonProperty("products")]
        public Dictionary<long, int> ProvidedProducts { get; set; }
    }
}