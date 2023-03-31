namespace SmartCarWashDemo.Services.DataBase.Interfaces
{
    /// <summary>
    /// Интерфейс базы данных.
    /// </summary>
    public interface IDataBase : ICustomersDataBase, IProductsDataBase, ISalesDataBase, ISalesPointsDataBase
    {
    }
}