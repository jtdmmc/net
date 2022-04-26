using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.DrawingTool
{
    /// <summary>
    /// 绘制类型
    /// </summary>
    public enum DrawType
    {
        /// <summary>
        /// 非绘制状态
        /// </summary>
        None,
        /// <summary>
        /// 矩形
        /// </summary>
        Rectangle,
        /// <summary>
        /// 多边形
        /// </summary>
        Polygon,
        /// <summary>
        /// 缺陷标记
        /// </summary>
        DefectMarking,

        /// <summary>
        /// 椭圆
        /// </summary>
        /// 
        Ellipse,

        /// <summary>
        /// 折线
        /// </summary>
        Polyline,
    }
}
