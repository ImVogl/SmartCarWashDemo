using System;
using System.Collections.Generic;

namespace SmartCarWashDemo.Model.DataBase
{
    /// <summary>
    /// Модель сущности базы данных покупатель.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Получает или задает идентификатор пользователя.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Получает или задает имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает коллекцию всех идентификаторов сделанных пользователем покупок.
        /// </summary>
        public ICollection<long> SalesIds { get; set; }

        /// <summary>
        /// Получает или задает день и время, когда был создан пользователь.
        /// </summary>
        public DateTime CreationDateTime { get; set; }
    }
}