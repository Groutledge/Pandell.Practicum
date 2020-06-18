using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pandell.Practicum.App.Extensions
{
    public static class JsonExtensions
    {
        public static JsonObject<string[]> ToJsonObject(this IEnumerable<int> values)
        {
            return JsonConvert.SerializeObject(values, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
        }

        public static IEnumerable<int> FromJsonObject(this JsonObject<string[]> jsonObject)
        {
            return JsonConvert.DeserializeObject<IEnumerable<int>>(jsonObject.Json, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
        }
    }
}