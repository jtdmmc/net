using MVI.DotnetSoftwarePlatform.Api.Thrift;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MT.PrismApp.Resource.Converters
{
    /// <summary>
    /// 计算 <see cref="System.Windows.Controls.TreeViewItem"/> 的缩进的转换器。
    /// </summary>
    [ValueConversion(typeof(TreeViewItem), typeof(Thickness))]
    public sealed class IndentConverter : IValueConverter
    {
        /// <summary>
        /// 获取或设置缩进的像素个数。
        /// </summary>
        public double Indent { get; set; }
        /// <summary>
        /// 获取或设置初始的左边距。
        /// </summary>
        public double MarginLeft { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);
            int level = this.GetLevels(item);
            return new Thickness(Indent * level, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        // 获取 当前元素的在TreeView中的层级
        public int GetLevels(TreeViewItem item)
        {
            int level = 0;
            Type tree = typeof(TreeView);

            FrameworkElement elem = item.Parent as FrameworkElement;
            while (elem != null && elem.GetType() != tree)
            {
                level++;
                elem = elem.Parent as FrameworkElement;
            }

            return level;
        }
    }
}


