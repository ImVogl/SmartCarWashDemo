using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Services.DataBase;

namespace SmartCarWashDemoTests
{
    public class DbUtilities
    {
        public static readonly DbContextOptions Options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "smart_car_washing")
            .EnableSensitiveDataLogging()
            .Options;
    }
}