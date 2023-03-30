using System.Collections.Generic;
using SmartCarWashDemo.Model.DataBase;
using SmartCarWashDemo.Model.Exceptions;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Интерфейс базы данных пользователей
    /// </summary>
    public interface ICustomersDataBase
    {
        /// <summary>
        /// Добавление в базу данных нового покупателя.
        /// </summary>
        /// <param name="name">Имя покупателя.</param>
        /// <param name="saleIds">Коллекция идентификаторов, сделанных покупателем покупок.</param>
        void AddCustomer(string name, IEnumerable<long> saleIds);

        /// <summary>
        /// Обновление сведений о покупателе.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <param name="name">Имя покупателя.</param>
        /// <param name="saleIds">Коллекция идентификаторов, сделанных покупателем покупок.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для покупателя.</exception>
        void UpdateCustomer(long id, string name, IEnumerable<long> saleIds);

        /// <summary>
        /// Удаление из базы данных покупателя.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для покупателя.</exception>
        void RemoveUser(long id);

        /// <summary>
        /// Получение покупателя с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <returns><see cref="Customer"/>.</returns>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для покупателя.</exception>
        Customer GetCustomer(long id);
    }
}