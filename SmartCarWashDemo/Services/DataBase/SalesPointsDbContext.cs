using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Model.DataBase.Point;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase.Interfaces;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей точек продаж.
    /// </summary>
    public partial class DataBaseContext : ISalesPointsDataBase
    {
        /// <inheritdoc />
        public DbSet<SalesPoint> SalesPoints { get; set; }

        /// <inheritdoc />
        public long AddPoint(string name, Dictionary<long, int> products)
        {
            var point = new SalesPoint 
            { 
                Name = name,
                ProvidedProducts = products.Select(pair => new ProvidedProduct { ProductId = pair.Key, ProductQuantity = pair.Value }).ToList()
            };

            var pointEntity = SalesPoints.Add(point);

            SaveChanges();
            return pointEntity.Entity.Id;
        }

        /// <inheritdoc />
        public void UpdatePoint(long id, string name, Dictionary<long, int> products)
        {
            var point = GetPointInternal(id);
            point.Name = name;
            point.ProvidedProducts = products.Select(pair => new ProvidedProduct
                { ProductId = pair.Key, ProductQuantity = pair.Value }).ToList();

            SaveChanges();
        }

        /// <inheritdoc />
        public void RemovePoint(long id)
        {
            var point = GetPointInternal(id);
            SalesPoints.Remove(point);
            SaveChanges();
        }

        /// <inheritdoc />
        public SalesPoint GetPoint(long id)
        {
            return GetPointInternal(id);
        }

        /// <summary>
        /// Построение таблиц точек продаж.
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/>.</param>
        private void CreatingSalesPointModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProvidedProduct>().HasIndex(product => product.Id).IsUnique();
            modelBuilder.Entity<ProvidedProduct>().Property(product => product.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProvidedProduct>().Property(product => product.ProductId).IsRequired();
            modelBuilder.Entity<ProvidedProduct>().Property(product => product.ProductQuantity).IsRequired();
            
            modelBuilder.Entity<SalesPoint>().HasIndex(point => point.Id).IsUnique();
            modelBuilder.Entity<SalesPoint>().Property(point => point.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<SalesPoint>()
                .HasMany(point => point.Sales)
                .WithOne(sale => sale.SalesPoint)
                .HasForeignKey(sale => sale.SalesPointId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<SalesPoint>()
                .HasMany(point => point.ProvidedProducts)
                .WithOne(product => product.Point)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }

        /// <summary>
        /// Получение точки продажи.
        /// </summary>
        /// <param name="id">Идентификатор точки продажи.</param>
        /// <returns><see cref="SalesPoint"/>.</returns>
        [NotNull]
        private SalesPoint GetPointInternal(long id)
        {
            try
            {
                return SalesPoints
                           .Include(point => point.ProvidedProducts)
                           .SingleOrDefault(sale => sale.Id == id)
                       ?? throw new SalesPointEntityNotFoundException();
            }
            catch (InvalidOperationException)
            {
                Logger.Error($"В базе данных обнаружено более одной точки продажи с идентификатором {id}");
                throw;
            }
        }

    }
}