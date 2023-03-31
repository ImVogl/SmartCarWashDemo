using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarWashDemo.Model.DataBase.Point
{
    /// <summary>
    /// Модель сущности базы данных: точка продажи.
    /// </summary>
    [Table("sales_point")]
    public class SalesPoint
    {
        /// <summary>
        /// Получает или задает идентификатор точки продажи.
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Получает или задает имя точки продажи.
        /// </summary>
        [Required]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает коллекцию <see cref="ProvidedProduct"/>.
        /// </summary>
        [Required]
        [Column("products")]
        public ICollection<ProvidedProduct> ProvidedProducts { get; set; }
    }
}