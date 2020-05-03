using System;

namespace QLibrary.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static int Century(this DateTime dt)
        {
            var year = dt.Year;
            return (int)(year / 100) + ((year % 100 == 0) ? 0 : 1);
        }

        public static string ToCCYYMMDD(this DateTime d)
        {
            var CC = d.Century().ToString();
            var YY = d.ToString("YY");
            var MM = d.ToString("MM");
            var DD = d.ToString("DD");
            return CC + YY + MM + DD;
        }
        
    }
}
