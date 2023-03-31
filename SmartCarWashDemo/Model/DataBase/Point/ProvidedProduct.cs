using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarWashDemo.Model.DataBase.Point
{
    /// <summary>
    /// Модель со сведениями о продуктах, доступных на точке продажи.
    /// </summary>
    [Table("provided_product")]
    public class ProvidedProduct
    {
        /// <summary>
        /// Получает или задает идентификатор продукта.
        /// </summary>
        [Required]
        [Column("id")]
        public long ProductId { get; set; }

        /// <summary>
        /// Получает или задает число единиц продукции на точке продажи.
        /// </summary>
        [Required]
        [Column("quantity")]
        public int ProductQuantity { get; set; }
    }
}