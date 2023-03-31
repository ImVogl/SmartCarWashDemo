using Newtonsoft.Json;
using System;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// DTO со сведениями о покупателе.
    /// </summary>
    public class CustomerDto : BaseDto
    {
        /// <summary>
        /// Получает или задает день и время, когда пользователь был зарегистрирован.
        /// </summary>
        [JsonRequired]
        [JsonProperty("registration")]
        public DateTime RegistrationDateTime { get; set; }
    }
}