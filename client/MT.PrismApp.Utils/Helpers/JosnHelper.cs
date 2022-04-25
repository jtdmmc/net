/*********************************************************
* 项目名称：JosnHelper.cs
* 开发人员：liyan
* 公    司：聚时科技
* 创建时间：2021/12/1 10:52:00
* 更新时间：2021/12/1 10:52:00
* CLR版本 ：4.0.30319.42000
*
* 描述说明：
* 
* *******************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MT.PrismApp.Utils.Helpers
{
    public static class JosnHelper
    {
        public static T GetEntityByJosnStr<T>(string josnStr) where T : class
        {
            try
            {
                T t = JsonConvert.DeserializeObject<T>(josnStr);
                return t;
            }
            catch
            {
                return default(T);
            }
        }

        public static T GetNewEntityByOldEntity<T>(T t) where T : class
        {
            try
            {
                string josnStr = GetJosnStrByEntity<T>(t);
                T res = GetEntityByJosnStr<T>(josnStr);
                return res;
            }
            catch
            {
                return default(T);
            }
        }

        public static string GetJosnStrByEntity<T>(T t) where T : class
        {
            try
            {
                string josnStr = JsonConvert.SerializeObject(t);
                return josnStr;
            }
            catch
            {
                return null;
            }
        }
    }
}
