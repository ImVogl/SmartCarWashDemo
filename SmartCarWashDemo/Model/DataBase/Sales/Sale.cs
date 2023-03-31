using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace SmartCarWashDemo.Model.DataBase.Sales
{
    /// <summary>
    /// Модель сущности базы данных: акт продажи.
    /// </summary>
    [Table("sales")]
    public class Sale
    {
        /// <summary>
        /// Получает или задает идентификатор акта продажи.
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Получает или задает день продажи (Увы, но в .NET 5.0 нет DateOnly).
        /// </summary>
        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Получает или задает время продажи.
        /// </summary>
        [Required]
        [Column("time")]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Получает или задает идентификатор точки продажи.
        /// </summary>
        [Required]
        [Column("point_id")]
        public long SalesPointId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор покупателя.
        /// </summary>
        [CanBeNull]
        [Column("customer_id")]
        public long? CustomerId { get; set; }

        /// <summary>
        /// Получает или задает коллекцию <see cref="SaleData"/>.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        [Required]
        [Column("sales")]
        public ICollection<SaleData> SalesData { get; set; } = null!;

        /// <summary>
        /// Получает или задает общую стоимость всей продукции.
        /// </summary>
        [Required]
        [Column("amount")]
        public float TotalAmount { get; set; }
    }
}