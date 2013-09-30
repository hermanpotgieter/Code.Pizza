using System;
using System.ComponentModel;

namespace Code.Pizza.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum enumeration)
        {
            string value = enumeration.ToString();

            DescriptionAttribute[] attribute = (DescriptionAttribute[])enumeration.GetType()
                .GetField(value).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attribute.Length > 0 ? attribute[0].Description : value;
        }
    }
}