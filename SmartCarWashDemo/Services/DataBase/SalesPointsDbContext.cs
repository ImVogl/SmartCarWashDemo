using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Model.DataBase.Point;
using SmartCarWashDemo.Model.Exceptions;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей точек продаж.
    /// </summary>
    public partial class DataBaseContext
    {
        /// <summary>
        /// Коллекция entity <see cref="SalesPoint"/>.
        /// </summary>
        private DbSet<SalesPoint> _salesPoints;

        /// <inheritdoc />
        public void AddPoint(string name, Dictionary<long, int> products)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void UpdatePoint(long id, string name, Dictionary<long, int> products)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void RemovePoint(long id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public SalesPoint GetPoint(long id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получение точки продажи.
        /// </summary>
        /// <param name="id">Идентификатор точки продажи.</param>
        /// <returns><see cref="SalesPoint"/>.</returns>
        [NotNull]
        private SalesPoint GetPointInternal(long id)
        {
            try
            {
                return _salesPoints
                           .SingleOrDefault(sale => sale.Id == id)
                       ?? throw new EntityNotFoundException();
            }
            catch (InvalidOperationException)
            {
                Logger.Error($"В базе данных обнаружено более одной точки продажи с идентификатором {id}");
                throw;
            }
        }

    }
}