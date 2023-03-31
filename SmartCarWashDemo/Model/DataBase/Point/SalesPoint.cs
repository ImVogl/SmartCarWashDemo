using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace SmartCarWashDemo.Model.DataBase.Point
{
    /// <summary>
    /// Модель сущности базы данных: точка продажи.
    /// </summary>
    [Table("sales_point")]
    public class SalesPoint : EntityBase
    {
        /// <summary>
        /// Получает или задает имя точки продажи.
        /// </summary>
        [Required]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает коллекцию <see cref="ProvidedProduct"/>.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ICollection<ProvidedProduct> ProvidedProducts { get; set; } = null!;
    }
}