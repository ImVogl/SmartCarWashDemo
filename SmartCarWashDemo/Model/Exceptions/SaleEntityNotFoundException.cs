using System;

namespace SmartCarWashDemo.Model.Exceptions
{
    /// <summary>
    /// Исключение, генерируемое в случае, когда не удалось обнаружить сущность "Акт продажи" в базе данных.
    /// </summary>
    public class SaleEntityNotFoundException : Exception
    {
    }
}