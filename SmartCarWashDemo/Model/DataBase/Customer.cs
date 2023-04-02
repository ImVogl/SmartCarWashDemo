using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using SmartCarWashDemo.Model.DataBase.Sales;

namespace SmartCarWashDemo.Model.DataBase
{
    /// <summary>
    /// Модель сущности базы данных: покупатель.
    /// </summary>
    [Table("customers")]
    public class Customer : EntityBase
    {
        /// <summary>
        /// Получает или задает имя пользователя.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает коллекцию <see cref="Sale"/> для данного пользователя.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ICollection<Sale> Sales { get; set; } = null!;

        /// <summary>
        /// Получает или задает день и время, когда был создан пользователь.
        /// </summary>
        [Column("registration_date_time")]
        public DateTime CreationDateTime { get; set; }
    }
}