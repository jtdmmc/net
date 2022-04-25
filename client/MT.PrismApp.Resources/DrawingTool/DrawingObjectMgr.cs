using CanvasDrawing.DrawingTool.DrawingObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace CanvasDrawing.DrawingTool
{
    /// <summary>
    /// 绘图对象管理类，对画布中绘制的对象进行统一管理
    /// </summary>
    public class DrawingObjectMgr
    {
        public DrawingObjectMgr()
        {
        }
        #region 对外暴露接口

        #region 矩形 集合管理
        public List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();
        public void RemoveAllRectangles()
        {
            Rectangles.Clear();
        }
        public void RemoveRectangle(Rectangle rectangle)
        {
            if (Rectangles.Contains(rectangle))
            {
                Rectangles.Remove(rectangle);
            }
        }

        public void AddRectangle(Rectangle rectangle)
        {
            if (!Rectangles.Contains(rectangle))
            {
                Rectangles.Add(rectangle);
            }
        }
        #endregion
        #region 多边形 集合管理
        public List<Polygon> Polygons { get; set; } = new List<Polygon>();
        public void RemovePolygons(List<Polygon> polygons)
        {
            foreach (Polygon polygon in polygons)
            {
                RemovePolygon(polygon);
            }
        }
        public void RemovePolygon(Polygon polygon)
        {
            if (Polygons.Contains(polygon))
            {
                Polygons.Remove(polygon);
            }
        }
        public void AddPolygon(Polygon Polygon)
        {
            if (!Polygons.Contains(Polygon))
            {
                Polygons.Add(Polygon);
            }
        }
        #endregion
        #region 缺陷标记 集合管理
        public List<DefectMarking> DefectMarkings { get; set; }
        public void RemoveAllDefectMarkings()
        {
            DefectMarkings.Clear();
        }
        public void RemoveDefectMarking(DefectMarking defectMarking)
        {
            if (DefectMarkings.Contains(defectMarking))
            {
                DefectMarkings.Remove(defectMarking);
            }
        }
        public void AddDefectMarkings(List<DefectMarking> defectMarkings)
        {
            foreach (DefectMarking defectMarking in defectMarkings)
            {
                AddDefectMarking(defectMarking);
            }
        }

        public void AddDefectMarking(DefectMarking defectMarking)
        {
            if (!DefectMarkings.Contains(defectMarking))
            {
                DefectMarkings.Add(defectMarking);
            }
        }

        #endregion
        #endregion
    }
}
