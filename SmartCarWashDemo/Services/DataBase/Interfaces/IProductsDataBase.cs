using SmartCarWashDemo.Model.DataBase;
using SmartCarWashDemo.Model.Exceptions;

namespace SmartCarWashDemo.Services.DataBase.Interfaces
{
    /// <summary>
    /// Интерфейс базы данных продукции.
    /// </summary>
    public interface IProductsDataBase
    {
        /// <summary>
        /// Добавление нового продукта в базу данных.
        /// </summary>
        /// <param name="name">Имя продукта.</param>
        /// <param name="price">Стоимость продукта.</param>
        void AddProduct(string name, float price);

        /// <summary>
        /// Обновление сведений о продукте.
        /// </summary>
        /// <param name="id">Идентификатор обновляемого продукта.</param>
        /// <param name="name">Имя обновляемого продукта.</param>
        /// <param name="price">Цена обновляемого продукта.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для продукта.</exception>
        void UpdateProduct(long id, string name, float price);

        /// <summary>
        /// Удаляет продукт из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для продукта.</exception>
        void RemoveProduct(long id);

        /// <summary>
        /// Получает экземпляр продукта.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns><see cref="Product"/>.</returns>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для продукта.</exception>
        Product GetProduct(long id);
    }
}