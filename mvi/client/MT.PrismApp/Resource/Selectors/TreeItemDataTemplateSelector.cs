using MT.PrismApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MT.PrismApp.Resource.Selectors
{
    public class TreeItemDataTemplateSelector : DataTemplateSelector
    {
        private DataTemplate firstTemplate;
        private DataTemplate secondTemplate;

        public DataTemplate FirstTemplate
        {
            get { return this.firstTemplate; }
            set { this.firstTemplate = value; }
        }
        public DataTemplate SecondTemplate
        {
            get { return this.secondTemplate; }
            set { this.secondTemplate = value; }
        }

        public TreeItemDataTemplateSelector() { }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            MenuViewModel menuViewModel = item as MenuViewModel;
            if (menuViewModel.Level == 1)
            {
                return this.FirstTemplate;
            }
            else
            {
                return this.SecondTemplate;
            }
        }

    }
}
