﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using SmartCarWashDemo.Model.DataBase.Point;

namespace SmartCarWashDemo.Model.DataBase.Sales
{
    /// <summary>
    /// Модель сущности базы данных: акт продажи.
    /// </summary>
    [Table("sales")]
    public class Sale : EntityBase
    {
        /// <summary>
        /// Получает или задает день продажи (Увы, но в .NET 5.0 нет DateOnly).
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Получает или задает время продажи.
        /// </summary>
        [Column("time")]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Получает или задает точку продажи.
        /// </summary>
        [NotNull]
        [Column("point")]
        public SalesPoint SalesPoint { get; set; } = null!;

        /// <summary>
        /// Получает или задает идентификатор точки продажи.
        /// </summary>
        [NotNull]
        [Column("point_id")]
        public long SalesPointId { get; set; }

        /// <summary>
        /// Получает или задает <see cref="Customer"/>.
        /// </summary>
        [CanBeNull]
        [Column("customer")]
        public Customer Customer { get; set; }

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
        public ICollection<SaleData> SalesData { get; set; } = null!;

        /// <summary>
        /// Получает или задает общую стоимость всей продукции.
        /// </summary>
        [Column("amount")]
        public float TotalAmount { get; set; }
    }
}