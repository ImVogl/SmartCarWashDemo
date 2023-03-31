using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO со сведениями об акте продажи.
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

        /// <summary>
        /// Получает или задает общую стоимость всей продукции.
        /// </summary>
        [CanBeNull]
        [JsonProperty("amount")]
        public float? TotalAmount { get; set; }

        /// <summary>
        /// Получает или задает день и время создания акта продажи.
        /// </summary>
        [CanBeNull]
        [JsonProperty("sell_date_time")]
        public DateTime? SellDateTime { get; set; }
    }
}