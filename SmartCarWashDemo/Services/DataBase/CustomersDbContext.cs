using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NLog;
using SmartCarWashDemo.Model.DataBase;
using SmartCarWashDemo.Model.DataBase.Sales;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase.Interfaces;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей покупателей.
    /// </summary>
    public partial class DataBaseContext : DbContext, IDataBase
    {
        /// <summary>
        /// Логгер данного класса.
        /// </summary>
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Коллекция entity <see cref="Customer"/>.
        /// </summary>
        private DbSet<Customer> _customers;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DataBaseContext"/>.
        /// </summary>
        /// <param name="options"></param>
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc />
        public void AddCustomer(string name, IEnumerable<long> saleIds)
        {
            var sales = GetSalesWithIds(saleIds);
            _customers.Add(new Customer { Name = name, CreationDateTime = DateTime.Now, Sales = sales });
            SaveChanges();
        }

        /// <inheritdoc />
        public void UpdateCustomer(long id, string name, IEnumerable<long> saleIds)
        {
            var customer = GetCustomerInternal(id);
            customer.Name = name;
            customer.Sales = GetSalesWithIds(saleIds);
            SaveChanges();
        }

        /// <inheritdoc />
        public void RemoveCustomer(long id)
        {
            var customer = GetCustomerInternal(id);
            _customers.Remove(customer);
            SaveChanges();
        }

        /// <inheritdoc />
        public Customer GetCustomer(long id)
        {
            return GetCustomerInternal(id);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CreatingCustomerModel(modelBuilder);
        }

        /// <summary>
        /// Построение таблиц покупателя.
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/>.</param>
        private void CreatingCustomerModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasIndex(customer => customer.Id).IsUnique();
            modelBuilder.Entity<Customer>().Property(customer => customer.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>().Property(customer => customer.Name).IsRequired();
            modelBuilder.Entity<Customer>().Property(customer => customer.CreationDateTime).IsRequired();
            modelBuilder.Entity<Customer>().Property(customer => customer.Sales).IsRequired();
            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.Sales)
                .WithOne()
                .HasForeignKey(sale => sale.CustomerId)
                .IsRequired();
        }

        /// <summary>
        /// Получение покупателя.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <returns><see cref="Customer"/>.</returns>
        [NotNull]
        private Customer GetCustomerInternal(long id)
        {
            try {
                return _customers
                           .Include(customer => customer.Sales)
                           .SingleOrDefault(customer => customer.Id == id)
                       ?? throw new EntityNotFoundException();
            }
            catch (InvalidOperationException) {
                Logger.Error($"В базе данных обнаружено более одного пользователя в идентификатором {id}");
                throw;
            }
        }

        /// <summary>
        /// Получение список <see cref="Sale"/>.
        /// </summary>
        /// <param name="saleIds">Идентификаторы актов продаж.</param>
        /// <returns>Список <see cref="Sale"/>.</returns>
        private List<Sale> GetSalesWithIds(IEnumerable<long> saleIds)
        {
            var sales = new List<Sale>();
            foreach (var id in saleIds)
            {
                try {
                    sales.Add(GetSale(id));
                }
                catch (EntityNotFoundException) {
                    Logger.Warn($"При попытке добавить нового пользователя не удалось добавить акт продажи с идентификатором {id}");
                }
            }

            return sales;
        }
    }
}