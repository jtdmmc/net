using MVI.DotnetSoftwarePlatform.Api.Thrift;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MT.PrismApp.Resource.Converters
{
    /// <summary>
    /// 自定义转换器
    /// </summary>
    public class PowersConverter : IValueConverter
    {
        /// <summary>
        /// 后台转换往前端传值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            List<TPower> roles = value as List<TPower>;
            string str = string.Empty;
            if (roles != null)
            {
                foreach (TPower power in roles)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        str = power.PowerName.ToString();
                    }
                    else
                    {
                        str = str + "," + power.PowerName.ToString();
                    }
                }
            }

            return str;
        }
        /// <summary>
        /// 前端往后端传值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
