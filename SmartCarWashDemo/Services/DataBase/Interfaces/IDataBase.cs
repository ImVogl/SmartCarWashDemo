namespace SmartCarWashDemo.Services.DataBase.Interfaces
{
    /// <summary>
    /// Интерфейс базы данных.
    /// </summary>
    public interface IDataBase
    {
        /// <summary>
        /// Пересоздание базы данных.
        /// </summary>
        void ReInitDatabase();
    }
}