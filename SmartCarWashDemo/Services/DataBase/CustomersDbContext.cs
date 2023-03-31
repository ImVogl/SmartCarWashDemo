using System.Collections.Generic;
using SmartCarWashDemo.Model.DataBase;
using SmartCarWashDemo.Services.DataBase.Interfaces;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей покупателей.
    /// </summary>
    public partial class DataBaseContext : IDataBase
    {
        /// <inheritdoc />
        public void AddCustomer(string name, IEnumerable<long> saleIds)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void UpdateCustomer(long id, string name, IEnumerable<long> saleIds)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void RemoveCustomer(long id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Customer GetCustomer(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}