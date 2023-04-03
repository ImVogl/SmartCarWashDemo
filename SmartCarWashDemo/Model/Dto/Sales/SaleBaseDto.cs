using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO создания акта продаж.
    /// </summary>
    public class SaleBaseDto : BaseDto
    {
        /// <summary>
        /// Получает или задает идентификатор точки продажи.
        /// </summary>
        [JsonRequired]
        [JsonProperty("point_id")]
        public long SalesPointId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор покупателя.
        /// </summary>
        [CanBeNull]
        [JsonProperty("customer_id")]
        public long? CustomerId { get; set; }
    }
}