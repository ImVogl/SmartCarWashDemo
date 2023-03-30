using SmartCarWashDemo.Model.Dto;

namespace SmartCarWashDemo.Services.Validators
{
    /// <summary>
    /// Интерфейс валидатора DTO.
    /// </summary>
    public interface IDtoValidator
    {
        /// <summary>
        /// Проверка имени покупателя.
        /// </summary>
        /// <param name="name">Имя покупателя.</param>
        /// <returns>Значение, показывающее, что имя покупателя корректно.</returns>
        bool ValidateName(string name);

        /// <summary>
        /// Проверка <see cref="UpdateCustomerDto"/>.
        /// </summary>
        /// <param name="dto"><see cref="UpdateCustomerDto"/>.</param>
        /// <returns>Значение, показывающее, что сведения корректны.</returns>
        bool Validate(UpdateCustomerDto dto);
    }
}