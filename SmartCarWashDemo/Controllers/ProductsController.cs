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
    /// Контроллер работы с продукцией.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Логгер для <see cref="ProductsController"/>.
        /// </summary>
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="IProductsDataBase"/>.
        /// </summary>
        private readonly IProductsDataBase _db;

        /// <summary>
        /// Экземпляр <see cref="IDtoValidator"/>.
        /// </summary>
        private readonly IDtoValidator _validator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ProductsController"/>.
        /// </summary>
        /// <param name="db">Инициализирует новый экземпляр <see cref="IProductsDataBase"/>.</param>
        /// <param name="validator">Экземпляр <see cref="IDtoValidator"/>.</param>
        public ProductsController(IProductsDataBase db, IDtoValidator validator)
        {
            _db = db;
            _validator = validator;
        }

        /// <summary>
        /// Добавление нового продукта.
        /// </summary>
        /// <param name="dto">Сведения о добавляемом продукте.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Добавлен новый продукт.</response>
        /// <response code="400">Введено некорректное название или стоимость продукта.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("~/[controller]/add")]
        public IActionResult Add([FromBody] ProductDto dto)
        {
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            Logger.Debug($"Получен запрос на добавление нового продукта {dto.Name} со стоимостью {dto.Price:F} рублей");
            try {
                _db.AddProduct(dto.Name, dto.Price);
                return Ok();
            }
            catch {
                Logger.Error("Не удалось добавить новый продукт");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Обновление сведений о существующем продукте.
        /// </summary>
        /// <param name="dto"><see cref="ProductDto"/>.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Продукт был обновлен.</response>
        /// <response code="400">Введено некорректное название или стоимость продукта.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("~/[controller]/update")]
        public IActionResult Update([FromBody] ProductDto dto)
        {
            if (!_validator.Validate(dto))
            {
                LogBadDto(dto);
                return BadRequest();
            }

            Logger.Debug($"Получен запрос на обновление продукта с идентификатором {dto.Id}, именем {dto.Name} и со стоимостью {dto.Price:F} рублей");
            try {
                _db.UpdateProduct(dto.Id, dto.Name, dto.Price);
                return Ok();
            }
            catch (ProductEntityNotFoundException) {
                Logger.Warn($"Не удалось обновить сведения о продукте с идентификатором {dto.Id}, так как продукт с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось обновить сведения о продукте");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Удаление продукта.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Продукт был удален.</response>
        /// <response code="400">Не удалось найти продукт с заданным идентификатором.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("~/[controller]/remove/{id}")]
        public IActionResult Remove(long id)
        {
            Logger.Debug($"Получен запрос на удаление продукта с идентификатором {id}");
            try {
                _db.RemoveProduct(id);
                return Ok();
            }
            catch (ProductEntityNotFoundException) {
                Logger.Warn($"Не удалось удалить продукт с идентификатором {id}, так как продукт с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось удалить продукт");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Получение сведений о продукте с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns>Результат выполнения запроса.</returns>
        /// <response code="200">Получены сведения о продукте.</response>
        /// <response code="400">Не удалось найти продукт с заданным идентификатором.</response>
        /// <response code="500">Неизвестная ошибка.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("~/[controller]/get/{id}")]
        public IActionResult Get(long id)
        {
            Logger.Debug($"Получен запрос на получение товара с идентификатором {id}");
            try {
                var product = _db.GetProduct(id);
                return Ok(new ProductDto { Id = product.Id, Name = product.Name, Price = product.Price });
            }
            catch (ProductEntityNotFoundException) {
                Logger.Warn($"Не удалось получить продукт с идентификатором {id}, так как продукт с таким идентификатором не существует.");
                return BadRequest();
            }
            catch {
                Logger.Error("Не удалось получить продукт");
                return CommonUtils.InternalServerError();
            }
        }

        /// <summary>
        /// Логгирование ошибки, связанной с некорректным DTO.
        /// </summary>
        /// <param name="dto"><see cref="ProductDto"/>.</param>
        private void LogBadDto(ProductDto dto)
        {
            var message = string.Format("Сведения о продукте некорректны. Полученные сведения: {0}; {1}; {2}",
                $"идентификатор  - {dto.Id}",
                $"имя - {dto.Name}",
                $"цена - {dto.Price}");

            Logger.Warn(message);
        }
    }
}
