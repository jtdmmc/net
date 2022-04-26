using System;
using System.Collections.Generic;
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
    /// DrawingObjectBox.xaml 的交互逻辑
    /// </summary>
    public partial class StretchingBox : UserControl
    {
        PointCollection initPoints;
        double initW;
        double initH;
        /// <summary>
        /// 当前绘制颜色
        /// </summary>
        public SolidColorBrush CurrentStroke { get; set; } = Brushes.Yellow;
        /// <summary>
        /// 当前绘制线宽
        /// </summary>
        public double CurrentStrokeThickness { get; set; } = 10.0;
        Polygon _Polygon = new Polygon();
        public Canvas MyCanvas { get; set; }
        public DrawingCanvas DrawingCanvas { get; set; }
        public StretchingBox()
        {
            InitializeComponent();
            _Polygon.Stroke = CurrentStroke;
            _Polygon.StrokeThickness = CurrentStrokeThickness;
            _Polygon.Visibility = Visibility.Hidden;
            mainGrid.Children.Add(_Polygon);

        }

        private Shape _StretchingObject;

        public Shape StretchingObject
        {
            get { return _StretchingObject; }
            set
            {
              
                if (value == null)
                {
                    if (initPoints != null)
                    {
                        Polygon polygon = new Polygon();
                        polygon.Stroke = CurrentStroke;
                        polygon.StrokeThickness = CurrentStrokeThickness;
                        foreach (var point in _Polygon.Points)
                        {
                            polygon.Points.Add(new Point(point.X + shapeP.X, point.Y + shapeP.Y));
                        }
                        DrawingCanvas.AddPolygon(polygon);
                        initPoints = null;
                    }
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (_StretchingObject != value)
                    {
                        _StretchingObject = value;
                        this.Visibility = Visibility.Visible;
                        if (initPoints != null)
                        {
                            Polygon polygon = new Polygon();
                            polygon.Stroke = CurrentStroke;
                            polygon.StrokeThickness = CurrentStrokeThickness;
                            foreach (var point in _Polygon.Points)
                            {
                                polygon.Points.Add(new Point(point.X + shapeP.X, point.Y + shapeP.Y));
                            }
                            DrawingCanvas.AddPolygon(polygon);
                            initPoints = null;
                        }
            
                        if (_StretchingObject is Rectangle)
                        {
                            _Polygon.Visibility = Visibility.Hidden;
                            this.Width = _StretchingObject.Width;
                            this.Height = _StretchingObject.Height;
                            Canvas.SetLeft(this, Canvas.GetLeft(_StretchingObject));
                            Canvas.SetTop(this, Canvas.GetTop(_StretchingObject));
                        }
                        else if (_StretchingObject is Polygon)
                        {
                            _Polygon.Visibility = Visibility.Visible;
                            Polygon polygon = _StretchingObject as Polygon;
                            double minx = polygon.Points.Min(_ => _.X);
                            double miny = polygon.Points.Min(_ => _.Y);
                            double maxx = polygon.Points.Max(_ => _.X);
                            double maxy = polygon.Points.Max(_ => _.Y);


                            initW = this.Width = maxx - minx;
                            initH = this.Height = maxy - miny;
                            Canvas.SetLeft(this, minx);
                            Canvas.SetTop(this, miny);
                            _Polygon.Points.Clear();
                            foreach (var point in polygon.Points)
                            {
                                _Polygon.Points.Add(new Point(point.X - minx, point.Y - miny));
                            }
                            initPoints = _Polygon.Points;
                        }
                        shapeP.X = Canvas.GetLeft(this);
                        shapeP.Y = Canvas.GetTop(this);
                    }

                }
            }
        }
        void Stretching()
        {
            if (StretchingObject != null)
            {
                if (_StretchingObject is Rectangle)
                {
                    StretchingObject.Width = this.Width;
                    StretchingObject.Height = this.Height;
                    Canvas.SetLeft(StretchingObject, Canvas.GetLeft(this));
                    Canvas.SetTop(StretchingObject, Canvas.GetTop(this));
                }
                else if (_StretchingObject is Polygon)
                {

                    double scalex = this.Width / initW;

                    double scaley = this.Height / initH;

                    PointCollection points = new PointCollection();
                    foreach (var point in initPoints)
                    {
                        Point point1 = new Point();
                        point1.X = point.X * scalex;
                        point1.Y = point.Y * scaley;
                        points.Add(point1);


                    }
                    _Polygon.Points = points;
                }
            }
        }




        #region 小矩形反向变换处理，保持视觉大小不变

        /// <summary>
        /// 缺陷框反向变换，在矩形框缩放的前提下保持label大小不变
        /// 思路：整体缩放之后对label施加一个逆向的线性变换
        /// 为了保证label的左下角与矩形框的左上角位置保持不变，线性变换的变换中心需要设置为（0，label.height）
        /// 当父控件缩放的时候，需要调用此方法以实现label大小不变
        /// </summary>
        /// <param name="scale"></param>
        public void ReverseTransform(double scale)
        {
            ScaleTransform scaleTransform = (ScaleTransform)((TransformGroup)rect1.RenderTransform).Children.First(tr => tr is ScaleTransform);
            scaleTransform.ScaleX = 1.0 / scale;
            scaleTransform.ScaleY = 1.0 / scale;
            scaleTransform.CenterX = 6;
            scaleTransform.CenterY = 6;

            scaleTransform = (ScaleTransform)((TransformGroup)rect2.RenderTransform).Children.First(tr => tr is ScaleTransform);
            scaleTransform.ScaleX = 1.0 / scale;
            scaleTransform.ScaleY = 1.0 / scale;
            scaleTransform.CenterX = 6;
            scaleTransform.CenterY = 6;

            scaleTransform = (ScaleTransform)((TransformGroup)rect3.RenderTransform).Children.First(tr => tr is ScaleTransform);
            scaleTransform.ScaleX = 1.0 / scale;
            scaleTransform.ScaleY = 1.0 / scale;
            scaleTransform.CenterX = 6;
            scaleTransform.CenterY = 6;

            scaleTransform = (ScaleTransform)((TransformGroup)rect4.RenderTransform).Children.First(tr => tr is ScaleTransform);
            scaleTransform.ScaleX = 1.0 / scale;
            scaleTransform.ScaleY = 1.0 / scale;
            scaleTransform.CenterX = 6;
            scaleTransform.CenterY = 6;

            scaleTransform = (ScaleTransform)((TransformGroup)rect5.RenderTransform).Children.First(tr => tr is ScaleTransform);
            scaleTransform.ScaleX = 1.0 / scale;
            scaleTransform.ScaleY = 1.0 / scale;
            scaleTransform.CenterX = 6;
            scaleTransform.CenterY = 6;

            scaleTransform = (ScaleTransform)((TransformGroup)rect6.RenderTransform).Children.First(tr => tr is ScaleTransform);
            scaleTransform.ScaleX = 1.0 / scale;
            scaleTransform.ScaleY = 1.0 / scale;
            scaleTransform.CenterX = 6;
            scaleTransform.CenterY = 6;

            scaleTransform = (ScaleTransform)((TransformGroup)rect7.RenderTransform).Children.First(tr => tr is ScaleTransform);
            scaleTransform.ScaleX = 1.0 / scale;
            scaleTransform.ScaleY = 1.0 / scale;
            scaleTransform.CenterX = 6;
            scaleTransform.CenterY = 6;

            scaleTransform = (ScaleTransform)((TransformGroup)rect8.RenderTransform).Children.First(tr => tr is ScaleTransform);
            scaleTransform.ScaleX = 1.0 / scale;
            scaleTransform.ScaleY = 1.0 / scale;
            scaleTransform.CenterX = 6;
            scaleTransform.CenterY = 6;
        }

        #endregion

        #region 小矩形鼠标事件处理 ，实现边框拉伸

        #region 矩形
        bool captured = false;
        //鼠标移动前一个点
        Point previousP;
        //控件移动前一个坐标
        Point shapeP;
        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect.CaptureMouse();
            captured = true;
            previousP = e.GetPosition(MyCanvas);
            rect.Cursor = Cursors.Hand;
            shapeP.X = Canvas.GetLeft(this);
            shapeP.Y = Canvas.GetTop(this);
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect.ReleaseMouseCapture();
            captured = false;
            rect.Cursor = Cursors.Arrow;
        }

        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (captured)
            {
                double x = e.GetPosition(MyCanvas).X;
                double y = e.GetPosition(MyCanvas).Y;
                shapeP.X += x - previousP.X;
                Canvas.SetLeft(this, shapeP.X);
                previousP.X = x;
                shapeP.Y += y - previousP.Y;
                Canvas.SetTop(this, shapeP.Y);
                previousP.Y = y;
                Stretching();
            }
        }
        #endregion

        #region 矩形1
        private void rect1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect1.CaptureMouse();
        }
        private void rect1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect1.ReleaseMouseCapture();
        }
        private void rect1_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentP = e.GetPosition(MyCanvas);

                double width = Canvas.GetLeft(this) - currentP.X + this.Width;
                if (width > 0)
                {
                    this.Width = width;
                    Canvas.SetLeft(this, currentP.X);
                }
                double height = Canvas.GetTop(this) - currentP.Y + this.Height;
                if (height > 0)
                {
                    this.Height = height;
                    Canvas.SetTop(this, currentP.Y);
                }
                Stretching();
            }
        }
        #endregion

        #region 矩形2
        private void rect2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect2.CaptureMouse();
        }
        private void rect2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect2.ReleaseMouseCapture();
        }
        private void rect2_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentP = e.GetPosition(MyCanvas);

                double height = Canvas.GetTop(this) - currentP.Y + this.Height;
                if (height > 0)
                {
                    this.Height = height;
                    Canvas.SetTop(this, currentP.Y);
                }
                Stretching();
            }
        }
        #endregion

        #region 矩形3
        private void rect3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect3.CaptureMouse();
        }
        private void rect3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect3.ReleaseMouseCapture();
        }
        private void rect3_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentP = e.GetPosition(MyCanvas);

                double width = currentP.X - Canvas.GetLeft(this);
                if (width > 0)
                {
                    this.Width = width;
                }
                double height = Canvas.GetTop(this) - currentP.Y + this.Height;
                if (height > 0)
                {
                    this.Height = height;
                    Canvas.SetTop(this, currentP.Y);
                }
                Stretching();
            }
        }
        #endregion

        #region 矩形4
        private void rect4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect4.CaptureMouse();
        }
        private void rect4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect4.ReleaseMouseCapture();
        }
        private void rect4_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentP = e.GetPosition(MyCanvas);

                double width = Canvas.GetLeft(this) - currentP.X + this.Width;
                if (width > 0)
                {
                    this.Width = width;
                    Canvas.SetLeft(this, currentP.X);
                }
                Stretching();
            }
        }
        #endregion
        #region 矩形5
        private void rect5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect5.CaptureMouse();
        }
        private void rect5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect5.ReleaseMouseCapture();
        }
        private void rect5_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentP = e.GetPosition(MyCanvas);

                double width = currentP.X - Canvas.GetLeft(this);
                if (width > 0)
                {
                    this.Width = width;
                }
            }
            Stretching();
        }
        #endregion


        #region 矩形6
        private void rect6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect6.CaptureMouse();
        }
        private void rect6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect6.ReleaseMouseCapture();
        }
        private void rect6_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentP = e.GetPosition(MyCanvas);

                double width = Canvas.GetLeft(this) - currentP.X + this.Width;
                if (width > 0)
                {
                    this.Width = width;
                    Canvas.SetLeft(this, currentP.X);
                }
                double height = currentP.Y - Canvas.GetTop(this);
                if (height > 0)
                {
                    this.Height = height;
                }
                Stretching();
            }
        }
        #endregion

        #region 矩形2
        private void rect7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect7.CaptureMouse();
        }
        private void rect7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect7.ReleaseMouseCapture();
        }
        private void rect7_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentP = e.GetPosition(MyCanvas);

                double height = currentP.Y - Canvas.GetTop(this);
                if (height > 0)
                {
                    this.Height = height;
                }
                Stretching();
            }
        }
        #endregion

        #region 矩形3
        private void rect8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect8.CaptureMouse();
        }
        private void rect8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            rect8.ReleaseMouseCapture();
        }
        private void rect8_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentP = e.GetPosition(MyCanvas);

                double width = currentP.X - Canvas.GetLeft(this);
                if (width > 0)
                {
                    this.Width = width;
                }
                double height = currentP.Y - Canvas.GetTop(this);
                if (height > 0)
                {
                    this.Height = height;
                }
                Stretching();
            }
        }

        #endregion

        #endregion


    }
}
