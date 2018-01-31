using System;

namespace BYBY.Infrastructure.Helpers
{

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static string GetDayOfWeekText(this DayOfWeek dayofweek)
        {
            switch (dayofweek)
            {
                case DayOfWeek.Sunday:
                    return "周日";
                case DayOfWeek.Monday:
                    return "周一";
                case DayOfWeek.Tuesday:
                    return "周二";
                case DayOfWeek.Wednesday:
                    return "周三";
                case DayOfWeek.Thursday:
                    return "周四";
                case DayOfWeek.Friday:
                    return "周五";
                case DayOfWeek.Saturday:
                    return "周六";
                default:
                    break;
            }
            return "日期错误";
        }

        public static string ToDateTimeString(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToTimeString(this DateTime val)
        {
            return val.ToString("HH:mm");
        }


        public static string ToDateTimeString(this DateTime? val)
        {
            return val.HasValue ? val.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        }

        public static string ToDateString(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd");
        }

        public static string ToDateString(this DateTime? val)
        {
            return val.HasValue ? val.Value.ToString("yyyy-MM-dd") : "";
        }

      

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
