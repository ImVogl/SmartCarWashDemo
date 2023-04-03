using System;
using System.Collections.Generic;
using System.Linq;
using SmartCarWashDemo.Model;
using SmartCarWashDemo.Services.DataBase.Interfaces;

namespace SmartCarWashDemo
{
    /// <summary>
    /// Заполнение сведений в таблицу.
    /// </summary>
    public class CreateTables
    {
        /// <summary>
        /// Контекст базы данных покупателей.
        /// </summary>
        private readonly ICustomersDataBase _customersDb;

        /// <summary>
        /// Контекст базы данных продукции.
        /// </summary>
        private readonly IProductsDataBase _productsDb;

        /// <summary>
        /// Контекст базы данных актов продаж.
        /// </summary>
        private readonly ISalesDataBase _salesDb;

        /// <summary>
        /// Контекст базы точек продаж.
        /// </summary>
        private readonly ISalesPointsDataBase _salesPointsDb;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CreateTables"/>.
        /// </summary>
        /// <param name="customersDb">Контекст базы данных покупателей.</param>
        /// <param name="productsDb">Контекст базы данных продукции.</param>
        /// <param name="salesDb">Контекст базы данных актов продаж.</param>
        /// <param name="salesPointsDb">Контекст базы точек продаж.</param>
        public CreateTables(
            ICustomersDataBase customersDb,
            IProductsDataBase productsDb,
            ISalesDataBase salesDb,
            ISalesPointsDataBase salesPointsDb)
        {
            _customersDb = customersDb;
            _productsDb = productsDb;
            _salesDb = salesDb;
            _salesPointsDb = salesPointsDb;
        }

        /// <summary>
        /// Заполнение таблиц тестовыми данными.
        /// </summary>
        public void InitTables()
        {
            _customersDb.ReInitDatabase();
            _customersDb.AddCustomer("Иван");
            var peterId = _customersDb.AddCustomer("Петр");

            var tomatoId = _productsDb.AddProduct("Помидоры", 180f);
            var cucumberId = _productsDb.AddProduct("Огурцы", 69.99f);
            var carrotId = _productsDb.AddProduct("Морковь", 44.99f);
            var onionId = _productsDb.AddProduct("Лук", 65.99f);
            var potatoId = _productsDb.AddProduct("Картофель", 34.99f);
            var garlicId = _productsDb.AddProduct("Чеснок", 349.99f);
            var cabbageId = _productsDb.AddProduct("Капуста", 24.99f);
            var pepperId = _productsDb.AddProduct("Перец", 359.99f);
            var beetId = _productsDb.AddProduct("Свекла", 29.99f);
            var eggplantId = _productsDb.AddProduct("Баклажан", 209.99f);

            var appleId = _productsDb.AddProduct("Яблоки", 89.99f);
            var pearId = _productsDb.AddProduct("Груши", 159.99f);
            var orangeId = _productsDb.AddProduct("Апельсины", 109.99f);
            var bananaId = _productsDb.AddProduct("Бананы", 99.99f);
            var grapeId = _productsDb.AddProduct("Виноград", 299.99f);

            var fruits = new Dictionary<long, int>
            {
                { appleId, 50 },
                { pearId, 45 },
                { orangeId, 32 },
                { bananaId, 27 },
                { grapeId, 7 }
            };

            var fruitPointId = _salesPointsDb.AddPoint("Фруктовая лавка", fruits);
            var vegetables = new Dictionary<long, int>
            {
                { tomatoId, 20 },
                { cucumberId, 25 },
                { carrotId, 15 },
                { onionId, 15 },
                { potatoId, 500 },
                { garlicId, 5 },
                { cabbageId, 10 },
                { pepperId, 10 },
                { beetId, 40 },
                { eggplantId, 15 }
            };

            _salesPointsDb.AddPoint("Овощная лавка", vegetables);
            var foodShopId = _salesPointsDb.AddPoint(
                "Универмаг",
                vegetables.Concat(fruits).ToDictionary(item => item.Key, item => item.Value));

            var sale = new SaleInfo
            {
                SalesPointId = fruitPointId,
                CustomerId = null,
                Date = DateTime.Now,
                Time = TimeSpan.FromHours(14),
                SalesData = new List<SaleDataInfo>
                {
                    new () { ProductId = appleId, ProductQuantity = 10},
                    new () { ProductId = orangeId, ProductQuantity = 8 },
                    new () { ProductId = pearId, ProductQuantity = 5 }
                }
            };

            var salePeter = new SaleInfo
            {
                SalesPointId = foodShopId,
                CustomerId = peterId,
                Date = DateTime.Now,
                Time = TimeSpan.FromHours(14),
                SalesData = new List<SaleDataInfo>
                {
                    new () { ProductId = tomatoId, ProductQuantity = 10 },
                    new () { ProductId = cucumberId, ProductQuantity = 8 },
                    new () { ProductId = carrotId, ProductQuantity = 5 }
                }
            };

            _salesDb.AddSale(sale);
            _salesDb.AddSale(salePeter);
        }
    }
}