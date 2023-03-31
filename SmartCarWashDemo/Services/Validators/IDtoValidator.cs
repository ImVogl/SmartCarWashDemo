using SmartCarWashDemo.Model.Dto;
using SmartCarWashDemo.Model.Dto.Sales;

namespace SmartCarWashDemo.Services.Validators
{
    /// <summary>
    /// Интерфейс валидатора DTO.
    /// </summary>
    public interface IDtoValidator
    {
        /// <summary>
        /// Проверка значение полей <see cref="CustomerDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="CustomerDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate(CustomerDto dto);

        /// <summary>
        /// Проверка значение полей <see cref="ProductDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="ProductDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate(ProductDto dto);

        /// <summary>
        /// Проверка значение полей <see cref="SalesPointDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="SalesPointDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate(SalesPointDto dto);

        /// <summary>
        /// Проверка значение полей <see cref="SaleDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="SaleDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate(SaleDto dto);
    }
}