using CanvasDrawing.DrawingTool;
using Microsoft.Win32;
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

namespace MT.PrismApp.ModuleDrawingExample.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewA : UserControl
    {
        public ViewA()
        {
            InitializeComponent();
            drawingCanvas.ImageUri = "pack://application:,,,/MT.PrismApp.Resources;component/Images/u303.png";
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (rbNone.IsChecked == true)
            {
                drawingCanvas.CurrentDrawType = DrawType.None;
            }
            if (rbRect.IsChecked == true)
            {
                drawingCanvas.CurrentDrawType = DrawType.Rectangle;
            }
            if (rbPoly.IsChecked == true)
            {
                drawingCanvas.CurrentDrawType = DrawType.Polygon;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件(*.jpg,*.gif,*.bmp,*.png)|*.jpg;*.gif;*.bmp;*.png";
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                try
                {
                    drawingCanvas.ImageUri = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
