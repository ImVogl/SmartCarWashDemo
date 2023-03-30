using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartCarWashDemo.Model.DataBase
{
    /// <summary>
    /// Модель сущности базы данных покупатель.
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
        /// Получает или задает коллекцию всех идентификаторов сделанных пользователем покупок.
        /// </summary>
        [Required]
        [Column("sale_ids")]
        public ICollection<long> SalesIds { get; set; }

        /// <summary>
        /// Получает или задает день и время, когда был создан пользователь.
        /// </summary>
        [Required]
        [Column("registration_date_time")]
        public DateTime CreationDateTime { get; set; }
    }
}