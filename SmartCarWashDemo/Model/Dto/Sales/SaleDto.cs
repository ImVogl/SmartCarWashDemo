using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO с базовыми сведениями об акте продажи.
    /// </summary>
    public class SaleDto
    {
        /// <summary>
        /// Получает или задает идентификатор акта продажи.
        /// </summary>
        [JsonRequired]
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Получает или задает идентификатор точки продажи.
        /// </summary>
        [JsonRequired]
        [JsonProperty("point_id")]
        public long SalesPointId { get; set; }

        /// <summary>
        /// Получает или задает коллекцию <see cref="SaleDataDto"/>.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        [JsonRequired]
        [JsonProperty("sales")]
        public ICollection<SaleDataDto> SalesData { get; set; } = null!;

        /// <summary>
        /// Получает или задает идентификатор покупателя.
        /// </summary>
        [CanBeNull]
        [JsonProperty("customer_id")]
        public long? CustomerId { get; set; }
    }
}