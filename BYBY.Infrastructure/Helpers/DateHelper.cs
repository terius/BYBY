using System;

namespace BYBY.Infrastructure.Helpers
{
    public static class DateHelper
    {

        public static DateTime GetBirthdayFromIDCard(this string idcard)
        {
            DateTime birthday = DateTime.Parse("1900-01-01");
            if (string.IsNullOrEmpty(idcard) || (idcard.Length != 18 && idcard.Length != 15))
            {
                throw new FormatException("身份证号码错误");
            }

            string str = idcard.Substring(6, 4) + "-" + idcard.Substring(10, 2) + "-" + idcard.Substring(12, 2);
            if (DateTime.TryParse(str, out birthday))
            {
                return birthday;
            }

            return DateTime.Parse("1900-01-01");
        }

        public static DateTime ToDate(this string dateStr)
        {
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(dateStr, out dt))
            {
                return dt;
            }
            else
            {
                throw new FormatException("日期字符串错误");
            }

        }

        public static int GetAge(this DateTime dt)
        {
            return DateTime.Now.Year - dt.Year;
        }
        public static string GetAge(this DateTime? dt)
        {
            return dt == null ? "" : (DateTime.Now.Year - dt.Value.Year).ToString();
        }
    }
}
