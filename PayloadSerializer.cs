using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader
{
    public static class PayloadSerializer
    {
        private static readonly JsonSerializerSettings _serializerSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None,
            DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        };

        public static string SerializeToJson(SecurityCheckStatusCreatePayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload), "Объект payload не может быть null.");
            }

            try
            {
                return JsonConvert.SerializeObject(payload, _serializerSettings);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Не удалось сериализовать объект типа {payload.GetType().Name} в JSON.", ex);
            }
        }
    }
}
