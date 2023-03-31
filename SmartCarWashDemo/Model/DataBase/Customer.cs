using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SmartCarWashDemo.Model.DataBase.Sales;

namespace SmartCarWashDemo.Model.DataBase
{
    /// <summary>
    /// Модель сущности базы данных: покупатель.
    /// </summary>
    [Table("customers")]
    public class Customer
    {
        /// <summary>
        /// Получает или задает идентификатор пользователя.
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Получает или задает имя пользователя.
        /// </summary>
        [Required]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает коллекцию <see cref="Sale"/> для данного пользователя.
        /// </summary>
        public ICollection<Sale> Sales { get; set; }

        /// <summary>
        /// Получает или задает день и время, когда был создан пользователь.
        /// </summary>
        [Required]
        [Column("registration_date_time")]
        public DateTime CreationDateTime { get; set; }
    }
}