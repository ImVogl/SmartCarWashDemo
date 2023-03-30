using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartCarWashDemo
{
    /// <summary>
    /// Общие утилиты.
    /// </summary>
    public class CommonUtils
    {
        /// <summary>
        /// Получение ошибки с кодом 500.
        /// </summary>
        /// <returns><see cref="StatusCodeResult"/> с кодом 500.</returns>
        public static IActionResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}