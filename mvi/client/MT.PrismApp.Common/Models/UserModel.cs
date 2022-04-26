/*********************************************************
* 项目名称：BetaDataModel.cs
* 开发人员：liyan
* 公    司：聚时科技
* 创建时间：2021/12/14 16:07:43
* 更新时间：2021/12/14 16:07:43
* CLR版本 ：4.0.30319.42000
*
* 描述说明：
* 
* *******************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MT.PrismApp.Common.Models
{
    /// <summary>
    /// 用户model
    /// </summary>
    public class UserModel
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool IsEnabled { get; set; }
    }
}
