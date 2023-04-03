using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SmartCarWashDemo.Model.Dto
{
    /// <summary>
    /// DTO со сведениями о покупателе.
    /// </summary>
    public class CustomerDto : NamedBaseDto
    {
        /// <summary>
        /// Получает или задает коллекцию идентификаторов актов продаж.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sale_ids")]
        public ICollection<long> SaleIds { get; set; }

        /// <summary>
        /// Получает или задает день и время, когда пользователь был зарегистрирован.
        /// </summary>
        [JsonProperty("registration")]
        public DateTime? RegistrationDateTime { get; set; }
    }
}