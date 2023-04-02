using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarWashDemo.Model.DataBase.Point
{
    /// <summary>
    /// Модель со сведениями о продуктах, доступных на точке продажи.
    /// </summary>
    [Table("provided_product")]
    public class ProvidedProduct : EntityBase
    {
        /// <summary>
        /// Получает или задает <see cref="SalesPoint"/>.
        /// </summary>
        public SalesPoint Point { get; set; }

        /// <summary>
        /// Получает или задает идентификатор продукта.
        /// </summary>
        [Column("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// Получает или задает число единиц продукции на точке продажи.
        /// </summary>
        [Column("quantity")]
        public int ProductQuantity { get; set; }
    }
}