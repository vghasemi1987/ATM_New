using System;
using System.Collections.Generic;
using MD.PersianDateTime.Standard;

namespace ApplicationCommon
{
    public static class CustomDateTime
    {
        //public static DateTime ToMiladiDate(this string shamsiDate)
        //{
        //    //return DateTime.Parse(shamsiDate, new CultureInfo("fa-IR"));
        //    return PersianDateTime.Parse(shamsiDate).ToDateTime();
        //}

        public static DateTime ToMiladiDateOffSet(this string shamsiDate)
        {
            var persianDate = PersianDateTime.Parse(shamsiDate);
            return persianDate.ToDateTime();
        }

        public static DateTime ToMiladiDate(this string shamsiDate)
        {
            var persianDate =
                PersianDateTime.Parse(shamsiDate);

            return persianDate.ToDateTime();
        }

        public static string ToPersianDateTime(this DateTimeOffset dt)
        {
            //var d = dt.DateTime;
            //var pc = new PersianCalendar();
            //return $"{pc.GetYear(d)}/{pc.GetMonth(d)}/{pc.GetDayOfMonth(d)} - {pc.GetHour(d)}:{pc.GetSecond(d)}";

            return new PersianDateTime(dt.DateTime).ToString("yyyy/MM/dd - HH:mm");
            //return new PersianDateTime(dt).ToString("dddd d MMMM yyyy hh:mm"); شنبه 5 دی 1394 01:20
        }
        public static string ToPersianDateTime(this DateTime dt, string format)
        {
            return new PersianDateTime(dt).ToString(format);
        }
        public static string ToPersianDateTime(this DateTime? dt, string format)
        {
            return new PersianDateTime(dt).ToString(format);
        }
        //public static string ToPersianDateTime(this DateTime dt, string format)
        //{
        //    return new PersianDateTime(dt).ToString(format);
        //    //return PersianDateTime.Parse(persianDateTime).ToString(format);
        //}
        public static string ToPersianDateTime(this string dt)
        {
            return new PersianDateTime(DateTime.Parse(dt)).ToString("yyyy/MM/dd - HH:mm");
        }

        //public static string ToPersianDate(this DateTimeOffset dt)
        //{
        //    return new PersianDateTime(dt.DateTime).ToString("dddd d MMMM yyyy");
        //}
        //public static string ToPersianDate(this DateTime dt)
        //{
        //    return new PersianDateTime(dt).ToString("dddd d MMMM yyyy");
        //}

        public static string CurrentPersianDate()
        {
            return PersianDateTime.Now.ToString("yyyy/MM/dd");
        }

        public static string RelativeDate(this DateTime theDate)
        {
            var thresholds = new Dictionary<long, string>();
            const int minute = 60;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            thresholds.Add(60, "{0} ثانیه پیش");
            thresholds.Add(minute * 2, "یک دقیقه پیش");
            thresholds.Add(45 * minute, "{0} دقیقه پیش");
            thresholds.Add(120 * minute, "یکساعت پیش");
            thresholds.Add(day, "{0} ساعت پیش");
            thresholds.Add(day * 2, "دیروز");
            thresholds.Add(day * 30, "{0} روز پیش");
            thresholds.Add(day * 365, "{0} ماه پیش");
            thresholds.Add(long.MaxValue, "{0} سال پیش");

            var since = (DateTime.Now.Ticks - theDate.Ticks) / 10000000;
            foreach (var threshold in thresholds.Keys)
            {
                if (since >= threshold) continue;
                var t = new TimeSpan(DateTime.Now.Ticks - theDate.Ticks);
                return string.Format(thresholds[threshold], t.Days > 365 ? t.Days / 365 : (t.Days > 0 ? t.Days : (t.Hours > 0 ? t.Hours : (t.Minutes > 0 ? t.Minutes : (t.Seconds > 0 ? t.Seconds : 0)))));
            }
            return string.Empty;
        }
    }
}