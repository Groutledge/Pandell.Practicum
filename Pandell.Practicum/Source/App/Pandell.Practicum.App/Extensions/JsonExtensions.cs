using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pandell.Practicum.App.Extensions
{
    public static class JsonExtensions
    {
        public static JsonObject<string[]> ToJsonObject(this List<int> values)
        {
            return JsonConvert.SerializeObject(values, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
        }
    }
}