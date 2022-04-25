using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Common.Helpers
{
    public static class DateTimeExtension
    {
        public static long DateTimeToTick(this DateTime dt)
        {
            return dt.ToUniversalTime().ToFileTimeUtc();
        }

        public static long DateTimeToTick(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.ToUniversalTime().ToFileTimeUtc();
            }
            return 0;
        }

        public static DateTime TickToDateTime(this long ticks)
        {
            return DateTime.FromFileTimeUtc(ticks).ToLocalTime();
        }

        public static DateTime NanosecondToDateTime(this long ns, out long timeSupplementary)
        {
            timeSupplementary = (ns % 10000000) / 10;
            return new DateTime(ns);
        }
    }
}
