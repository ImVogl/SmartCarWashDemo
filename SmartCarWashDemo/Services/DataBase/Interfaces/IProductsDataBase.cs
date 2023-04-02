using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Model.DataBase;
using SmartCarWashDemo.Model.Exceptions;

namespace SmartCarWashDemo.Services.DataBase.Interfaces
{
    /// <summary>
    /// Интерфейс базы данных продукции.
    /// </summary>
    public interface IProductsDataBase : IDataBase
    {
        /// <summary>
        /// Получает или задает коллекцию entity <see cref="Product"/>.
        /// </summary>
        DbSet<Product> Products { get; set; }

        /// <summary>
        /// Добавление нового продукта в базу данных.
        /// </summary>
        /// <param name="name">Имя продукта.</param>
        /// <param name="price">Стоимость продукта.</param>
        /// <returns>Идентификатор добавленного товара.</returns>
        long AddProduct(string name, float price);

        /// <summary>
        /// Обновление сведений о продукте.
        /// </summary>
        /// <param name="id">Идентификатор обновляемого продукта.</param>
        /// <param name="name">Имя обновляемого продукта.</param>
        /// <param name="price">Цена обновляемого продукта.</param>
        /// <exception cref="ProductEntityNotFoundException"><see cref="ProductEntityNotFoundException"/>.</exception>
        void UpdateProduct(long id, string name, float price);

        /// <summary>
        /// Удаляет продукт из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <exception cref="ProductEntityNotFoundException"><see cref="ProductEntityNotFoundException"/>.</exception>
        void RemoveProduct(long id);

        /// <summary>
        /// Получает экземпляр продукта.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns><see cref="Product"/>.</returns>
        /// <exception cref="ProductEntityNotFoundException"><see cref="ProductEntityNotFoundException"/>.</exception>
        Product GetProduct(long id);
    }
}