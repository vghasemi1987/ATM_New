using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ApplicationCommon
{
    public static class Tools
    {
        public static string NewFileName(string file)
        {
            return Guid.NewGuid() + Path.GetExtension(file);
        }

        private const int MaxLengthSlug = 45;
        public static string EncodeUrl(this string title)
        {
            var slug = RemoveAccent(title).ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
            slug = Regex.Replace(slug, @"\s+", "-").Trim();
            slug = Regex.Replace(slug, @"-+", "-");
            return slug.Substring(0, slug.Length <= MaxLengthSlug ? slug.Length : MaxLengthSlug).Trim();
        }

        private static string RemoveAccent(string text)
        {
            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string DecodeUrl(this string value)
        {
            value = value.Replace("-", " ");
            return value;
        }

        public static string ToLatinNum(this string value)
        {
            return Regex.Replace(
                value,
                @"\d+",
                m => string.Join("", m.Groups[0].Value.Select(x => Convert.ToChar(x - 1728)))
                );
        }
        public static string RemoveAllHtmlTags(this string text, int maxChar)
        {
            var withoutHtml = string.IsNullOrEmpty(text) ?
                          string.Empty :
                          Regex.Replace(text, "<(.|\\n)*?>", string.Empty);

            withoutHtml = withoutHtml.Replace("&nbsp;", " ").Replace("&zwnj;", " ").Replace("&quot;", " ").Replace("amp;", "");
            withoutHtml = withoutHtml.Replace("&laquo;", "«");
            withoutHtml = withoutHtml.Replace("&raquo;", "»");
            return withoutHtml.Length < maxChar ? withoutHtml : withoutHtml.Substring(0, maxChar);
            //return withoutHtml;
        }

        public static string ConvertToMinSec(this int second)
        {
            var min = second / 60;
            var sec = second - min * 60;
            return $"{min}:{sec}";
        }

        public static T GetObjectValue<T>(this object item, string propName)
        {
            var pi = item.GetType().GetProperty(propName);
            return (T)pi.GetValue(item, null);
        }

        public static string DescriptionAttr<T>(this T source)
        {
            if (source==null)
                return "---";

            var fi = source.GetType().GetField(source.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }
        public static decimal ToDecimal(this object source)
        {
            try
            {
                double.TryParse(source.ToString().Trim(), out double doubleNumber);
                decimal.TryParse(Math.Round(doubleNumber).ToString(), out decimal decimalNumber);
                return decimalNumber;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static string ToCardType(this string cardNumber)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();

            myDic.Add("603799", "بانک ملی");
            myDic.Add("589210", "بانک سپه");
            myDic.Add("604932", "بانک سپه");
            myDic.Add("627648", "بانک توسعه صادرات");
            myDic.Add("627961", "بانک صنعت و معدن");
            myDic.Add("603770", "بانک کشاورزی");
            myDic.Add("628023", "بانک مسکن");
            myDic.Add("627760", "پست بانک");
            myDic.Add("502908", "بانک توسعه تعاون");
            myDic.Add("627412", "بانک اقتصاد نوین");
            myDic.Add("622106", "بانک پارسیان");
            myDic.Add("502229", "بانک پاسارگاد");
            myDic.Add("639599", "بانک قوامین");
            myDic.Add("627488", "بانک کارآفرین");
            myDic.Add("639346", "بانک سینا");
            myDic.Add("639607", "بانک سرمایه");
            myDic.Add("504706", "بانک شهر");
            myDic.Add("502938", "بانک دی");
            myDic.Add("603769", "بانک صادرات");
            myDic.Add("610433", "بانک ملت");
            myDic.Add("627353", "بانک تجارت");
            myDic.Add("589463", "بانک رفاه");
            myDic.Add("507677", "موسسه نور");
            myDic.Add("606373", "بانک قرض الحسنه مهر ایرانیان");
            myDic.Add("505416", "بانک گردشگری");
            myDic.Add("627381", "انصار");
            myDic.Add("636214", "آینده");
            myDic.Add("621986", "سامان");
            myDic.Add("606256", "عسگریه");
            myDic.Add("505801", "کوثر");
            myDic.Add("628157", "موسسه اعتباری توسعه");
            myDic.Add("936450", "سروش");
            myDic.Add("636949", "حکمت ایرانیان");
            myDic.Add("505785", "ایران زمین");
            myDic.Add("636795", "بانک مرکزی");
            myDic.Add("581672000", "شاپرک");
            myDic.Add("504172", "رسالت");

            myDic.Add("505809", "خاورمیانه");
            myDic.Add("585947", "خاورمیانه");

            myDic.Add("581874", "ایران و ونزولا");
            myDic.Add("585983", "بانک تجارت");
            myDic.Add("639370", "موسسه مالی و اعتباری مهر");
         




            foreach (var item in myDic)
			{
                if (cardNumber.StartsWith(item.Key))
                    return item.Value;
			}
            return "";
        }
    }
}