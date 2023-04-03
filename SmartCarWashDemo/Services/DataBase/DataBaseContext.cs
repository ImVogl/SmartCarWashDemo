using Microsoft.EntityFrameworkCore;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Общий контекст базы данных.
    /// </summary>
    public partial class DataBaseContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DataBaseContext"/>.
        /// </summary>
        /// <param name="options"></param>
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        
        /// <inheritdoc />
        public void ReInitDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CreatingCustomerModel(modelBuilder);
            CreatingProductModel(modelBuilder);
            CreatingSaleModel(modelBuilder);
            CreatingSalesPointModel(modelBuilder);
        }
    }
}