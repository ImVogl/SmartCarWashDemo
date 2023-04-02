using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarWashDemo.Model.DataBase.Sales
{
    /// <summary>
    /// Модель данных со сведениями о проданном товаре.
    /// </summary>
    public class SaleData : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("sale_id")]
        public long SaleId { get; set; }

        /// <summary>
        /// Получает или задает родительский <see cref="Sales.Sale"/>.
        /// </summary>
        public Sale Sale { get; set; }

        /// <summary>
        /// Получает или задает идентификатор проданного продукта.
        /// </summary>
        [Column("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// Получает или задает число проданных товаров.
        /// </summary>
        [Column("quantity")]
        public int ProductQuantity { get; set; }

        /// <summary>
        /// Получает или задает общую стоимость проданного товара.
        /// </summary>
        [Column("amount")]
        public float ProductAmount { get; set; }
    }
}