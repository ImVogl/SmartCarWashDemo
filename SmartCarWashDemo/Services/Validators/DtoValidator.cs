using SmartCarWashDemo.Model.Dto;
using SmartCarWashDemo.Model.Dto.Sales;

namespace SmartCarWashDemo.Services.Validators
{
    /// <summary>
    /// Валидатор DTO.
    /// </summary>
    public class DtoValidator : IDtoValidator
    {
        /// <inheritdoc />
        public bool Validate(CustomerDto dto)
        {
            if (dto == null)
                return false;

            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        /// <inheritdoc />
        public bool Validate(ProductDto dto)
        {
            if (dto == null)
                return false;

            if (string.IsNullOrWhiteSpace(dto.Name))
                return false;

            return dto.Price >= 0f;
        }

        /// <inheritdoc />
        public bool Validate(SalesPointDto dto)
        {
            if (dto == null)
                return false;

            if (string.IsNullOrWhiteSpace(dto.Name))
                return false;
            
            return dto.ProvidedProducts != null;
        }

        /// <inheritdoc />
        public bool Validate(SaleDto dto)
        {
            if (dto == null)
                return false;

            if (dto.SalesData == null)
                return false;

            return dto.SalesPointId > 0;
        }
    }
}