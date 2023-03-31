using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase;
using SmartCarWashDemo.Services.DataBase.Interfaces;

namespace SmartCarWashDemoTests
{
    public class CustomersDbTests
    {
        private const string Name = "Test name";
        private static readonly DbContextOptions Options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "smart_car_washing")
            .EnableSensitiveDataLogging()
            .Options;

        private ICustomersDataBase _context;

        [SetUp]
        public void SetUp()
        {
            _context = new DataBaseContext(Options);
            _context.ReInitDatabase();
        }

        [Test]
        [Description("Добавление нового покупателя.")]
        public void AddNewCustomerTest()
        {
            Assert.That(_context.Customers.Any(), Is.False);
            _context.AddCustomer(Name);

            Assert.That(_context.Customers.Count(), Is.EqualTo(1));

            var customer = _context.Customers.First();
            Assert.That(customer.Name, Is.EqualTo(Name));
            Assert.That(customer.Sales, Is.Empty);
        }

        [Test]
        [Description("Обновление сведений о существующем покупателе.")]
        public void UpdateCustomerTest()
        {
            const string newName = "New name";
            _context.AddCustomer(Name);
            Assert.That(_context.Customers.Count(), Is.EqualTo(1));

            var customer = _context.Customers.First();
            Assert.That(customer.Name, Is.EqualTo(Name));
            _context.UpdateCustomer(customer.Id, newName);
            
            Assert.That(_context.Customers.First().Name, Is.EqualTo(newName));
        }

        [Test]
        [Description("Удаление покупателя.")]
        public void RemoveCustomer()
        {
            const string secondName = "Second name";
            _context.AddCustomer(Name);
            var id = _context.Customers.First().Id;

            _context.AddCustomer(secondName);
            
            Assert.That(_context.Customers.Count(), Is.EqualTo(2));

            _context.RemoveCustomer(id);
            Assert.That(_context.Customers.Count(), Is.EqualTo(1));
            Assert.That(_context.Customers.First().Name, Is.EqualTo(secondName));
        }

        [Test]
        [Description("Получение покупателя с заданным идентификатором")]
        public void GetCustomerTest()
        {
            const string secondName = "Second name";
            _context.AddCustomer(Name);
            _context.AddCustomer(secondName);

            var first = _context.Customers.Single(customer => customer.Name == Name);
            var second = _context.Customers.Single(customer => customer.Name == secondName);
            
            Assert.That(_context.GetCustomer(first.Id).Name, Is.EqualTo(first.Name));
            Assert.That(_context.GetCustomer(second.Id).Name, Is.EqualTo(second.Name));
        }

        [Test]
        [Description("Вызов исключения при попытке обратиться к элементу коллекции покупателей.")]
        public void ThrowNotFoundExceptionTest()
        {
            Assert.That(_context.Customers, Is.Empty);
            Assert.Throws<EntityNotFoundException>(() => _context.RemoveCustomer(1));
            Assert.Throws<EntityNotFoundException>(() => _context.GetCustomer(1));
            Assert.Throws<EntityNotFoundException>(() => _context.UpdateCustomer(1, string.Empty));
        }
    }
}