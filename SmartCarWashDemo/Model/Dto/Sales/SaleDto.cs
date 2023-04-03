using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO создания нового акта продажи.
    /// </summary>
    public class SaleDto : SaleBaseDto
    {
        /// <summary>
        /// Получает или задает коллекцию <see cref="SaleDataBaseDto"/>.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        [JsonRequired]
        [JsonProperty("sales")]
        public ICollection<SaleDataBaseDto> SalesData { get; set; } = null!;
    }
}