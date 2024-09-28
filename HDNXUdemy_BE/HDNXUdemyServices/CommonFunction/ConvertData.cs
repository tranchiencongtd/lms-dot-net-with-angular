using HDNXUdemyModel.Base;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace HDNXUdemyServices.CommonFunction
{
    public static class ConvertData
    {
        private static readonly string NumberGroup = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator; // VN "."
        private static readonly string NumberDecimal = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator; // VN ","

        public static decimal ToDecimal(string value)
        {
            // Kiểu My
            if (NumberGroup == "," && NumberDecimal == ".")
            {
                return decimal.Parse(value);
            }

            // Kiểu Việt Nam
            else
            {
                const string temp = "@";
                value = value.Replace(".", temp);
                value = value.Replace(",", ".");
                value = value.Replace(temp, ",");
                return decimal.Parse(value);
            }
        }

        public static string ToStr(decimal pValue)
        {
            string value;

            // kieu My
            if (NumberGroup == "," && NumberDecimal == ".")
            {
                const string temp = "@";

                value = pValue.ToString(ProjectConfig.NumberFormat);
                value = value.Replace(".", temp);
                value = value.Replace(",", ".");
                value = value.Replace(temp, ",");
                return value;
            }

            // Kiểu Việt Nam
            else
            {
                value = pValue.ToString(ProjectConfig.NumberFormat);
                return value;
            }
        }

        public static DateTime? ToDateTime(this decimal pNumber)
        {
            if (pNumber > 0)
            {
                return DateTime.ParseExact(pNumber.ToString(CultureInfo.CurrentCulture), ProjectConfig.DateTimeSqlFormat ?? String.Empty, CultureInfo.CurrentCulture);
            }

            return null;
        }

        public static decimal ToBigInt(this DateTime pDateTime)
        {
            return Convert.ToDecimal(pDateTime.ToString(ProjectConfig.DateTimeSqlFormat));
        }

        public static decimal ToBigInt(this DateTime? pDateTime)
        {
            return Convert.ToDecimal(pDateTime?.ToString(ProjectConfig.DateTimeSqlFormat));
        }

        public static decimal ToBigInt(this string pDateDdMmYyyy, int pAddDays = 0)
        {
            return DateTime.TryParseExact(pDateDdMmYyyy, ProjectConfig.DateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out var oDate) ? oDate.AddDays(pAddDays).ToBigInt() : 0;
        }

        public static DateTime ToDateExp(this long pNumber)
        {
            return pNumber > 0 ? DateTime.ParseExact(pNumber.ToString(), ProjectConfig.DateExpSqlFormat ?? String.Empty, CultureInfo.CurrentCulture) : ProjectConfig.MaxDateExp;
        }

        public static long ToBigIntDateExp(this DateTime pDateTime)
        {
            return Convert.ToInt64(pDateTime.ToString(ProjectConfig.DateExpSqlFormat));
        }

        public static DateTime AddNow(this DateTime pDateTime)
        {
            DateTime now = DateTime.UtcNow;
            return pDateTime.Date.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddTicks(now.Ticks);
        }

        public static bool IsBetween(this DateTime pDateTimeBetween, DateTime pDateTimeBegin, DateTime pDateTimeEnd)
        {
            return pDateTimeBegin >= pDateTimeBetween && pDateTimeBetween >= pDateTimeEnd;
        }

        public static string GetEnumDescription<TEnum>(this TEnum? pItem)
        {
            if (pItem == null)
            {
                return string.Empty;
            }
            return pItem?.GetType()?
                .GetField(pItem?.ToString() ?? string.Empty)?
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>().FirstOrDefault()?.Description ?? string.Empty;
        }

        public static TEnum GetEnumFromDescription<TEnum>(string pDescription)
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
            {
                throw new ArgumentException();
            }

            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((DescriptionAttribute)a.Att)
                                                                                          .Description == pDescription);
            return field == null ? default : (TEnum)field.Field.GetRawConstantValue();
        }

        public static string ConvertUnicodeToEngLish(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = value.Normalize(NormalizationForm.FormD);
                string valueId = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                return valueId.ToLower().Replace(" ", "-");
            }
            else
            {
                return string.Empty;
            }
        }

        public static DistributedCacheEntryOptions OptionCache()
        {
            DistributedCacheEntryOptions optionSaveCache = new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(ProjectConfig.SetSlidingExpiration),
                AbsoluteExpiration = DateTime.Now.AddMinutes(ProjectConfig.SetAbsoluteExpiration)
            };

            return optionSaveCache;
        }
    }
}