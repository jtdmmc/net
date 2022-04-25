/*********************************************************
* 项目名称：FileStreamHelper.cs
* 开发人员：liyan
* 公    司：聚时科技
* 创建时间：2021/12/1 14:00:45
* 更新时间：2021/12/1 14:00:45
* CLR版本 ：4.0.30319.42000
*
* 描述说明：
* 
* *******************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MT.PrismApp.Utils.Helpers
{
    public static class FileStreamHelper
    {

        public static string GetFileStringByPath(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader stream = new StreamReader(filePath))
                    {
                        string res = stream.ReadToEnd();
                        return res;
                    }
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool SaveFileContentByPath(string filePath,string content)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamWriter stream = new StreamWriter(filePath))
                    {
                        stream.WriteLine(content);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
