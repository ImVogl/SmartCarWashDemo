using NUnit.Framework;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase;
using SmartCarWashDemo.Services.DataBase.Interfaces;
using System.Linq;

namespace SmartCarWashDemoTests
{
    public class ProductsDbTests
    {
        private const string Name = "Test name";
        private IProductsDataBase _context;

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
            Assert.That(_context.Products.Any(), Is.False);
            _context.AddProduct(Name, 100f);

            Assert.That(_context.Products.Count(), Is.EqualTo(1));

            var customer = _context.Products.First();
            Assert.That(customer.Name, Is.EqualTo(Name));
            Assert.That(customer.Price, Is.GreaterThan(99f));
            Assert.That(customer.Price, Is.LessThan(101f));
        }

        [Test]
        [Description("Обновление сведений о существующем единице продукции.")]
        public void UpdateProductTest()
        {
            const string newName = "New name";
            _context.AddProduct(Name, 100f);
            Assert.That(_context.Products.Count(), Is.EqualTo(1));

            var customer = _context.Products.First();
            Assert.That(customer.Name, Is.EqualTo(Name));
            _context.UpdateProduct(customer.Id, newName, 100f);

            Assert.That(_context.Products.First().Name, Is.EqualTo(newName));
        }

        [Test]
        [Description("Удаление единицы продукции.")]
        public void RemoveProductTest()
        {
            const string secondName = "Second name";
            _context.AddProduct(Name, 100f);
            var id = _context.Products.First().Id;

            _context.AddProduct(secondName, 100f);

            Assert.That(_context.Products.Count(), Is.EqualTo(2));

            _context.RemoveProduct(id);
            Assert.That(_context.Products.Count(), Is.EqualTo(1));
            Assert.That(_context.Products.First().Name, Is.EqualTo(secondName));
        }

        [Test]
        [Description("Получение единицы продукции с заданным идентификатором")]
        public void GetProductTest()
        {
            const string secondName = "Second name";
            _context.AddProduct(Name, 100f);
            _context.AddProduct(secondName, 100f);

            var first = _context.Products.Single(customer => customer.Name == Name);
            var second = _context.Products.Single(customer => customer.Name == secondName);

            Assert.That(_context.GetProduct(first.Id).Name, Is.EqualTo(first.Name));
            Assert.That(_context.GetProduct(second.Id).Name, Is.EqualTo(second.Name));
        }

        [Test]
        [Description("Вызов исключения при попытке обратиться к элементу коллекции единицы продукции.")]
        public void ThrowNotFoundExceptionTest()
        {
            Assert.That(_context.Products, Is.Empty);
            Assert.Throws<ProductEntityNotFoundException>(() => _context.RemoveProduct(1));
            Assert.Throws<ProductEntityNotFoundException>(() => _context.GetProduct(1));
            Assert.Throws<ProductEntityNotFoundException>(() => _context.UpdateProduct(1, string.Empty, 0f));
        }
    }
}