﻿using Microsoft.EntityFrameworkCore;
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
        void AddCustomer(string name);

        /// <summary>
        /// Обновление сведений о покупателе.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <param name="name">Имя покупателя.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для покупателя.</exception>
        void UpdateCustomer(long id, string name);

        /// <summary>
        /// Удаление из базы данных покупателя.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для покупателя.</exception>
        void RemoveCustomer(long id);

        /// <summary>
        /// Получение покупателя с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <returns><see cref="Customer"/>.</returns>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для покупателя.</exception>
        Customer GetCustomer(long id);
    }
}