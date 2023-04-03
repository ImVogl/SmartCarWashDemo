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
    /// ����� � �������������� �������� �������� ����������.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// �������������� ����� ��������� <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">�������� �� ���������� �� ����������������� ����� ����������</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// �������� �������� �� ���������� �� ����������������� ����� ����������.
        /// </summary>
        public IConfiguration Configuration { get; }
        
        /// <summary>
        /// ������������ �����.
        /// </summary>
        /// <param name="services">��������� <see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureContainer(services);
            ConfigureSwagger(services);
        }
        
        /// <summary>
        /// ��������� ����������.
        /// </summary>
        /// <param name="app">��������� <see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">��������� <see cref="IWebHostEnvironment"/>.</param>
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

            using var scope = app.ApplicationServices.CreateScope();
            var createTablesService = new CreateTables(
            scope.ServiceProvider.GetRequiredService<ICustomersDataBase>(),
            scope.ServiceProvider.GetRequiredService<IProductsDataBase>(),
            scope.ServiceProvider.GetRequiredService<ISalesDataBase>(),
            scope.ServiceProvider.GetRequiredService<ISalesPointsDataBase>());
            
            createTablesService.InitTables();
        }

        /// <summary>
        /// ����������� ������������.
        /// </summary>
        /// <param name="services">��������� <see cref="IServiceCollection"/>.</param>
        private void ConfigureContainer(IServiceCollection services)
        {
            services.AddScoped<IDtoValidator, DtoValidator>();
            services.AddDbContext<DataBaseContext>((_, options) => options.UseInMemoryDatabase("smart_car_washing").EnableSensitiveDataLogging());
            services.AddScoped<ICustomersDataBase>(provider => provider.GetRequiredService<DataBaseContext>());
            services.AddScoped<IProductsDataBase>(provider => provider.GetRequiredService<DataBaseContext>());
            services.AddScoped<ISalesDataBase>(provider => provider.GetRequiredService<DataBaseContext>());
            services.AddScoped<ISalesPointsDataBase>(provider => provider.GetRequiredService<DataBaseContext>());
        }

        /// <summary>
        /// ��������� Swagger.
        /// </summary>
        /// <param name="services">��������� <see cref="IServiceCollection"/>.</param>
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
