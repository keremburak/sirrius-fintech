using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Helper
{
    public static class EnumHelper
    {
        public enum Roles
        {
            SuperAdmin,
            Admin,
            User
        }
        public enum TrasactionalTypes
        {
            D,   //Debit
            C    //Credit
        }

        //public static string GetDescription<T>(this T source)
        //{
        //    FieldInfo fi = source.GetType().GetField(source.ToString());

        //    DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
        //        typeof(DescriptionAttribute), false);

        //    if (attributes != null && attributes.Length > 0) return attributes[0].Description;
        //    else return source.ToString();
        //}


        //EnumHelper.GetEnumDescription(EnumHelper.GetEnumValueFromString<EnumHelper.Roles>("Admin"))
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static T GetEnumValueFromString<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
