using CanvasDrawing.DrawingTool.DrawingObject;
using MT.PrismApp.Resources.DrawingTool;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanvasDrawing.DrawingTool
{
    /// <summary>
    /// DrawingCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class DrawingCanvas : UserControl
    {
        StretchingBox _stretchingBox = new StretchingBox();
        DrawingObjectMgr _drawingObjectMgr;
        public void SetDrawingObjectMgr(DrawingObjectMgr drawingObjectMgr)
        {
            _drawingObjectMgr = drawingObjectMgr;
        }
        public DrawingObjectMgr GetDrawingObjectMgr()
        {
            if (_drawingObjectMgr == null)
                _drawingObjectMgr = new DrawingObjectMgr();
            return _drawingObjectMgr;
        }
        public DrawingCanvas()
        {
            InitializeComponent();
            initTransformGroup();
            _stretchingBox.Visibility = Visibility.Hidden;
            _stretchingBox.MyCanvas = CVS;
            _stretchingBox.DrawingCanvas = this;
            CVS.Children.Add(_stretchingBox);
        }
        #region 矩形 集合管理

        public void AddRectangle(Rectangle rectangle)
        {

            if (!CVS.Children.Contains(rectangle))
            {
                CVS.Children.Add(rectangle);
            }
            _drawingObjectMgr.AddRectangle(rectangle);
        }
        public void RemoveRectangle(Rectangle rectangle)
        {

            if (CVS.Children.Contains(rectangle))
            {
                CVS.Children.Remove(rectangle);
            }
            _drawingObjectMgr.RemoveRectangle(rectangle);
        }
        #endregion

        #region 多边形 集合管理
        public void AddPolygon(Polygon polygon)
        {
            if (!CVS.Children.Contains(polygon))
            {
                CVS.Children.Add(polygon);
            }
            _drawingObjectMgr.AddPolygon(polygon);
        }
        public void RemovePolygon(Polygon polygon)
        {
            if (CVS.Children.Contains(polygon))
            {
                CVS.Children.Remove(polygon);
            }
            _drawingObjectMgr.RemovePolygon(polygon);
        }
        #endregion

        #region 缺陷标记 集合管理

        public void AddDefectMarking(DefectMarking defectMarking)
        {
            if (!CVS.Children.Contains(defectMarking))
            {
                CVS.Children.Add(defectMarking);
            }
            _drawingObjectMgr.AddDefectMarking(defectMarking);
        }
        public void RemoveDefectMarking(DefectMarking defectMarking)
        {
            if (CVS.Children.Contains(defectMarking))
            {
                CVS.Children.Remove(defectMarking);
            }
            _drawingObjectMgr.RemoveDefectMarking(defectMarking);
        }


        #endregion


        #region 对外暴露接口

        private string _ImageUri = null;
        public string ImageUri
        {
            get
            {
                return _ImageUri;
            }
            set
            {
                _ImageUri = value;
                if (!string.IsNullOrEmpty(ImageUri))
                {
                    Uri uri = new Uri(ImageUri, UriKind.RelativeOrAbsolute);
                    IMG.Source = new BitmapImage(uri);
                    ImageAdaptive();
                }
                else
                {
                    IMG.Source = null;
                }
            }
        }
        /// <summary>
        /// 当前绘图类型
        /// </summary>
        private DrawType _CurrentDrawType = DrawType.None;

        public DrawType CurrentDrawType
        {
            get { return _CurrentDrawType; }
            set
            {

                _CurrentDrawType = value;
                isDrawing = false;
                rectangle.Height = rectangle.Width = 0;
                polyline.Points.Clear();
            }
        }

        /// <summary>
        /// 当前绘制颜色
        /// </summary>
        public SolidColorBrush CurrentStroke { get; set; } = Brushes.Yellow;
        public SolidColorBrush MouseOverColor { get; set; } = Brushes.SaddleBrown;
        /// <summary>
        /// 当前绘制线宽
        /// </summary>
        public double CurrentStrokeThickness { get; set; } = 10.0;
        #endregion

        #region 画布鼠标键盘事件处理 实现画图和缩放

        /// <summary>
        /// 鼠标移动后的前一个位置
        /// </summary>
        private Point prevMouseXY;
        /// <summary>
        /// 画布鼠标滚轮事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CVS_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var point = e.GetPosition(CCL);
            var delta = e.Delta * 0.0005;
            scaleCanvas(point, delta);
        }

        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        private bool mouseDown;

        /// <summary>
        /// 画布鼠标左键按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CVS_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (GetCtrlStatus())
            {
                //如果ctrl键按下，则捕获鼠标，为拖动做准备
                CVS.CaptureMouse();
                mouseDown = true;
                prevMouseXY = e.GetPosition(CCL);
            }
            else
            {
                //如果ctrl键未按下
                Point currentP = e.GetPosition(CVS);

                //绘制中
                if (isDrawing == true)
                {
                    if (CurrentDrawType == DrawType.Rectangle)
                    {
                        endDrawingRectangle(currentP);
                        //结束绘制
                        isDrawing = false;
                    }
                    else if (CurrentDrawType == DrawType.Polygon)
                    {
                        polyline.Points.RemoveAt(polyline.Points.Count - 1);
                        polyline.Points.Add(currentP);
                        polyline.Points.Add(currentP);
                    }
                }
                else
                {
                    //未绘制
                    //先判定鼠标下面有没有控件，有控件则选中
                    Shape shape = null;
                    foreach (Rectangle rectangle in GetDrawingObjectMgr().Rectangles)
                    {
                        bool xscope = currentP.X > Canvas.GetLeft(rectangle) && currentP.X < Canvas.GetLeft(rectangle) + rectangle.Width;
                        bool yscope = currentP.Y > Canvas.GetTop(rectangle) && currentP.Y < Canvas.GetTop(rectangle) + rectangle.Height;
                        if (xscope && yscope)
                        {
                            shape = rectangle;
                            break;
                        }
                    }
                    foreach (Polygon polygon in GetDrawingObjectMgr().Polygons)
                    {
                        if (RayCastingAlgorithm.IsWithin(currentP, polygon.Points))
                        {
                            RemovePolygon(polygon);
                            shape = polygon;
                            break;
                        }
                    }
                    _stretchingBox.StretchingObject = shape;
                    if (shape == null)
                    {
                        //鼠标下没有控件开始绘制

                        if (CurrentDrawType == DrawType.Rectangle)
                        {
                            startDrawingRectangle(currentP);
                        }
                        else if (CurrentDrawType == DrawType.Polygon)
                        {
                            startDrawingPolygon(currentP);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 画布鼠标左键弹起处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CVS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas == null)
            {
                return;
            }
            canvas.ReleaseMouseCapture();
            mouseDown = false;
        }
        /// <summary>
        /// 画布鼠标移动处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CVS_MouseMove(object sender, MouseEventArgs e)
        {

            //ctrl键按下则拖动画布
            if (GetCtrlStatus())
            {
                if (mouseDown)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        var position = e.GetPosition(CCL);
                        dragCanvas(position);
                    }
                }
            }
            else
            {
                //ctrl键未按下则根据当前鼠标的位置动态画图
                if (isDrawing == true)
                {
                    Point currentP = e.GetPosition(CVS);
                    if (CurrentDrawType == DrawType.Rectangle)
                    {
                        drawingRectangle(currentP);
                    }
                    else if (CurrentDrawType == DrawType.Polygon)
                    {
                        drawingPolygon(currentP);
                    }
                }
            }
        }

        /// <summary>
        /// 画布鼠标右击处理 右击完成画图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CVS_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!GetCtrlStatus())
            {
                if (isDrawing == true)
                {
                    Point currentP = e.GetPosition(CVS);

                    if (CurrentDrawType == DrawType.Rectangle)
                    {
                        endDrawingRectangle(currentP);
                    }
                    else if (CurrentDrawType == DrawType.Polygon)
                    {
                        endDrawingPolygon(currentP);
                    }
                }
                else
                {
                    CurrentDrawType = DrawType.None;
                }
                //结束绘制
                isDrawing = false;
            }
        }
        #endregion

        #region 绘图
        /// <summary>
        /// 是否正在绘图中
        /// </summary>
        bool isDrawing = false;
        /// <summary>
        /// 动态绘图对象-折线
        /// </summary>
        Polyline polyline = new Polyline() { StrokeThickness = 10, Stroke = Brushes.Red, StrokeDashArray = new DoubleCollection() { 2, 3 } };
        /// <summary>
        /// 动态绘图对象-矩形
        /// </summary>
        Rectangle rectangle = new Rectangle() { StrokeThickness = 10, Stroke = Brushes.Red, StrokeDashArray = new DoubleCollection() { 2, 3 } };
        #region 绘制矩形
        /// <summary>
        /// 开始画AI检测区域
        /// </summary>
        /// <param name="currentP"></param>
        void startDrawingRectangle(Point currentP)
        {
            isDrawing = true;
            rectangle.Width = rectangle.Height = 0;
            Canvas.SetLeft(rectangle, currentP.X);
            Canvas.SetTop(rectangle, currentP.Y);
            if (!CVS.Children.Contains(rectangle))
                CVS.Children.Add(rectangle);
        }
        /// <summary>
        /// 正在画AI检测区域矩形
        /// </summary>
        /// <param name="currentP"></param>
        void drawingRectangle(Point currentP)
        {
            if (currentP.X - Canvas.GetLeft(rectangle) > 0)
                rectangle.Width = currentP.X - Canvas.GetLeft(rectangle);
            if (currentP.Y - Canvas.GetTop(rectangle) > 0)
                rectangle.Height = currentP.Y - Canvas.GetTop(rectangle);
        }
        /// <summary>
        /// 结束画AI检测区域
        /// </summary>
        /// <param name="currentP"></param>
        void endDrawingRectangle(Point currentP)
        {
            if (currentP.X - Canvas.GetLeft(rectangle) > 0)
                rectangle.Width = currentP.X - Canvas.GetLeft(rectangle);
            if (currentP.Y - Canvas.GetTop(rectangle) > 0)
                rectangle.Height = currentP.Y - Canvas.GetTop(rectangle);

            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
                double x = Canvas.GetLeft(rectangle);
                double y = Canvas.GetTop(rectangle);
                double w = rectangle.Width;
                double h = rectangle.Height;
                createRectangle(x, y, w, h);
            }
            rectangle.Height = rectangle.Width = 0;
        }



        /// <summary>
        /// 创建矩形
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        void createRectangle(double x, double y, double w, double h)
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = CurrentStroke;
            rect.StrokeThickness = CurrentStrokeThickness;
            rect.Tag = DrawType.Rectangle;
            rect.Width = w;
            rect.Height = h;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            //加入画布
            AddRectangle(rect);
        }

        #endregion

        #region 绘制多边形
        /// <summary>
        /// 开始画多边形
        /// </summary>
        /// <param name="currentP"></param>
        void startDrawingPolygon(Point currentP)
        {
            isDrawing = true;
            polyline.Points.Add(currentP);
            if (polyline.Points.Count == 1)
                polyline.Points.Add(currentP);
            if (!CVS.Children.Contains(polyline))
                CVS.Children.Add(polyline);

        }
        /// <summary>
        /// 正在画多边形
        /// </summary>
        /// <param name="currentP"></param>
        void drawingPolygon(Point currentP)
        {
            polyline.Points[polyline.Points.Count - 1] = currentP;
        }
        /// <summary>
        /// 结束画多边形
        /// </summary>
        /// <param name="currentP"></param>
        void endDrawingPolygon(Point currentP)
        {
            if (polyline.Points.Count > 3)
            {
                Polygon polygon = new Polygon();
                polyline.Points.RemoveAt(polyline.Points.Count - 1);
                createPolygon(polyline.Points.ToArray());
            }
            polyline.Points.Clear();
        }
        /// <summary>
        /// 创建多边形
        /// </summary>
        /// <param name="points"></param>
        void createPolygon(Point[] points)
        {
            if (points != null && points.Length > 3)
            {
                Polygon polygon = new Polygon();
                polygon.Stroke = CurrentStroke;
                polygon.StrokeThickness = CurrentStrokeThickness;
                polygon.Tag = DrawType.Polygon;
                foreach (var point in points)
                    polygon.Points.Add(point);
                //加入画布
                AddPolygon(polygon);
            }
        }
        #endregion

        #endregion

        #region 画布缩放拖动
        /// <summary>
        /// 变换组
        /// </summary>
        TransformGroup _group;
        /// <summary>
        /// 比例变换
        /// </summary>
        ScaleTransform _scaleTransform = new ScaleTransform();
        /// <summary>
        /// 差值变换
        /// </summary>
        TranslateTransform _translateTransform = new TranslateTransform();

        /// <summary>
        /// 初始化变换
        /// </summary>
        void initTransformGroup()
        {
            _group = CVS.FindResource("Imageview") as TransformGroup;
            _group.Children.Add(_scaleTransform);
            _group.Children.Add(_translateTransform);
        }
        /// <summary>
        /// 鼠标滚轮 缩放变化  为了保证缩放后当前鼠标点的位置不变，需要施加一个差值变化。
        /// </summary>
        /// <param name="group"></param>
        /// <param name="point"></param>
        /// <param name="delta"></param>
        private void scaleCanvas(Point point, double delta)
        {
            var pointToContent = _group.Inverse.Transform(point);
            if (_scaleTransform.ScaleX + delta < 0.05) return;
            _scaleTransform.ScaleX += delta;
            _scaleTransform.ScaleY += delta;
            _stretchingBox.ReverseTransform(_scaleTransform.ScaleX);
            _translateTransform.X = -1 * ((pointToContent.X * _scaleTransform.ScaleX) - point.X);
            _translateTransform.Y = -1 * ((pointToContent.Y * _scaleTransform.ScaleY) - point.Y);
        }
        /// <summary>
        /// 鼠标拖动 差值变换
        /// </summary>
        /// <param name="point"></param>
        private void dragCanvas(Point point)
        {
            _translateTransform.X -= prevMouseXY.X - point.X;
            _translateTransform.Y -= prevMouseXY.Y - point.Y;
            prevMouseXY = point;
        }
        /// <summary>
        /// 图像大小自适应
        /// </summary>
        private void ImageAdaptive()
        {
            BitmapSource bmp = (BitmapSource)IMG.Source;
            if (bmp != null)
            {
                var image_w = bmp.PixelWidth;
                var image_h = bmp.PixelHeight;
                var grid_w = gdMain.ActualWidth;
                var grid_h = gdMain.ActualHeight;
                Double scaleX = grid_w / image_w;
                Double scaleY = grid_h / image_h;
                Double scale = scaleX < scaleY ? scaleX : scaleY;
                _scaleTransform.ScaleX = _scaleTransform.ScaleY = 1;
                _translateTransform.X = _translateTransform.Y = 0;
                _scaleTransform.ScaleX = scale;
                _scaleTransform.ScaleY = scale;
            }
        }

        private bool GetCtrlStatus()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}
