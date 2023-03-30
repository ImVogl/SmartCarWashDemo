using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
        /// ��������� Swagger.
        /// </summary>
        /// <param name="services">��������� <see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartCarWashDemo", Version = "v1" });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

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
        }
    }
}
