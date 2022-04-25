using MT.PrismApp.Common.Consts;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MT.PrismApp.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        static Dictionary<string, MenuViewModel> _menus = new Dictionary<string, MenuViewModel>();

        static MenuViewModel()
        {
            //权限管理
            MenuViewModel menuViewModel = new MenuViewModel();
            menuViewModel.Id = "0";
            menuViewModel.Name = "工作台";
            menuViewModel.Level = 1;
            menuViewModel.ParentId = string.Empty;
            menuViewModel.RegionName = RegionNames.Workbench;
            menuViewModel.PowerName = "工作台";
            _menus.Add(menuViewModel.Id, menuViewModel);
            //权限管理
             menuViewModel = new MenuViewModel();
            menuViewModel.Id = "1";
            menuViewModel.Name = "权限管理";
            menuViewModel.Level = 1;
            menuViewModel.ParentId = string.Empty;
            menuViewModel.RegionName = RegionNames.PowerMgr;
            menuViewModel.PowerName = "权限管理";
            _menus.Add(menuViewModel.Id, menuViewModel);

            //日志管理
            menuViewModel = new MenuViewModel();
            menuViewModel.Id = "2";
            menuViewModel.Name = "日志管理";
            menuViewModel.Level = 1;
            menuViewModel.ParentId = string.Empty;
            menuViewModel.RegionName = RegionNames.LogMgr;
            menuViewModel.PowerName = "日志管理";
            _menus.Add(menuViewModel.Id, menuViewModel);

            //数据统计
            menuViewModel = new MenuViewModel();
            menuViewModel.Id = "3";
            menuViewModel.Name = "数据统计";
            menuViewModel.Level = 1;
            menuViewModel.ParentId = string.Empty;
            menuViewModel.RegionName = RegionNames.DataStatistics;
            menuViewModel.PowerName = "数据统计";
            _menus.Add(menuViewModel.Id, menuViewModel);

            //配置管理
            menuViewModel = new MenuViewModel();
            menuViewModel.Id = "4";
            menuViewModel.Name = "配置管理";
            menuViewModel.Level = 1;
            menuViewModel.ParentId = string.Empty;
            menuViewModel.RegionName = string.Empty;
            menuViewModel.PowerName = string.Empty;
            _menus.Add(menuViewModel.Id, menuViewModel);
            //缺陷类型配置
            menuViewModel = new MenuViewModel();
            menuViewModel.Id = "41";
            menuViewModel.Name = "缺陷类型配置";
            menuViewModel.Level = 2;
            menuViewModel.ParentId = "4";
            menuViewModel.RegionName = RegionNames.DefectTypeCfg;
            menuViewModel.PowerName = "缺陷类型配置";
            _menus.Add(menuViewModel.Id, menuViewModel);
            //缺陷量化配置
            menuViewModel = new MenuViewModel();
            menuViewModel.Id = "42";
            menuViewModel.Name = "缺陷量化配置";
            menuViewModel.Level = 2;
            menuViewModel.ParentId = "4";
            menuViewModel.RegionName = RegionNames.DefectQuantificationCfg;
            menuViewModel.PowerName = "缺陷量化配置";
            _menus.Add(menuViewModel.Id, menuViewModel);

            //绘制示例
            menuViewModel = new MenuViewModel();
            menuViewModel.Id = "5";
            menuViewModel.Name = "绘制示例";
            menuViewModel.Level = 1;
            menuViewModel.ParentId = string.Empty;
            menuViewModel.RegionName = RegionNames.DrawingExample;
            menuViewModel.PowerName = "绘制示例";
            _menus.Add(menuViewModel.Id, menuViewModel);
        }
        static public List<MenuViewModel> GetMenus(TAccount account)
        {
            List<MenuViewModel> ls = _menus.Values.ToList();
            if (account.UniqueName == "user1")
            {
                ls.Remove(_menus["2"]);
            }
            if (account.UniqueName == "user2")
            {
                ls.Remove(_menus["3"]);
                ls.Remove(_menus["42"]);
            }



            foreach (MenuViewModel menux in ls.ToList())
            {
                if (menux.SubItems == null)
                    menux.SubItems = new List<MenuViewModel>();
                menux.SubItems.Clear();
                foreach (MenuViewModel menuy in ls.ToList())
                {
                    if (menux.Id == menuy.ParentId)
                    {
                        ls.Remove(menuy);
                        menux.SubItems.Add(menuy);
                    }
                }
            }
            return ls;
        }

        public string Id { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string PowerName { get; set; }
        public List<MenuViewModel> SubItems { get; set; }


        public string NormalIcon { get; set; }
        public string SelectedIcon { get; set; }

        private string icon;
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value)
                {
                    Icon = SelectedIcon;
                }
                else
                {
                    Icon = NormalIcon;
                }
                SetProperty(ref isSelected, value);
            }
        }
    }
}
