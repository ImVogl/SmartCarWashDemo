using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using SmartCarWashDemo.Model.Dto;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase.Interfaces;
using SmartCarWashDemo.Services.Validators;

namespace SmartCarWashDemo.Controllers
{
    /// <summary>
    /// Контроллер работы с покупателями.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Логгер для <see cref="CustomerController"/>.
        /// </summary>
        private static readonly  ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Экземпляр <see cref="ICustomersDataBase"/>.
        /// </summary>
        private readonly ICustomersDataBase _db;

        /// <summary>
        /// Валидатор входящих аргументов запроса.
        /// </summary>
        private readonly IDtoValidator _validator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CustomerController"/>.
        /// </summary>
        /// <param name="db">Экземпляр <see cref="ICustomersDataBase"/>.</param>
        /// <param name="validator">Валидатор входящих аргументов запроса.</param>
        public CustomerController(ICustomersDataBase db, IDtoValidator validator)
        {
            _db = db;
            _validator = validator;
        }

        /// <summary>
        /// Добавление нового покупателя.
        /// </summary>
        /// <param name="dto">Сведения о покупателе.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Добавлен новый пользователь.</response>
        /// <response code="400">Введено некорректное имя покупателя.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("~/[controller]/add/{name}")]
        public IActionResult Add([FromBody] CustomerDto dto)
        {
            Logger.Debug($"Получен запрос на добавление нового покупателя с именем {dto.Name}");
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            try {
                _db.AddCustomer(dto.Name, dto.SalesIds);
                return Ok();
            }
            catch {
                Logger.Error("Не удалось добавить нового пользователя");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Обновление сведений о покупателе.
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200">Сведения о пользователе обновлены.</response>
        /// <response code="400">Введено некорректные сведения о покупателе или пользователь не найден.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        /// <returns>Результат выполнения запроса.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("~/[controller]/update")]
        public IActionResult Update([FromBody] CustomerDto dto)
        {
            Logger.Debug($"Получен запрос на обновление сведений о покупателе с идентификатором {dto.Id}");
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            try {
                _db.UpdateCustomer(dto.Id, dto.Name, dto.SalesIds);
                return Ok();
            }
            catch (EntityNotFoundException) {
                Logger.Warn($"Не удалось найти пользователя с идентификатором {dto.Id}");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось обновить сведения о пользователе");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Удаление покупателя.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Пользователь удален.</response>
        /// <response code="400">Пользователь не найден.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("~/[controller]/remove/{id}")]
        public IActionResult Remove(long id)
        {
            Logger.Debug($"Получен запрос на удаление покупателя с идентификатором {id}");
            try {
                _db.RemoveCustomer(id);
                return Ok();
            }
            catch (EntityNotFoundException) {
                Logger.Warn($"Не удалось найти пользователя с идентификатором {id}");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось добавить обновить сведения о пользователе");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Получение сведений о покупателе по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Пользователь удален.</response>
        /// <response code="400">Пользователь не найден.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("~/[controller]/get/{id}")]
        public IActionResult Get(long id)
        {
            Logger.Debug($"Получен запрос на получение сведений о покупателе с идентификатором {id}");
            try {
                var customer = _db.GetCustomer(id);
                
                // Можно через AutoMapper, к примеру, но для тестового задания, полагаю, это будет избыточным.
                return Ok(new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    RegistrationDateTime = customer.CreationDateTime,
                    SalesIds = customer.SalesIds
                });
            }
            catch (EntityNotFoundException) {
                Logger.Warn($"Не удалось найти пользователя с идентификатором {id}");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось добавить обновить сведения о пользователе");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Логгирование ошибки, связанной с некорректным DTO.
        /// </summary>
        /// <param name="dto"><see cref="CustomerDto"/>.</param>
        private void LogBadDto(CustomerDto dto)
        {
            var message = string.Format("Сведения о пользователе некорректны. Полученные сведения: {0}; {1}; {2}",
                $"идентификатор  - {dto.Id}",
                $"имя - {dto.Name}",
                dto.SalesIds == null
                    ? "идентификаторы проданных товаров - null"
                    : $"идентификаторы проданных товаров - {string.Join("; ", dto.SalesIds)}");

            Logger.Warn(message);
        }
    }
}
