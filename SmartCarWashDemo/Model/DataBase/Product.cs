using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartCarWashDemo.Model.DataBase
{
    /// <summary>
    /// Модель сущности базы данных: продукт.
    /// </summary>
    [Table("products")]
    public class Product : EntityBase
    {
        /// <summary>
        /// Получает или задает имя 
        /// </summary>
        [Required]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает стоимость продукта в рублях.
        /// </summary>
        [Required]
        [Column("price")]
        public float Price { get; set; }
    }
}