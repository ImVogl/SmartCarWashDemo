using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartCarWashDemo.Services.DataBase;
using SmartCarWashDemo.Services.Validators;
using SmartCarWashDemo.Services.DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SmartCarWashDemo
{
    /// <summary>
    /// Класс с имплементацией основных настроек приложения.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">Сущность со сведениями из конфигурационного файла приложения</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Получает сущность со сведениями из конфигурационного файла приложения.
        /// </summary>
        public IConfiguration Configuration { get; }
        
        /// <summary>
        /// Конфигурация служб.
        /// </summary>
        /// <param name="services">Экземпляр <see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureContainer(services);
            ConfigureSwagger(services);
        }
        
        /// <summary>
        /// Настройка приложения.
        /// </summary>
        /// <param name="app">Экземпляр <see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">Экземпляр <see cref="IWebHostEnvironment"/>.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartCarWashDemo v1"));
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Регистрация зависимостей.
        /// </summary>
        /// <param name="services">Экземпляр <see cref="IServiceCollection"/>.</param>
        private void ConfigureContainer(IServiceCollection services)
        {
            var option = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "smart_car_wash")
                .EnableSensitiveDataLogging()
                .Options;

            services.AddScoped<IDtoValidator, DtoValidator>();
            services.AddScoped(_ => new DataBaseContext(option));
            services.AddScoped<ICustomersDataBase>(provider => provider.GetRequiredService<DataBaseContext>());
            services.AddScoped<IProductsDataBase>(provider => provider.GetRequiredService<DataBaseContext>());
            services.AddScoped<ISalesDataBase>(provider => provider.GetRequiredService<DataBaseContext>());
            services.AddScoped<ISalesPointsDataBase>(provider => provider.GetRequiredService<DataBaseContext>());
        }

        /// <summary>
        /// Настройка Swagger.
        /// </summary>
        /// <param name="services">Экземпляр <see cref="IServiceCollection"/>.</param>
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartCarWashDemo", Version = "v1" });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
