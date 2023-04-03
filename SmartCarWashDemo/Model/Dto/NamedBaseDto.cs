using Newtonsoft.Json;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// Базовое DTO с именем сущности.
    /// </summary>
    public abstract class NamedBaseDto : BaseDto
    {
        /// <summary>
        /// Получает или задает имя сущности.
        /// </summary>
        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}