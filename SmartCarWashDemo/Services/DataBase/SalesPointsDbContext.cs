using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Model.DataBase.Point;
using SmartCarWashDemo.Model.Exceptions;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей точек продаж.
    /// </summary>
    public partial class DataBaseContext
    {
        /// <summary>
        /// Коллекция entity <see cref="SalesPoint"/>.
        /// </summary>
        private DbSet<SalesPoint> _salesPoints;

        /// <inheritdoc />
        public void AddPoint(string name, Dictionary<long, int> products)
        {
            _salesPoints.Add(new SalesPoint
            {
                Name = name,
                ProvidedProducts = products.Select(pair => new ProvidedProduct { ProductId = pair.Key, ProductQuantity = pair.Value }).ToList()
            });

            SaveChanges();
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
            _salesPoints.Remove(point);
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
            modelBuilder.Entity<SalesPoint>().HasIndex(point => point.Id).IsUnique();
            modelBuilder.Entity<SalesPoint>().Property(point => point.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<SalesPoint>().HasMany(point => point.ProvidedProducts).WithOne().IsRequired();
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
                return _salesPoints
                           .Include(point => point.ProvidedProducts)
                           .SingleOrDefault(sale => sale.Id == id)
                       ?? throw new EntityNotFoundException();
            }
            catch (InvalidOperationException)
            {
                Logger.Error($"В базе данных обнаружено более одной точки продажи с идентификатором {id}");
                throw;
            }
        }

    }
}