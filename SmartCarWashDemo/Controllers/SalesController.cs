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
        /// Экземпляр <see cref="ICustomersDataBase"/>.
        /// </summary>
        private readonly ICustomersDataBase _customerDb;

        /// <summary>
        /// Экземпляр <see cref="ISalesPointsDataBase"/>.
        /// </summary>
        private readonly ISalesPointsDataBase _pointsDataBase;

        /// <summary>
        /// Экземпляр <see cref="IDtoValidator"/>.
        /// </summary>
        private readonly IDtoValidator _validator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SalesController"/>.
        /// </summary>
        /// <param name="db">Экземпляр <see cref="ISalesDataBase"/>.</param>
        /// <param name="customerDb">Экземпляр <see cref="ICustomersDataBase"/>.</param>
        /// <param name="pointsDataBase">Экземпляр <see cref="ISalesPointsDataBase"/>.</param>
        /// <param name="validator">Экземпляр <see cref="IDtoValidator"/>.</param>
        public SalesController(ISalesDataBase db, ICustomersDataBase customerDb, ISalesPointsDataBase pointsDataBase, IDtoValidator validator)
        {
            _db = db;
            _customerDb = customerDb;
            _pointsDataBase = pointsDataBase;
            _validator = validator;
        }

        /// <summary>
        /// Добавление нового акта продажи.
        /// </summary>
        /// <param name="dto">Сведения об акте продажи.</param>
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
        /// <param name="dto">Сведения об акте продажи.</param>
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultSaleDto))]
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
        /// Отправка сведений о проданных товарах.
        /// </summary>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Добавлена новый акт продажи.</response>
        /// <response code="400">DTO содержит некорректные значения полей.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("~/[controller]/sale")]
        public IActionResult Sale([FromBody] SaleDto dto)
        {
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            Logger.Debug("Получен запрос Со сведениями о проданных товарах");
            long newSaleId = -1;
            try {
                var targetPoint = _pointsDataBase.GetPoint(dto.SalesPointId);
                var pointProducts = targetPoint.ProvidedProducts.ToDictionary(product => product.ProductId,
                    product => product.ProductQuantity);
                var dtoProducts = dto.SalesData.ToDictionary(data => data.ProductId);
                var notFoundIds = dtoProducts.Keys.Except(pointProducts.Keys);
                if (notFoundIds.Any())
                {
                    Logger.Warn($"Отсутствуют товары с идентификаторами {string.Join(" ", notFoundIds)}");
                    return BadRequest();
                }

                foreach (var key in dtoProducts.Keys)
                {
                    pointProducts[key] -= dtoProducts[key].ProductQuantity;
                    if (pointProducts[key] >= 0)
                        continue;

                    Logger.Warn($"В указанной точке продажи не удалось обнаружить товар с идентификатром {key}");
                    return BadRequest();
                }

                _pointsDataBase.UpdatePoint(targetPoint.Id, targetPoint.Name, pointProducts);
                newSaleId = _db.AddSale(ConvertDtoToInfo(dto));
                if (dto.CustomerId != null)
                    _customerDb.AddSale((long)dto.CustomerId, newSaleId);

                return Ok();
            }
            catch (SaleEntityNotFoundException) {
                Logger.Warn($"не удалось найти акт продажи с идентификатором {newSaleId}");
                return BadRequest();
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
        /// Логгирование ошибки, связанной с некорректным DTO.
        /// </summary>
        /// <param name="dto"><see cref="ResultSaleDto"/>.</param>
        private void LogBadDto(SaleDto dto)
        {
            var salesDataMessage = dto.SalesData
                .Aggregate(
                    string.Empty,
                    (result, data) =>
                        result +
                        $"Идентификатор продукта {data.ProductId}; общее число проданных товаров {data.ProductQuantity};\r\n");

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
        /// <param name="dto"><see cref="ResultSaleDto"/>.</param>
        /// <returns><see cref="SaleInfo"/>.</returns>
        private SaleInfo ConvertDtoToInfo(SaleDto dto)
        {
            var now = DateTime.Now;
            return new SaleInfo
            {
                Id = dto.Id ?? -1,
                SalesPointId = dto.SalesPointId,
                CustomerId = dto.CustomerId,
                SalesData = dto.SalesData.Select(data => new SaleDataInfo
                {
                    ProductId = data.ProductId,
                    ProductQuantity = data.ProductQuantity
                }).ToList(),

                Date = now.Date,
                Time = now.TimeOfDay
            };
        }

        /// <summary>
        /// Преобразует сущность база данных в DTO.
        /// </summary>
        /// <param name="entity"><see cref="Sale"/>.</param>
        /// <returns><see cref="ResultSaleDto"/>.</returns>
        private ResultSaleDto ConvertEntityToDto(Sale entity)
        {
            return new ResultSaleDto
            {
                Id = entity.Id,
                SalesPointId = entity.SalesPoint.Id,
                CustomerId = entity.Customer?.Id,
                TotalAmount = entity.TotalAmount,
                SellDateTime = entity.Date.Add(entity.Time),
                SalesData = entity.SalesData
                    .Select(data =>
                        new ResultSaleDataDto
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
