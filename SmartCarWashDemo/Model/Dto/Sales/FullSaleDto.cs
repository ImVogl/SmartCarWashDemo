using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO со всеми сведениями об акте продажи.
    /// </summary>
    public class FullSaleDto : SaleDto
    {
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