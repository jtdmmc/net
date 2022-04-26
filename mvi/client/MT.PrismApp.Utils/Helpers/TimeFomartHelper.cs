/*********************************************************
* 项目名称：TimeFomartHelper.cs
* 开发人员：liyan
* 公    司：聚时科技
* 创建时间：2022/1/10 16:02:24
* 更新时间：2022/1/10 16:02:24
* CLR版本 ：4.0.30319.42000
*
* 描述说明：
* 
* *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.PrismApp.Utils.Helpers
{
    public static class TimeFomartHelper
    {
        private const string _timeFormat = "yyyy/MM/dd HH:mm:ss";

        public static string GetTimeNowStr()
        {
            return GetTimeStr(DateTime.Now);
        }
        public static string GetTimeStr(DateTime dateTime, string timeFormat = null)
        {
            if (string.IsNullOrEmpty(timeFormat))
            {
                return dateTime.ToString(_timeFormat);
            }
            else
            {
                return dateTime.ToString(timeFormat);
            }
        }

        public static string GetTimeStrNoFormat(DateTime dateTime)
        {
            string timeFormat = "yyyyMMddHHmmss";
            return dateTime.ToString(timeFormat);
        }

        public static DateTime? GetDateTime(string timeStr)
        {
            if (DateTime.TryParse(timeStr, out DateTime dateTime))
            {
                return dateTime;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过时间戳获取当前时间
        /// </summary>
        /// <param name="timeStampStr"></param>
        /// <param name="timeZone"></param>
        /// <param name="timeFormat"></param>
        /// <returns></returns>
        public static DateTime? GetDateTimeByTimeStamp(string timeStampStr, DateTime? timeZone = null, TimeFormat timeFormat = TimeFormat.Second)
        {
            DateTime? dateTime;
            if (timeZone == null)
            {
                dateTime = new DateTime(1970, 1, 1, 8, 0, 0);
            }
            else
            {
                dateTime = timeZone.Value;
            }

            try
            {
                switch (timeFormat)
                {
                    case TimeFormat.Year:
                        dateTime = dateTime.Value.AddYears(int.Parse(timeStampStr));
                        break;
                    case TimeFormat.Month:
                        dateTime = dateTime.Value.AddMonths(int.Parse(timeStampStr));
                        break;
                    case TimeFormat.Day:
                        dateTime = dateTime.Value.AddDays(double.Parse(timeStampStr));
                        break;
                    case TimeFormat.Hour:
                        dateTime = dateTime.Value.AddHours(double.Parse(timeStampStr));
                        break;
                    case TimeFormat.Minute:
                        dateTime = dateTime.Value.AddMinutes(double.Parse(timeStampStr));
                        break;
                    case TimeFormat.Second:
                        dateTime = dateTime.Value.AddSeconds(double.Parse(timeStampStr));
                        break;
                    case TimeFormat.millisecond:
                        dateTime = dateTime.Value.AddMilliseconds(double.Parse(timeStampStr));
                        break;
                }
                return dateTime;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 通过时间戳获取当前时间
        /// </summary>
        /// <param name="timeStampStr"></param>
        /// <param name="timeZone"></param>
        /// <param name="timeFormat"></param>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime? timeNow = null, TimeFormat timeFormat = TimeFormat.Second)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan timeSpan;
            if (timeNow.HasValue)
            {
                timeSpan = timeNow.Value - dateTime;
            }
            else
            {
                timeSpan = DateTime.Now - dateTime;
            }

            double res = 0;
            try
            {
                switch (timeFormat)
                {
                    case TimeFormat.Day:
                        res = timeSpan.TotalDays;
                        break;
                    case TimeFormat.Hour:
                        res = timeSpan.TotalHours;
                        break;
                    case TimeFormat.Minute:
                        res = timeSpan.TotalMinutes;
                        break;
                    case TimeFormat.Second:
                        res = timeSpan.TotalSeconds;
                        break;
                    case TimeFormat.millisecond:
                        res = timeSpan.TotalMilliseconds;
                        break;
                }
                return Math.Round(res).ToString();
            }
            catch
            {
                return null;
            }
        }

        public static string TimeStrConvertTimeStamp(string timeFormatStr)
        {
            DateTime? dateTime = GetDateTime(timeFormatStr);
            return GetTimeStamp(dateTime.Value);
        }

        public static string TimeStampConvertTimeFormatStr(string timeStampStr)
        {
            DateTime? dateTime = GetDateTimeByTimeStamp(timeStampStr);
            return GetTimeStr(dateTime.Value);
        }

        /// <summary>
        /// 获取最近一周的开始时间
        /// </summary>
        /// <returns></returns>
        public static string GetLastWeekStartTimeStr()
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            return GetTimeStamp(dateTime);
        }

        /// <summary>
        /// 获取最近一天的开始时间
        /// </summary>
        /// <returns></returns>
        public static string GetLastDayStartTimeStr()
        {
            DateTime dateTime = DateTime.Now.AddDays(-1);
            return GetTimeStamp(dateTime);
        }

        public enum TimeFormat
        {
            Year = 0,
            Month = 1,
            Day = 2,
            Hour = 3,
            Minute = 4,
            Second = 5,
            millisecond = 6
        }
    }
}
