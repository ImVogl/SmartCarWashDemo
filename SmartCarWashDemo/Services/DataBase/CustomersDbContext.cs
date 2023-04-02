using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
    public partial class DataBaseContext : ICustomersDataBase
    {
        /// <summary>
        /// Логгер данного класса.
        /// </summary>
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <inheritdoc />
        public DbSet<Customer> Customers { get; set; }

        /// <inheritdoc />
        public void AddCustomer(string name)
        {
            Customers.Add(new Customer { Name = name, CreationDateTime = DateTime.Now, Sales = new List<Sale>() });
            SaveChanges();
        }

        /// <inheritdoc />
        public void UpdateCustomer(long id, string name, IEnumerable<long> saleIds)
        {
            var customer = GetCustomerInternal(id);
            customer.Name = name;
            customer.Sales.Clear();
            customer.Sales = Sales.ToList().Where(sale => saleIds.Contains(sale.Id)).ToList();

            SaveChanges();
        }

        /// <inheritdoc />
        public void AddSale(long id, long saleId)
        {
            var customer = GetCustomerInternal(id);
            customer.Sales.Add(GetSaleInternal(saleId));

            SaveChanges();
        }

        /// <inheritdoc />
        public void RemoveCustomer(long id)
        {
            var customer = GetCustomerInternal(id);
            Customers.Remove(customer);
            SaveChanges();
        }

        /// <inheritdoc />
        public Customer GetCustomer(long id)
        {
            return GetCustomerInternal(id);
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
            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.Sales)
                .WithOne(sale => sale.Customer)
                .HasForeignKey(sale => sale.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
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
                return Customers
                           .Include(customer => customer.Sales)
                           .SingleOrDefault(customer => customer.Id == id)
                       ?? throw new CustomerEntityNotFoundException();
            }
            catch (InvalidOperationException) {
                Logger.Error($"В базе данных обнаружено более одного пользователя с идентификатором {id}");
                throw;
            }
        }
    }
}