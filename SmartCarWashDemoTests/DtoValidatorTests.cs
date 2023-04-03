using System;
using System.Collections.Generic;
using NUnit.Framework;
using SmartCarWashDemo.Model.Dto;
using SmartCarWashDemo.Model.Dto.Sales;
using SmartCarWashDemo.Services.Validators;

namespace SmartCarWashDemoTests
{
    public class DtoValidatorTests
    {
        private const string ValidName = "Not Empty";
        private static readonly IDtoValidator DtoValidator = new DtoValidator();
        
        [Test]
        [Description("Проход всех кейсов для DTO покупателя.")]
        public void CustomerDtoValidationTest()
        {
            Assert.That(DtoValidator.Validate((CustomerDto)null), Is.False);
            Assert.That(DtoValidator.Validate(new CustomerDto{ Name = string.Empty, Id = 10, RegistrationDateTime = DateTime.Now }), Is.False);
            Assert.That(DtoValidator.Validate(new CustomerDto{ Name = ValidName, Id = 10, RegistrationDateTime = DateTime.Now }), Is.True);
        }

        [Test]
        [Description("Проход всех кейсов для DTO продукта.")]
        public void ProductDtoValidationTest()
        {
            Assert.That(DtoValidator.Validate((ProductDto)null), Is.False);
            Assert.That(DtoValidator.Validate(new ProductDto { Name = string.Empty, Id = 10, Price = 100f }), Is.False);
            Assert.That(DtoValidator.Validate(new ProductDto { Name = ValidName, Id = 10, Price = -100f }), Is.False);
            Assert.That(DtoValidator.Validate(new ProductDto { Name = ValidName, Id = 10, Price = 100f }), Is.True);
        }

        [Test]
        [Description("Проход всех кейсов для DTO точек продаж.")]
        public void SalesPointDtoValidationTest()
        {
            Assert.That(DtoValidator.Validate((SalesPointDto)null), Is.False);
            Assert.That(DtoValidator.Validate(new SalesPointDto { Name = string.Empty, Id = 10, ProvidedProducts = new Dictionary<long, int>()}), Is.False);
            Assert.That(DtoValidator.Validate(new SalesPointDto { Name = ValidName, Id = 10, ProvidedProducts = null }), Is.False);
            Assert.That(DtoValidator.Validate(new SalesPointDto { Name = ValidName, Id = 10, ProvidedProducts = new Dictionary<long, int>() }), Is.True);
        }

        [Test]
        [Description("Проход всех кейсов для DTO актов продаж.")]
        public void SaleDtoValidationTest()
        {
            Assert.That(DtoValidator.Validate((SaleDto)null), Is.False);
            Assert.That(
                DtoValidator.Validate(new SaleDto
                {
                    Id = 10,
                    SalesData = null,
                    CustomerId = null,
                    SalesPointId = 10
                }), Is.False);

            Assert.That(
                DtoValidator.Validate(new SaleDto
                {
                    Id = 10,
                    SalesData = new List<SaleDataBaseDto>(),
                    CustomerId = null,
                    SalesPointId = -10,
                }), Is.False);

            Assert.That(
                DtoValidator.Validate(new SaleDto
                {
                    Id = 10,
                    SalesData = new List<SaleDataBaseDto>(),
                    CustomerId = null,
                    SalesPointId = 10
                }), Is.True);
        }

        [Test]
        [Description("Проход всех кейсов для полных DTO актов продаж.")]
        public void FullSaleDtoValidationTest()
        {
            Assert.That(DtoValidator.Validate((ResultSaleDto)null), Is.False);
            Assert.That(
                DtoValidator.Validate(new ResultSaleDto
                {
                    Id = 10,
                    SalesData = null,
                    TotalAmount = 10f,
                    CustomerId = null,
                    SalesPointId = 10,
                    SellDateTime = DateTime.Now
                }), Is.False);

            Assert.That(
                DtoValidator.Validate(new ResultSaleDto
                {
                    Id = 10,
                    SalesData = new List<ResultSaleDataDto>(),
                    TotalAmount = 10f,
                    CustomerId = null,
                    SalesPointId = -10,
                    SellDateTime = DateTime.Now
                }), Is.False);

            Assert.That(
                DtoValidator.Validate(new ResultSaleDto
                {
                    Id = 10,
                    SalesData = new List<ResultSaleDataDto>(),
                    TotalAmount = 10f,
                    CustomerId = null,
                    SalesPointId = 10,
                    SellDateTime = DateTime.Now
                }), Is.True);
        }
    }
}