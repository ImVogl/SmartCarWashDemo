using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarWashDemo.Model.DataBase
{
    /// <summary>
    /// Модель сущности базы данных точка продажи.
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
        /// Получает или задает словарь, где ключ - идентификатор продукта, а значение - число товаров доступных к продаже.
        /// P.S. Я читал задание, где указано, что нужно использовать сущность ProvidedProduct. Но в тестовом задании не предполагается, что
        /// элементы коллекции должны быть доступны для расширения.
        /// </summary>
        [Required]
        [Column("products")]
        public Dictionary<long, int> ProvidedProducts { get; set; }
    }
}