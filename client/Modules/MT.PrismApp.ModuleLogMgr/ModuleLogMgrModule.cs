using MT.PrismApp.Common.Consts;
using MT.PrismApp.ModuleLogMgr.ViewModels;
using MT.PrismApp.ModuleLogMgr.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MT.PrismApp.ModuleLogMgr
{
    public class ModuleLogMgrModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>(RegionNames.LogMgr);
        }
    }
}