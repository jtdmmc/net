using MT.PrismApp.Common.Consts;
using MT.PrismApp.ModuleDefectTypeCfg.ViewModels;
using MT.PrismApp.ModuleDefectTypeCfg.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MT.PrismApp.ModuleDefectTypeCfg
{
    public class ModuleDefectTypeCfgModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>(RegionNames.DefectTypeCfg);
        }
    }
}