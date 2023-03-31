using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Model.DataBase.Sales;
using SmartCarWashDemo.Model;

namespace SmartCarWashDemo.Services.DataBase.Interfaces
{
    /// <summary>
    /// Интерфейс базы данных акта продажи.
    /// </summary>
    public interface ISalesDataBase
    {
        /// <summary>
        /// Добавление новой точки продажи в базу данных.
        /// </summary>
        /// <param name="entity"><see cref="Sale"/>.</param>
        void AddSale(SaleInfo entity);

        /// <summary>
        /// Обновление сведений о точке продажи в базе данных.
        /// </summary>
        /// <param name="entity"><see cref="Sale"/>.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для точки продажи.</exception>
        void UpdateSale(SaleInfo entity);

        /// <summary>
        /// Удаляет точку продажи из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор точки продажи.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для точки продажи.</exception>
        void RemoveSale(long id);

        /// <summary>
        /// Получает экземпляр точки продажи.
        /// </summary>
        /// <param name="id">Идентификатор точки продажи.</param>
        /// <returns><see cref="Sale"/>.</returns>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для точки продажи.</exception>
        Sale GetSale(long id);
    }
}