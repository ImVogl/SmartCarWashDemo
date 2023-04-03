using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Model.DataBase.Sales;
using SmartCarWashDemo.Model.Exceptions;
using System.Linq;
using System;
using JetBrains.Annotations;
using SmartCarWashDemo.Model;
using SmartCarWashDemo.Services.DataBase.Interfaces;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей актов продаж.
    /// </summary>
    public partial class DataBaseContext : ISalesDataBase
    {
        /// <inheritdoc />
        public DbSet<Sale> Sales { get; set; }

        /// <inheritdoc />
        public long AddSale(SaleInfo info)
        {
            var sale = Sales.Add(ConvertInfoToEntity(info));
            SaveChanges();
            return sale.Entity.Id;
        }

        /// <inheritdoc />
        public void UpdateSale(SaleInfo info)
        {
            var sale = GetSaleInternal(info.Id);
            var update = ConvertInfoToEntity(info);
            sale.Customer = update.Customer;
            sale.Date = update.Date;
            sale.SalesData = update.SalesData;
            sale.SalesPoint = update.SalesPoint;
            sale.Time = update.Time;
            sale.TotalAmount = update.TotalAmount;

            SaveChanges();
        }

        /// <inheritdoc />
        public void RemoveSale(long id)
        {
            var sale = GetSaleInternal(id);
            Sales.Remove(sale);
            SaveChanges();
        }

        /// <inheritdoc />
        public Sale GetSale(long id)
        {
            return GetSaleInternal(id);
        }

        /// <summary>
        /// Построение таблиц актов продаж.
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/>.</param>
        private void CreatingSaleModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleData>().HasIndex(data => data.Id).IsUnique();
            modelBuilder.Entity<SaleData>().Property(data => data.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sale>().HasIndex(sale => sale.Id).IsUnique();
            modelBuilder.Entity<Sale>().Property(sale => sale.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sale>().Property(sale => sale.Date).IsRequired();
            modelBuilder.Entity<Sale>().Property(sale => sale.Time).IsRequired();
            modelBuilder.Entity<Sale>().Property(sale => sale.TotalAmount).IsRequired();
            modelBuilder.Entity<Sale>().Property(sale => sale.CustomerId).IsRequired(false);
            modelBuilder.Entity<Sale>()
                .HasMany(sale => sale.SalesData)
                .WithOne(data => data.Sale)
                .HasForeignKey(data => data.SaleId)
                .IsRequired();
        }

        /// <summary>
        /// Получение акта продажи.
        /// </summary>
        /// <param name="id">Идентификатор акта продажи.</param>
        /// <returns><see cref="Sale"/>.</returns>
        [NotNull]
        private Sale GetSaleInternal(long id)
        {
            try {
                return Sales
                    .Include(sale => sale.Customer)
                    .Include(sale => sale.SalesPoint)
                    .Include(sale => sale.SalesData)
                    .SingleOrDefault(sale => sale.Id == id)
                       ?? throw new SaleEntityNotFoundException();
            }
            catch (InvalidOperationException) {
                Logger.Error($"В базе данных обнаружено более одного акта продажи с идентификатором {id}");
                throw;
            }
        }

        /// <summary>
        /// Преобразует <see cref="SaleInfo"/> в <see cref="Sale"/>.
        /// </summary>
        /// <param name="info"><see cref="SaleInfo"/>.</param>
        /// <returns><see cref="Sale"/>.</returns>
        private Sale ConvertInfoToEntity(SaleInfo info)
        {
            var prices = info.SalesData.Select(data => GetProduct(data.ProductId)).ToDictionary(item => item.Id, item => item.Price);
            return new Sale
            {
                Customer = info.CustomerId != null ? GetCustomerInternal((long)info.CustomerId) : null,
                Date = info.Date,
                SalesPoint = GetPointInternal(info.SalesPointId),
                Time = info.Time,
                TotalAmount = info.SalesData.Aggregate(0f, (sum, data) => sum + prices[data.ProductId] * data.ProductQuantity),
                SalesData = info.SalesData.Select(data => new SaleData
                {
                    ProductAmount = prices[data.ProductId] * data.ProductQuantity,
                    ProductId = data.ProductId,
                    ProductQuantity = data.ProductQuantity
                }).ToList()
            };
        }
    }
}