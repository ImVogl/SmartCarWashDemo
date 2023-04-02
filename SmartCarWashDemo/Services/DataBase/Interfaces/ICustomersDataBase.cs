using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Model.DataBase;
using SmartCarWashDemo.Model.Exceptions;

namespace SmartCarWashDemo.Services.DataBase.Interfaces
{
    /// <summary>
    /// Интерфейс базы данных пользователей
    /// </summary>
    public interface ICustomersDataBase : IDataBase
    {
        /// <summary>
        /// Получает или задает коллекцию entity <see cref="Customer"/>.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Добавление в базу данных нового покупателя.
        /// </summary>
        /// <param name="name">Имя покупателя.</param>
        /// <returns>Идентификатор нового покупателя.</returns>
        long AddCustomer(string name);

        /// <summary>
        /// Обновление сведений о покупателе.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <param name="name">Имя покупателя.</param>
        /// <param name="saleIds">Коллекция идентификаторов актов продажи.</param>
        /// <exception cref="CustomerEntityNotFoundException"><see cref="CustomerEntityNotFoundException"/>.</exception>
        void UpdateCustomer(long id, string name, IEnumerable<long> saleIds);

        /// <summary>
        /// Добавляет акт продажи, относящийся к данному пользователю.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="saleId">Идентификатор акта продажи.</param>
        /// <exception cref="CustomerEntityNotFoundException"><see cref="CustomerEntityNotFoundException"/>.</exception>
        /// <exception cref="SaleEntityNotFoundException"><see cref="SaleEntityNotFoundException"/>.</exception>
        void AddSale(long id, long saleId);

        /// <summary>
        /// Удаление из базы данных покупателя.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <exception cref="CustomerEntityNotFoundException"><see cref="CustomerEntityNotFoundException"/>.</exception>
        void RemoveCustomer(long id);

        /// <summary>
        /// Получение покупателя с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <returns><see cref="Customer"/>.</returns>
        /// <exception cref="CustomerEntityNotFoundException"><see cref="CustomerEntityNotFoundException"/>.</exception>
        Customer GetCustomer(long id);
    }
}