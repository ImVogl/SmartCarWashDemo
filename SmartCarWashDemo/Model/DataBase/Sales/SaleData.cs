using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartCarWashDemo.Model.DataBase.Sales
{
    /// <summary>
    /// Модель данных со сведениями о проданном товаре.
    /// </summary>
    public class SaleData
    {
        /// <summary>
        /// Получает или задает идентификатор проданного продукта.
        /// </summary>
        [Required]
        [Column("id")]
        public long ProductId { get; set; }

        /// <summary>
        /// Получает или задает число проданных товаров.
        /// </summary>
        [Required]
        [Column("quantity")]
        public int ProductQuantity { get; set; }

        /// <summary>
        /// Получает или задает общую стоимость проданного товара.
        /// </summary>
        [Required]
        [Column("amount")]
        public float ProductAmount { get; set; }
    }
}