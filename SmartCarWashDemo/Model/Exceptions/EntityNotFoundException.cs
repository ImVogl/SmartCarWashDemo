using System;

namespace SmartCarWashDemo.Model.Exceptions
{
    /// <summary>
    /// Исключение, генерируемое в случае, когда не удалось обнаружить сущность в базе данных.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
    }
}