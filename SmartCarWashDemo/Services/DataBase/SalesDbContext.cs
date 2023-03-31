using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Model.DataBase.Sales;
using SmartCarWashDemo.Model.Exceptions;
using System.Linq;
using System;
using JetBrains.Annotations;
using SmartCarWashDemo.Model;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей актов продаж.
    /// </summary>
    public partial class DataBaseContext
    {
        /// <summary>
        /// Коллекция entity <see cref="Sale"/>.
        /// </summary>
        private DbSet<Sale> _sales;

        /// <inheritdoc />
        public void AddSale(SaleInfo info)
        {
            _sales.Add(ConvertInfoToEntity(info));
            SaveChanges();
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
            _sales.Remove(sale);
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
            modelBuilder.Entity<Sale>().HasIndex(sale => sale.Id).IsUnique();
            modelBuilder.Entity<Sale>().Property(sale => sale.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sale>().Property(sale => sale.Date).IsRequired();
            modelBuilder.Entity<Sale>().Property(sale => sale.Time).IsRequired();
            modelBuilder.Entity<Sale>().Property(sale => sale.TotalAmount).IsRequired();
            modelBuilder.Entity<Sale>().Property(sale => sale.SalesData).IsRequired();
            modelBuilder.Entity<Sale>().HasOne(sale => sale.SalesPoint).WithOne().IsRequired();
            modelBuilder.Entity<Sale>().HasOne(sale => sale.Customer).WithOne().IsRequired(false);
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
                return _sales
                    .Include(sale => sale.Customer)
                    .Include(sale => sale.SalesPoint)
                    .SingleOrDefault(sale => sale.Id == id)
                       ?? throw new EntityNotFoundException();
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
            return new Sale
            {
                Customer = info.CustomerId != null ? GetCustomerInternal((long)info.CustomerId) : null,
                Date = info.Date,
                SalesPoint = GetPointInternal(info.SalesPointId),
                Time = info.Time,
                TotalAmount = info.TotalAmount,
                SalesData = info.SalesData.Select(data => new SaleData
                {
                    ProductAmount = data.ProductAmount,
                    ProductId = data.ProductId,
                    ProductQuantity = data.ProductQuantity
                }).ToList()
            };
        }
    }
}