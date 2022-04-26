using System;
using System.Collections;
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

namespace MT.PrismApp.Resources.Controls
{
    /// <summary>
    /// CheckComBox.xaml 的交互逻辑
    /// </summary>
    public partial class CheckComBox : UserControl
    {
        /// <summary>
        /// 列表项model ，内部使用
        /// </summary>
        private ObservableCollection<Node> _nodeList = new ObservableCollection<Node>();

        /// <summary>
        /// 展示字段
        /// </summary>
        public string DisplayMember { get; set; }


        /// <summary>
        /// 注意ComboBoxItem的路由策略是RoutingStrategy.Tunnel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _borderbg_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void CheckBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckBox clickedBox = (CheckBox)sender;
            clickedBox.IsChecked = !clickedBox.IsChecked;
            e.Handled = true;
            if ("All".Equals(clickedBox.Content))
            {
                if (clickedBox.IsChecked.Value)
                {
                    foreach (Node node in _nodeList)
                    {
                        node.IsSelected = true;
                    }
                }
                else
                {
                    foreach (Node node in _nodeList)
                    {
                        node.IsSelected = false;
                    }
                }
            }
            else
            {
                int _selectedCount = 0;
                foreach (Node s in _nodeList)
                {
                    if (s.IsSelected && s.Title != "All")
                        _selectedCount++;
                }
                if (_selectedCount == _nodeList.Count - 1)
                    _nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = true;
                else
                    _nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = false;
            }
            SetSelectedItems();
        }

        private void SetSelectedItems()
        {
            List<object> temp = new List<object>();
            foreach (Node node in _nodeList)
            {
                if (node.IsSelected && node.Title != "All")
                {
                    temp.Add(ItemsSourceDic[node.Title]);
                }
            }

            SelectedItems = temp;
        }
        private void SelectNodes()
        {
            if (SelectedItems != null)
            {
                foreach (var item in SelectedItems)
                {
                    string displayValue = item.GetType().GetProperty(DisplayMember).GetValue(item).ToString();
                    Node node = _nodeList.FirstOrDefault(i => i.Title == displayValue);
                    if (node != null)
                        node.IsSelected = true;
                }
            }
        }
        private void SetText()
        {
            if (this.SelectedItems != null)
            {
                StringBuilder displayText = new StringBuilder();
                foreach (Node s in _nodeList)
                {
                    if (s.IsSelected == true && s.Title == "All")
                    {
                        displayText = new StringBuilder();
                        displayText.Append("All");
                        break;
                    }
                    else if (s.IsSelected == true && s.Title != "All")
                    {
                        displayText.Append(s.Title);
                        displayText.Append(',');
                    }
                }
                this.Text = displayText.ToString().TrimEnd(new char[] { ',' });
            }
            else
                this.Text = string.Empty;
            // set DefaultText if nothing else selected
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = this.DefaultText;
            }
        }
        public CheckComBox()
        {
            InitializeComponent();
        }

         public Action SelectedItemsChanged;

        public static readonly DependencyProperty SelectedItemsProperty =
         DependencyProperty.Register
         ("SelectedItems", typeof(IList),
         typeof(CheckComBox), new FrameworkPropertyMetadata(null,
         new PropertyChangedCallback
         (CheckComBox.OnSelectedItemsChanged)));

        private static void OnSelectedItemsChanged
        (DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckComBox control = (CheckComBox)d;
            control.SelectNodes();
            control.SetText();
            control.SelectedItemsChanged?.Invoke();
        }

        public static readonly DependencyProperty ItemsSourceProperty =
    DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CheckComBox), new FrameworkPropertyMetadata(null,
    new PropertyChangedCallback(CheckComBox.OnItemsSourceChanged)));
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckComBox control = (CheckComBox)d;
            control.OnItemsSourceChanged();
        }
        //public static readonly DependencyProperty ItemsSourceProperty =
        //    DependencyProperty.Register("ItemsSource", typeof(Dictionary<string, object>),
        //    typeof(MultiSelectComboBox), new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// ItemsSource改变处理
        /// </summary>
        private void OnItemsSourceChanged()
        {
            _nodeList.Clear();
            ItemsSourceDic.Clear();
            bool addAll = false;
            if (this.ItemsSource == null)
                this.ItemsSource = new List<object>();
            foreach (var item in this.ItemsSource)
            {
                if (!addAll)
                {
                    addAll = true;
                    _nodeList.Add(new Node("All"));
                }

                string displayValue = item.GetType().GetProperty(DisplayMember).GetValue(item).ToString();
                ItemsSourceDic.Add(displayValue, item);
                Node node = new Node(displayValue);
                _nodeList.Add(node);
            }
            MultiSelectCombo.ItemsSource = _nodeList;
        }

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text",
           typeof(string), typeof(CheckComBox),
           new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string),
            typeof(CheckComBox), new UIPropertyMetadata(string.Empty));

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public Dictionary<string, object> ItemsSourceDic = new Dictionary<string, object>();

        public IList SelectedItems
        {
            get
            {
                return (IList)GetValue(SelectedItemsProperty);
            }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }


    }
    public class Node : INotifyPropertyChanged
    {
        private string _title;
        private bool _isSelected;
        #region ctor
        public Node(string title)
        {
            Title = title;
        }
        #endregion

        #region Properties
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
