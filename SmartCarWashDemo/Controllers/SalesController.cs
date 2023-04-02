using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using SmartCarWashDemo.Model;
using SmartCarWashDemo.Model.DataBase.Sales;
using SmartCarWashDemo.Model.Dto.Sales;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase.Interfaces;
using SmartCarWashDemo.Services.Validators;

namespace SmartCarWashDemo.Controllers
{
    /// <summary>
    /// Контроллер работы с актами продаж.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        /// <summary>
        /// Логгер для данного класса.
        /// </summary>
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Экземпляр <see cref="ISalesDataBase"/>.
        /// </summary>
        private readonly ISalesDataBase _db;

        /// <summary>
        /// Экземпляр <see cref="IDtoValidator"/>.
        /// </summary>
        private readonly IDtoValidator _validator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SalesController"/>.
        /// </summary>
        /// <param name="db">Экземпляр <see cref="ISalesDataBase"/>.</param>
        /// <param name="validator">Экземпляр <see cref="IDtoValidator"/>.</param>
        public SalesController(ISalesDataBase db, IDtoValidator validator)
        {
            _db = db;
            _validator = validator;
        }

        /// <summary>
        /// Добавление нового акта продажи.
        /// </summary>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Добавлена новый акт продажи.</response>
        /// <response code="400">DTO содержит некорректные значения полей.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("~/[controller]/add")]
        public IActionResult Add([FromBody] SaleDto dto)
        {
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            Logger.Debug("Получен запрос на добавление нового акта продажи");
            try {
                _db.AddSale(ConvertDtoToInfo(dto));
                return Ok();
            }
            catch (CustomerEntityNotFoundException) {
                Logger.Warn($"не удалось найти пользователя с идентификатором {dto.CustomerId}");
                return BadRequest();
            }
            catch (SalesPointEntityNotFoundException) {
                Logger.Warn($"не удалось найти точку продажи с идентификатором {dto.SalesPointId}");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось добавить новый акт продажи");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Обновление сведений о существующем акте продажи.
        /// </summary>
        /// <param name="dto"><see cref="SaleDto"/>.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Акт продажи был обновлен.</response>
        /// <response code="400">Введено некорректное название сведений акта продажи.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("~/[controller]/update")]
        public IActionResult Update([FromBody] SaleDto dto)
        {
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            Logger.Debug($"Получен запрос на обновление акта продажи с идентификатором {dto.Id}");
            try {
                _db.UpdateSale(ConvertDtoToInfo(dto));
                return Ok();
            }
            catch (SaleEntityNotFoundException) {
                Logger.Warn($"Не удалось обновить сведения об акте продажи с идентификатором {dto.Id}, так как акта продажи с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось добавить новый акт продажи");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Удаление акта продажи.
        /// </summary>
        /// <param name="id">Идентификатор акта продажи.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Акт продажи был удален.</response>
        /// <response code="400">Не удалось найти акт продажи с заданным идентификатором.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [HttpDelete("~/[controller]/remove/{id}")]
        public IActionResult Remove(long id)
        {
            Logger.Debug($"Получен запрос на удаление акта продажи с идентификатором {id}");
            try {
                _db.RemoveSale(id);
                return Ok();
            }
            catch (SaleEntityNotFoundException) {
                Logger.Warn($"Не удалось удалить акт продажи с идентификатором {id}, так как акта продажи с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось добавить новый акт продажи");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Получение сведений об акте продажи с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор акта продажи.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Получены сведения об акте продажи.</response>
        /// <response code="400">Не удалось найти акт продажи с заданным идентификатором.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("~/[controller]/get/{id}")]
        public IActionResult Get(long id)
        {
            Logger.Debug($"Получен запрос на получение акта продажи с идентификатором {id}");
            try
            {
                var sale = _db.GetSale(id);
                return Ok(ConvertEntityToDto(sale));
            }
            catch (SaleEntityNotFoundException) {
                Logger.Warn($"Не удалось получить акт продажи с идентификатором {id}, так как акта продажи с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось добавить новый акт продажи");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Логгирование ошибки, связанной с некорректным DTO.
        /// </summary>
        /// <param name="dto"><see cref="SaleDto"/>.</param>
        private void LogBadDto(SaleDto dto)
        {
            var salesDataMessage = dto.SalesData
                .Aggregate(
                    string.Empty,
                    (result, data) =>
                        result +
                        $"Идентификатор продукта {data.ProductId}; общее число проданных товаров {data.ProductQuantity}; общая стоимость товаров {data.ProductAmount};\r\n");

            var message =
                string.Format(
                    "Сведения об акте продажи некорректны. Полученные сведения:{0}{1}{2}{3}",
                    $" идентификатор  - {dto.Id};",
                    $" идентификатор точки продажи - {dto.SalesPointId};",
                    dto.CustomerId == null ? string.Empty : $" {dto.CustomerId};",
                    salesDataMessage);

            Logger.Warn(message);
        }

        /// <summary>
        /// Преобразование DTO в сущность базы данных.
        /// </summary>
        /// <param name="dto"><see cref="SaleDto"/>.</param>
        /// <returns><see cref="SaleInfo"/>.</returns>
        private SaleInfo ConvertDtoToInfo(SaleDto dto)
        {
            var salesData = dto.SalesData
                .Select(data => new SaleData { ProductId = data.ProductId, ProductAmount = data.ProductAmount, ProductQuantity = data.ProductQuantity }).ToList();

            var now = DateTime.Now;
            return new SaleInfo
            {
                Id = dto.Id,
                SalesPointId = dto.SalesPointId,
                CustomerId = dto.CustomerId,
                SalesData = salesData.Select(data => new SaleDataInfo
                {
                    ProductAmount = data.ProductAmount, ProductId = data.ProductId,
                    ProductQuantity = data.ProductQuantity
                }).ToList(),

                Date = now.Date,
                Time = now.TimeOfDay,
                TotalAmount = salesData.Aggregate(0f, (sum, item) => sum + item.ProductAmount)
            };
        }

        /// <summary>
        /// Преобразует сущность база данных в DTO.
        /// </summary>
        /// <param name="entity"><see cref="Sale"/>.</param>
        /// <returns><see cref="SaleDto"/>.</returns>
        private SaleDto ConvertEntityToDto(Sale entity)
        {
            return new SaleDto
            {
                Id = entity.Id,
                SalesPointId = entity.SalesPoint.Id,
                CustomerId = entity.Customer?.Id,
                TotalAmount = entity.TotalAmount,
                SellDateTime = entity.Date.Add(entity.Time),
                SalesData = entity.SalesData
                    .Select(data =>
                        new SaleDataDto
                        {
                            ProductId = data.ProductId,
                            ProductQuantity = data.ProductQuantity,
                            ProductAmount = data.ProductAmount
                        })
                    .ToList(),
            };
        }
    }
}
