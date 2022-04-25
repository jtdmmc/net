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

namespace CanvasDrawing.DrawingTool.DrawingObject
{
    /// <summary>
    /// DefectMarking.xaml 的交互逻辑
    /// </summary>
    public partial class DefectMarking : UserControl
    {
        public DefectMarking()
        {
            InitializeComponent();
            this.Tag = DrawType.DefectMarking;
        }

        public DefectMarking(double x, double y) : this()
        {
            X = x;
            Y = y;
            Canvas.SetLeft(this, X);
            Canvas.SetTop(this, Y);
        }

        public double X { get; set; }

        public double Y { get; set; }

        public Action<DefectMarking> SelectStateChanged;

        Color SlectColor = (Color)ColorConverter.ConvertFromString("#A30014");
        Color DefalutColor = (Color)ColorConverter.ConvertFromString("#02A7F0");

        private bool _IsSelected;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;

                    Brush MyBrush = new SolidColorBrush(DefalutColor);
                    if (IsSelected)
                    {
                        MyBrush = new SolidColorBrush(SlectColor);
                    }

                    Rect1.Fill = MyBrush;
                    Rect2.Fill = MyBrush;
                    Rect3.Fill = MyBrush;
                    Rect4.Fill = MyBrush;
                    Ellipse1.Fill = MyBrush;

                    SelectStateChanged?.Invoke(this);
                }
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = !IsSelected;
            e.Handled = true;
        }
    }
}
