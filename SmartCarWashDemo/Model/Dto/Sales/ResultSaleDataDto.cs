using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto.Sales
{
    /// <summary>
    /// DTO с полными сведениями о проданном товаре.
    /// </summary>
    public class ResultSaleDataDto : SaleDataBaseDto
    {
        /// <summary>
        /// Получает или задает общую стоимость проданного товара.
        /// </summary>
        [JsonProperty("amount")]
        public float? ProductAmount { get; set; }
    }
}