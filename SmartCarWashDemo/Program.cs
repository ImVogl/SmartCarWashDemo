using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SmartCarWashDemo
{
    /// <summary>
    /// основной класс приложени€.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ¬ходна€ точка приложени€.
        /// </summary>
        /// <param name="args">јргументы запуска приложени€.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ’остинг.
        /// </summary>
        /// <param name="args">јргументы запуска приложени€.</param>
        /// <returns>Ёкземпл€р <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
