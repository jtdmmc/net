using MT.PrismApp.Common.Consts;
using MT.PrismApp.ModulePowerMgr.ViewModels;
using MT.PrismApp.ModulePowerMgr.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.PrismApp.ModulePowerMgr
{
    public class ModulePowerMgrModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PowerMgrView, PowerMgrViewModel>(RegionNames.PowerMgr);
        }
    }
}
