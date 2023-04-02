using System.ComponentModel.DataAnnotations.Schema;

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
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает стоимость продукта в рублях.
        /// </summary>
        [Column("price")]
        public float Price { get; set; }
    }
}