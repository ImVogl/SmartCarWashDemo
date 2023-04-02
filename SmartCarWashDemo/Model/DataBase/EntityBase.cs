using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarWashDemo.Model.DataBase
{
    /// <summary>
    /// Базовый класс с моделью сущности базы данных.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Получает или задает идентификатор сущности.
        /// </summary>
        [Column("id")]
        public long Id { get; set; } = default;

    }
}