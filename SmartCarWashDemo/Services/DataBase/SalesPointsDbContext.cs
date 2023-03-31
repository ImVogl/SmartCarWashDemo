using System.Collections.Generic;
using SmartCarWashDemo.Model.DataBase.Point;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей точек продаж.
    /// </summary>
    public partial class DataBaseContext
    {
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
    }
}