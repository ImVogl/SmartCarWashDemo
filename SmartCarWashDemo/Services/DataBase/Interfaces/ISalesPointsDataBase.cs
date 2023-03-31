using SmartCarWashDemo.Model.DataBase.Point;
using SmartCarWashDemo.Model.Exceptions;
using System.Collections.Generic;

namespace SmartCarWashDemo.Services.DataBase.Interfaces
{
    /// <summary>
    /// Интерфейс базы данных точек продажи продукции.
    /// </summary>
    public interface ISalesPointsDataBase
    {
        /// <summary>
        /// Добавление новой точки продажи в базу данных.
        /// </summary>
        /// <param name="name">Имя точки.</param>
        /// <param name="products">Словарь с идентификаторами товаров (ключ словаря) на точке и числом товаров (значение словаря) на этой точке.</param>
        void AddPoint(string name, Dictionary<long, int> products);

        /// <summary>
        /// Обновление сведений о точке продажи в базе данных.
        /// </summary>
        /// <param name="id">Идентификатор обновляемой точки.</param>
        /// <param name="name">Имя обновляемой точки.</param>
        /// <param name="products">Словарь с идентификаторами товаров (ключ словаря) на точке и числом товаров (значение словаря) на этой точке.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для точки продажи.</exception>
        void UpdatePoint(long id, string name, Dictionary<long, int> products);

        /// <summary>
        /// Удаляет точку продажи из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор точки продажи.</param>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для точки продажи.</exception>
        void RemovePoint(long id);

        /// <summary>
        /// Получает экземпляр точки продажи.
        /// </summary>
        /// <param name="id">Идентификатор точки продажи.</param>
        /// <returns><see cref="SalesPoint"/>.</returns>
        /// <exception cref="EntityNotFoundException"><see cref="EntityNotFoundException"/> для точки продажи.</exception>
        SalesPoint GetPoint(long id);
    }
}