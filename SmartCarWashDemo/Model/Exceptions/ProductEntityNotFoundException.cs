using System;

namespace SmartCarWashDemo.Model.Exceptions
{
    /// <summary>
    /// Исключение, генерируемое в случае, когда не удалось обнаружить сущность "Продукт" в базе данных.
    /// </summary>
    public class ProductEntityNotFoundException : Exception
    {
    }
}