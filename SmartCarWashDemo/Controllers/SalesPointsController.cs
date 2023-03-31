using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using SmartCarWashDemo.Model.Dto;
using SmartCarWashDemo.Model.Exceptions;
using SmartCarWashDemo.Services.DataBase;
using SmartCarWashDemo.Services.Validators;

namespace SmartCarWashDemo.Controllers
{
    /// <summary>
    /// Контроллер работы с точками продажи.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesPointsController : ControllerBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Экземпляр <see cref="ISalesPointsDataBase"/>.
        /// </summary>
        private readonly ISalesPointsDataBase _db;

        /// <summary>
        /// Экземпляр <see cref="IDtoValidator"/>.
        /// </summary>
        private readonly IDtoValidator _validator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SalesPointsController"/>.
        /// </summary>
        /// <param name="db">Экземпляр <see cref="ISalesPointsDataBase"/>.</param>
        /// <param name="validator">Экземпляр <see cref="IDtoValidator"/>.</param>
        public SalesPointsController(ISalesPointsDataBase db, IDtoValidator validator)
        {
            _db = db;
            _validator = validator;
        }

        /// <summary>
        /// Добавление новой токи продаж.
        /// </summary>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Добавлен новая точка доступа.</response>
        /// <response code="400">Введено некорректное название точки доступа.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("~/[controller]/add")]
        public IActionResult Add([FromBody] SalesPointDto dto)
        {
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            Logger.Debug($"Получен запрос на добавление новой точки продажи с именем {dto.Name}");
            try {
                _db.AddPoint(dto.Name, dto.ProvidedProducts);
                return Ok();
            }
            catch {
                Logger.Error("Не удалось добавить новую точку продажи");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Обновление сведений о существующей точке продажи.
        /// </summary>
        /// <param name="dto"><see cref="SalesPointDto"/>.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Точка продажи была обновлена.</response>
        /// <response code="400">Введено некорректное название точки продажи.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("~/[controller]/update")]
        public IActionResult Update([FromBody] SalesPointDto dto)
        {
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            Logger.Debug($"Получен запрос на обновление точки продажи с идентификатором {dto.Id} и именем {dto.Name}");
            try {
                _db.UpdatePoint(dto.Id, dto.Name, dto.ProvidedProducts);
                return Ok();
            }
            catch (EntityNotFoundException) {
                Logger.Warn($"Не удалось обновить сведения о точке продажи с идентификатором {dto.Id}, так как точка продажи с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось обновить сведения о точке продажи");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Удаление точки продажи.
        /// </summary>
        /// <param name="id">Идентификатор точки продажи.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Точка продажи была удалена.</response>
        /// <response code="400">Не удалось найти точку продажи с заданным идентификатором.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [HttpDelete("~/[controller]/remove/{id}")]
        public IActionResult Remove(long id)
        {
            Logger.Debug($"Получен запрос на удаление точки продажи с идентификатором {id}");
            try {
                _db.RemovePoint(id);
                return Ok();
            }
            catch (EntityNotFoundException) {
                Logger.Warn($"Не удалось удалить точку продажи с идентификатором {id}, так как точка продажи с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось удалить точку продажи");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Получение сведений о точке продажи с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор точки продажи.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Получены сведения о точке продажи.</response>
        /// <response code="400">Не удалось найти точку продажи с заданным идентификатором.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesPointDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("~/[controller]/get/{id}")]
        public IActionResult Get(long id)
        {
            Logger.Debug($"Получен запрос на получение точки продажи с идентификатором {id}");
            try {
                var point = _db.GetPoint(id);
                return Ok(new SalesPointDto { Id = point.Id, Name = point.Name, ProvidedProducts = point.ProvidedProducts });
            }
            catch (EntityNotFoundException) {
                Logger.Warn($"Не удалось получить точку продажи с идентификатором {id}, так как точка продажи с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось получить точку продажи");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Логгирование ошибки, связанной с некорректным DTO.
        /// </summary>
        /// <param name="dto"><see cref="SalesPointDto"/>.</param>
        private void LogBadDto(SalesPointDto dto)
        {
            var message = $"Сведения о точке продажи некорректны. Полученные сведения: идентификатор  - {dto.Id}; имя - {dto.Name}";
            Logger.Warn(message);
        }
    }
}
