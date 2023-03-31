using SmartCarWashDemo.Model.DataBase;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей продукции.
    /// </summary>
    public partial class DataBaseContext
    {
        /// <inheritdoc />
        public void AddProduct(string name, float price)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void UpdateProduct(long id, string name, float price)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void RemoveProduct(long id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Product GetProduct(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}