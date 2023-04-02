using System;
using System.Collections.Generic;
using NUnit.Framework;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase.Interfaces;
using SmartCarWashDemo.Services.DataBase;
using System.Linq;
using SmartCarWashDemo.Model;

namespace SmartCarWashDemoTests
{
    public class SalesDbTests
    {
        private ISalesDataBase _context;
        private SaleInfo _info;

        [SetUp]
        public void SetUp()
        {
            _context = new DataBaseContext(DbUtilities.Options);
            _context.ReInitDatabase();
            
            var pointContext = ((ISalesPointsDataBase)_context);
            pointContext.AddPoint("Test", new Dictionary<long, int>());
            var pointId = pointContext.SalesPoints.First().Id;
            _info = new SaleInfo
            {
                SalesPointId = pointId,
                CustomerId = null,
                Date = DateTime.Now,
                SalesData = new List<SaleDataInfo>(),
                Time = TimeSpan.FromHours(13),
                TotalAmount = 0f
            };
        }

        [Test]
        [Description("Добавление нового акта продажи.")]
        public void AddNewSaleTest()
        {
            Assert.That(_context.Sales.Any(), Is.False);
            _context.AddSale(_info);

            Assert.That(_context.Sales.Count(), Is.EqualTo(1));

            var sale = _context.Sales.First();
            Assert.That(sale.Id, Is.EqualTo(1));
        }

        [Test]
        [Description("Обновление сведений о существующем акте продажи.")]
        public void UpdateSaleTest()
        {
            _info.TotalAmount = 100f;
            var id = _context.AddSale(_info);
            Assert.That(_context.Sales.Count(), Is.EqualTo(1));
            Assert.That(id, Is.EqualTo(1));

            _info.TotalAmount = 50f;
            _info.Id = id;
            _context.UpdateSale(_info);

            var amount = _context.Sales.First().TotalAmount;
            Assert.That(amount, Is.GreaterThan(49f));
            Assert.That(amount, Is.LessThan(51f));
        }

        [Test]
        [Description("Удаление акта продажи.")]
        public void RemoveSaleTest()
        {
            _info.TotalAmount = 100f;
            var id = _context.AddSale(_info);

            _info.TotalAmount = 50f;
            _context.AddSale(_info);

            Assert.That(_context.Sales.Count(), Is.EqualTo(2));

            _context.RemoveSale(id);
            Assert.That(_context.Sales.Count(), Is.EqualTo(1));

            var amount = _context.Sales.First().TotalAmount;
            Assert.That(amount, Is.GreaterThan(49f));
            Assert.That(amount, Is.LessThan(51f));
        }

        [Test]
        [Description("Получение акта продажи с заданным идентификатором")]
        public void GetSaleTest()
        {
            _info.TotalAmount = 100f;
            var firstId = _context.AddSale(_info);

            _info.TotalAmount = 50f;
            var secondId = _context.AddSale(_info);

            var first = _context.Sales.Single(sale => sale.Id == firstId);
            var second = _context.Sales.Single(sale => sale.Id == secondId);

            var firstAmount = _context.GetSale(first.Id).TotalAmount;
            Assert.That(firstAmount, Is.GreaterThan(99f));
            Assert.That(firstAmount, Is.LessThan(101f));

            var secondAmount = _context.GetSale(second.Id).TotalAmount;
            Assert.That(secondAmount, Is.GreaterThan(49f));
            Assert.That(secondAmount, Is.LessThan(51f));
        }

        [Test]
        [Description("Вызов исключения при попытке обратиться к элементу коллекции единицы продукции.")]
        public void ThrowNotFoundExceptionTest()
        {
            const int unknownId = 100;
            Assert.That(_context.Sales, Is.Empty);
            Assert.Throws<SaleEntityNotFoundException>(() => _context.RemoveSale(unknownId));
            Assert.Throws<SaleEntityNotFoundException>(() => _context.GetSale(unknownId));
            Assert.Throws<SaleEntityNotFoundException>(() => _context.UpdateSale(new SaleInfo { Id = unknownId }));

            _info.CustomerId = unknownId;
            _info.SalesPointId = 1;
            Assert.Throws<CustomerEntityNotFoundException>(() => _context.AddSale(_info));

            _info.CustomerId = null;
            _info.SalesPointId = unknownId;
            Assert.Throws<SalesPointEntityNotFoundException>(() => _context.AddSale(_info));
        }
    }
}