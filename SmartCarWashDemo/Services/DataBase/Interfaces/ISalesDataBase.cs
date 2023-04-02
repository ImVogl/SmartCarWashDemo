using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Model.DataBase.Sales;
using SmartCarWashDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace SmartCarWashDemo.Services.DataBase.Interfaces
{
    /// <summary>
    /// Интерфейс базы данных акта продажи.
    /// </summary>
    public interface ISalesDataBase : IDataBase
    {
        /// <summary>
        /// Коллекция entity <see cref="Sale"/>.
        /// </summary>
        DbSet<Sale> Sales { get; set; }

        /// <summary>
        /// Добавление нового акта продажи в базу данных.
        /// </summary>
        /// <param name="entity"><see cref="Sale"/>.</param>
        /// <returns>Идентификатор добавленной сущности.</returns>
        /// <exception cref="CustomerEntityNotFoundException"><see cref="CustomerEntityNotFoundException"/>.</exception>
        /// <exception cref="SalesPointEntityNotFoundException"><see cref="SalesPointEntityNotFoundException"/>.</exception>
        long AddSale(SaleInfo entity);

        /// <summary>
        /// Обновление сведений об акте продажи в базе данных.
        /// </summary>
        /// <param name="entity"><see cref="Sale"/>.</param>
        /// <exception cref="SaleEntityNotFoundException"><see cref="SaleEntityNotFoundException"/>.</exception>
        void UpdateSale(SaleInfo entity);

        /// <summary>
        /// Удаляет акт продажи из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор акта продажи.</param>
        /// <exception cref="SaleEntityNotFoundException"><see cref="SaleEntityNotFoundException"/>.</exception>
        void RemoveSale(long id);

        /// <summary>
        /// Получает экземпляр акт продажи.
        /// </summary>
        /// <param name="id">Идентификатор акта продажи.</param>
        /// <returns><see cref="Sale"/>.</returns>
        /// <exception cref="SaleEntityNotFoundException"><see cref="SaleEntityNotFoundException"/>.</exception>
        Sale GetSale(long id);
    }
}