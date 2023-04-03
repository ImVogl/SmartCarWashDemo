using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// Базовый класс DTO.
    /// </summary>
    public abstract class BaseDto
    {
        /// <summary>
        /// Получает или задает идентификатор сущности.
        /// </summary>
        [JsonProperty("id")]
        public long? Id { get; set; }
    }
}