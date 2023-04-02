using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase.Interfaces;
using SmartCarWashDemo.Services.DataBase;

namespace SmartCarWashDemoTests
{
    public class SalesPointDbTests
    {
        private const string Name = "Test name";
        private readonly Dictionary<long, int> _products = new() { { 1, 10 }, { 2, 100 } };
        private ISalesPointsDataBase _context;

        [SetUp]
        public void SetUp()
        {
            _context = new DataBaseContext(DbUtilities.Options);
            _context.ReInitDatabase();
        }

        [Test]
        [Description("Добавление новой единицы продукции.")]
        public void AddNewProductTest()
        {
            Assert.That(_context.SalesPoints.Any(), Is.False);
            _context.AddPoint(Name, _products);

            Assert.That(_context.SalesPoints.Count(), Is.EqualTo(1));

            var customer = _context.SalesPoints.First();
            Assert.That(customer.Name, Is.EqualTo(Name));
            Assert.That(customer.ProvidedProducts.Count, Is.EqualTo(2));

            var first = customer.ProvidedProducts.SingleOrDefault(p => p.ProductId == 1);
            Assert.That(first, Is.Not.Null);
            Assert.That(first.ProductQuantity, Is.EqualTo(10));

            var second = customer.ProvidedProducts.SingleOrDefault(p => p.ProductId == 2);
            Assert.That(second, Is.Not.Null);
            Assert.That(second.ProductQuantity, Is.EqualTo(100));
        }

        [Test]
        [Description("Обновление сведений о существующем единице продукции.")]
        public void UpdateProductTest()
        {
            const string newName = "New name";
            _context.AddPoint(Name, _products);
            Assert.That(_context.SalesPoints.Count(), Is.EqualTo(1));

            var customer = _context.SalesPoints.First();
            Assert.That(customer.Name, Is.EqualTo(Name));
            _context.UpdatePoint(customer.Id, newName, new Dictionary<long, int> { { 3, 50 } });

            Assert.That(_context.SalesPoints.First().Name, Is.EqualTo(newName));
            Assert.That(customer.ProvidedProducts.Count, Is.EqualTo(1));

            var product = customer.ProvidedProducts.First();
            Assert.That(product.ProductId, Is.EqualTo(3));
            Assert.That(product.ProductQuantity, Is.EqualTo(50));
        }

        [Test]
        [Description("Удаление единицы продукции.")]
        public void RemoveProductTest()
        {
            const string secondName = "Second name";
            _context.AddPoint(Name, _products);
            var id = _context.SalesPoints.First().Id;

            _context.AddPoint(secondName, new Dictionary<long, int>());

            Assert.That(_context.SalesPoints.Count(), Is.EqualTo(2));

            _context.RemovePoint(id);
            Assert.That(_context.SalesPoints.Count(), Is.EqualTo(1));
            Assert.That(_context.SalesPoints.First().Name, Is.EqualTo(secondName));
        }

        [Test]
        [Description("Получение единицы продукции с заданным идентификатором")]
        public void GetProductTest()
        {
            const string secondName = "Second name";
            _context.AddPoint(Name, _products);
            _context.AddPoint(secondName, new Dictionary<long, int>());

            var first = _context.SalesPoints.Single(customer => customer.Name == Name);
            var second = _context.SalesPoints.Single(customer => customer.Name == secondName);

            Assert.That(_context.GetPoint(first.Id).Name, Is.EqualTo(first.Name));
            Assert.That(_context.GetPoint(second.Id).Name, Is.EqualTo(second.Name));
        }

        [Test]
        [Description("Вызов исключения при попытке обратиться к элементу коллекции единицы продукции.")]
        public void ThrowNotFoundExceptionTest()
        {
            Assert.That(_context.SalesPoints, Is.Empty);
            Assert.Throws<SalesPointEntityNotFoundException>(() => _context.RemovePoint(1));
            Assert.Throws<SalesPointEntityNotFoundException>(() => _context.GetPoint(1));
            Assert.Throws<SalesPointEntityNotFoundException>(() => _context.UpdatePoint(1, string.Empty, new Dictionary<long, int>()));
        }
    }
}