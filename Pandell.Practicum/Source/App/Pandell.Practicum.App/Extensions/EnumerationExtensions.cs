using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Pandell.Practicum.App.Extensions
{
    public static class EnumerationExtensions
    {
        public static string ToDescription<TEnum>(this TEnum value)
        {
            var enumFields = value.GetType().GetField(value.ToString()!);
            if (enumFields == null) return string.Empty;
            
            var attributes = (DescriptionAttribute[]) enumFields.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
        
        public static List<TEnum> ToEnumList<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
        }
    }
}