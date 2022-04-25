/*********************************************************
* 项目名称：BoxPosition.cs
* 开发人员：liyan
* 公    司：聚时科技
* 创建时间：2021/12/13 14:38:52
* 更新时间：2021/12/13 14:38:52
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
using System.Threading.Tasks;

namespace MT.PrismApp.Common.Enums
{
    /// <summary>
    /// 箱子位置
    /// </summary>
    public enum BoxPosition
    {
        [Description("前小箱")]
        FrontSmallBox = 0,
        [Description("后小箱")]
        BackSmallBox = 1,
        [Description("大箱")]
        LargeBox = 2,
    }
}
