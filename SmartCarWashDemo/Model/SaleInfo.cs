using JetBrains.Annotations;
using SmartCarWashDemo.Model.Dto.Sales;
using System.Collections.Generic;
using System;

namespace SmartCarWashDemo.Model
{
    /// <summary>
    /// Сведения обновления/добавления акта продажи.
    /// </summary>
    public class SaleInfo
    {
        /// <summary>
        /// Получает или задает идентификатор акта продажи.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Получает или задает идентификатор точки продажи.
        /// </summary>
        public long SalesPointId { get; set; }

        /// <summary>
        /// Получает или задает коллекцию <see cref="SaleDataDto"/>.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ICollection<SaleDataInfo> SalesData { get; set; } = null!;

        /// <summary>
        /// Получает или задает идентификатор покупателя.
        /// </summary>
        [CanBeNull]
        public long? CustomerId { get; set; }

        /// <summary>
        /// Получает или задает общую стоимость всей продукции.
        /// </summary>
        [CanBeNull]
        public float TotalAmount { get; set; }

        /// <summary>
        /// Получает или задает день продажи (Увы, но в .NET 5.0 нет DateOnly).
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Получает или задает время продажи.
        /// </summary>
        public TimeSpan Time { get; set; }

    }
}