using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// DTO со сведениями о покупателе.
    /// </summary>
    public class CustomerDto : BaseDto
    {
        /// <summary>
        /// Получает или задает коллекцию всех идентификаторов сделанных пользователем покупок.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sale_ids")]
        public ICollection<long> SalesIds { get; set; }

        /// <summary>
        /// Получает или задает день и время, когда пользователь был зарегистрирован.
        /// </summary>
        [JsonRequired]
        [JsonProperty("registration")]
        public DateTime RegistrationDateTime { get; set; }
    }
}