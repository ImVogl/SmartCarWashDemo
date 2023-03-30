using System;

namespace SmartCarWashDemo.Model.Exceptions.Customer
{
    /// <summary>
    /// Исключение, генерируемое в случае, когда не удалось обнаружить пользователя в базе данных.
    /// </summary>
    public class CustomerNotFoundException : Exception
    {
    }
}