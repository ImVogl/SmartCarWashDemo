namespace SmartCarWashDemo.Model
{
    /// <summary>
    /// Сведения о проданном товаре.
    /// </summary>
    public class SaleDataInfo
    {
        /// <summary>
        /// Получает или задает идентификатор проданного продукта.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Получает или задает число проданных товаров.
        /// </summary>
        public int ProductQuantity { get; set; }

        /// <summary>
        /// Получает или задает общую стоимость проданного товара.
        /// </summary>
        public float ProductAmount { get; set; }
    }
}