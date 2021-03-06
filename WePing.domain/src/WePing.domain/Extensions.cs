﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using WePing.domain.JoueurDetails.Dto;
using WePing.domain.Licences.Dto;

namespace WePing.domain
{
    public static class Extensions
    {
        public static string GetDefault(this Type type)
        {
            var prop = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(DefaultAttribute))).FirstOrDefault();
            string res = prop?.Name ?? "";
            if (string.IsNullOrEmpty(res) && type.GetProperties().Length == 1)
                return type.GetProperties().First().Name;
            return res;


        }

        public static List<string> GetPropertiesNames(this Type type) => type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();

        public static double GetPoints(this LicenceDto licence, string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0.0;
            var parsed = double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result);
            return parsed ? result : 0;
        }

        public static double GetPoints(this JoueurDetailDto licence,string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0.0;
            var parsed = double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result);
            return parsed ? result : 0;
        }
        public static double GetPoints(this LicenceDto licence, Expression<Func<string>> e)
        {
            var prop = e.GetName();
            var svalue = (string)licence.GetType().GetProperty(prop)?.GetGetMethod()?.Invoke(licence, null);
            return licence.GetPoints(svalue);
        }


        private static string GetName<T>(this Expression<Func<T>> e)
        {
            var member = (MemberExpression)e.Body;
            return member.Member.Name;
        }

        internal static int ToInt(this string value, int defaut = 0)
        {
            if (Int32.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var result))
                return result;
            return defaut;
        }

        internal static float ToFloat(this string value, float defaut = 1.0f)
        {
            if (float.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var result))
                return result;
            return defaut; ;
        }

        internal static double ToDouble(this string value, double defaut = 0.0)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var result))
                return result;
            return defaut; ;
        }

        internal static DateTime ToDate(this string value)
        {
            if (DateTime.TryParse(value, out var result))
                return result;
            return DateTime.Now;
        }
    }
}
