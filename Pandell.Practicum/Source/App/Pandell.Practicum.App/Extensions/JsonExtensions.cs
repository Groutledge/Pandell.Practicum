using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace Pandell.Practicum.App.Extensions
{
    public static class JsonExtensions
    {
        public static JsonObject<string[]> ToJsonObject(this IEnumerable<int> values)
        {
            return JsonConvert.SerializeObject(values, GenerateJsonSerializerSettings());
        }

        public static List<int> FromJsonObject(this JsonObject<string[]> jsonObject)
        {
            return FromJsonObject(jsonObject.Json);
        }

        public static List<int> FromJsonObject(this string json)
        {
            return JsonConvert.DeserializeObject<List<int>>(json, GenerateJsonSerializerSettings());
        }
        
        private static JsonSerializerSettings GenerateJsonSerializerSettings()
        {
            return new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
        }
        
        public static T DeserializeForHidden<T>(this string value, T _)
        {
            return string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(HttpUtility.HtmlDecode(value));
        }
        
        public static string SerializeForHidden(this object value)
        {
            if (value == null) return null;

            return HttpUtility.HtmlEncode(JsonConvert.SerializeObject(value,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
        }
    }
}