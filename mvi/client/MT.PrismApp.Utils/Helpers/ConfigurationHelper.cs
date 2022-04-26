/*********************************************************
* 项目名称：ConfigurationHelper.cs
* 开发人员：liyan
* 公    司：聚时科技
* 创建时间：2022/2/10 11:29:41
* 更新时间：2022/2/10 11:29:41
* CLR版本 ：4.0.30319.42000
*
* 描述说明：
* 
* *******************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.PrismApp.Utils.Helpers
{
    public static class ConfigurationHelper
    {
        public static bool isTestEnironment;
        public static string GetAppValue(string key)
        {
            try
            {
                Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string value = config.AppSettings.Settings[key].Value;
                return value;
            }
            catch
            {
                return null;
            }
        }

        public static void InitEnironment(string key = "IsTestEnironment")
        {
            string value = GetAppValue(key);
            if (string.IsNullOrEmpty(value))
            {
                isTestEnironment = true;
            }
            else
            {
                if (bool.TryParse(value, out bool res))
                {
                    isTestEnironment = res;
                }
                else
                {
                    isTestEnironment = true;
                }
            }
        }
    }
}
