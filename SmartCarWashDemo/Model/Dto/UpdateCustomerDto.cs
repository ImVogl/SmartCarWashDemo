using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// DTO обновлений сведений о пользователе.
    /// </summary>
    public class UpdateCustomerDto
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
        /// Получает или задает коллекцию всех идентификаторов сделанных пользователем покупок.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sale_ids")]
        public ICollection<long> SalesIds { get; set; }
    }
}