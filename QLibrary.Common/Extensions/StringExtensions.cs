using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using QLibrary.Common.Enums;

namespace QLibrary.Common.Extensions
{

    
    public static class StringExtensions
    {
        public static string Repeat(this char chatToRepeat, int repeat)
        {

            return new string(chatToRepeat, repeat);
        }
        public static string Repeat(this string stringToRepeat, int repeat)
        {
            var builder = new StringBuilder(repeat * stringToRepeat.Length);
            for (int i = 0; i < repeat; i++)
            {
                builder.Append(stringToRepeat);
            }
            return builder.ToString();
        }
        public static string ToFixedLengthString(this string s,int len)
        {
            if (s == null)
            {
                return new String(' ',len);
            }

            if(s==string.Empty)
            {
                return new String(' ',len);
            }

            if (s.Length < len)
            {
                var temp = new String(' ', len - s.Length);
                return s + temp;
            }
            return s;
            
        }

    }
}
