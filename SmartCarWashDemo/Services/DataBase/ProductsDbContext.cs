using Microsoft.EntityFrameworkCore;
using SmartCarWashDemo.Model.DataBase;
using SmartCarWashDemo.Model.Exceptions;
using System.Linq;
using System;
using JetBrains.Annotations;

namespace SmartCarWashDemo.Services.DataBase
{
    /// <summary>
    /// Контекст базы данных: часть, связанная с таблицей продукции.
    /// </summary>
    public partial class DataBaseContext
    {
        /// <summary>
        /// Коллекция entity <see cref="Product"/>.
        /// </summary>
        private DbSet<Product> _products;

        /// <inheritdoc />
        public void AddProduct(string name, float price)
        {
            _products.Add(new Product { Name = name, Price = price });
            SaveChanges();
        }

        /// <inheritdoc />
        public void UpdateProduct(long id, string name, float price)
        {
            var product = GetProductInternal(id);
            product.Name = name;
            product.Price = price;
            SaveChanges();
        }

        /// <inheritdoc />
        public void RemoveProduct(long id)
        {
            var product = GetProductInternal(id);
            _products.Remove(product);
            SaveChanges();
        }

        /// <inheritdoc />
        public Product GetProduct(long id)
        {
            return GetProductInternal(id);
        }

        /// <summary>
        /// Построение таблиц продуктов.
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/>.</param>
        private void CreatingProductModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(product => product.Id).IsUnique();
            modelBuilder.Entity<Product>().Property(product => product.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>().Property(product => product.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(product => product.Price).IsRequired();
        }

        /// <summary>
        /// Получение продукта.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns><see cref="Product"/>.</returns>
        [NotNull]
        private Product GetProductInternal(long id)
        {
            try {
                return _products.SingleOrDefault(customer => customer.Id == id) ?? throw new EntityNotFoundException();
            }
            catch (InvalidOperationException) {
                Logger.Error($"В базе данных обнаружено более одного продукта с идентификатором {id}");
                throw;
            }
        }
    }
}