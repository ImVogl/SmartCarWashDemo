using JetBrains.Annotations;
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
        bool Validate([NotNull] CustomerDto dto);

        /// <summary>
        /// Проверка значение полей <see cref="ProductDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="ProductDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate([NotNull] ProductDto dto);

        /// <summary>
        /// Проверка значение полей <see cref="SalesPointDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="SalesPointDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate([NotNull] SalesPointDto dto);

        /// <summary>
        /// Проверка значение полей <see cref="ResultSaleDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="ResultSaleDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate([NotNull] ResultSaleDto dto);

        /// <summary>
        /// Проверка значение полей <see cref="SaleBaseDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="SaleBaseDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate([NotNull] SaleDto dto);
    }
}