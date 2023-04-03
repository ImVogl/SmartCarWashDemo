using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO со всеми сведениями об акте продажи.
    /// </summary>
    public class ResultSaleDto : SaleBaseDto
    {
        /// <summary>
        /// Получает или задает общую стоимость всей продукции.
        /// </summary>
        [JsonRequired]
        [JsonProperty("amount")]
        public float TotalAmount { get; set; }

        /// <summary>
        /// Получает или задает день и время создания акта продажи.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sell_date_time")]
        public DateTime SellDateTime { get; set; } 

        /// <summary>
        /// Получает или задает коллекцию <see cref="ResultSaleDataDto"/>.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        [JsonRequired]
        [JsonProperty("sales")]
        public ICollection<ResultSaleDataDto> SalesData { get; set; } = null!;

    }
}