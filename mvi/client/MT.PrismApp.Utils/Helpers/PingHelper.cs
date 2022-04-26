/*********************************************************
* 项目名称：Class1.cs
* 开发人员：liyan
* 公    司：聚时科技
* 创建时间：2021/12/23 16:31:09
* 更新时间：2021/12/23 16:31:09
* CLR版本 ：4.0.30319.42000
*
* 描述说明：
* 
* *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MT.PrismApp.Utils.Helpers
{
    public static class PingHelper
    {
        /// <summary>
        /// 获取延迟时间（毫秒）
        /// </summary>
        public static int GetDelayTime(string ipAddr)
        {
            Ping ping = new Ping();
            int timeOut = 130;
            //Ping 选项设置
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            //测试数据
            string data = "";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            PingReply reply = ping.Send(ipAddr, timeOut, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                return reply.Options.Ttl;
            }

            return -1;
        }
    }
}
