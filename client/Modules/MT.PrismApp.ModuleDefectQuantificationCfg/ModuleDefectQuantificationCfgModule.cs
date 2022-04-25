using MT.PrismApp.Common.Consts;
using MT.PrismApp.ModuleDefectQuantificationCfg.ViewModels;
using MT.PrismApp.ModuleDefectQuantificationCfg.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MT.PrismApp.ModuleDefectQuantificationCfg
{
    public class ModuleDefectQuantificationCfgModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>(RegionNames.DefectQuantificationCfg);
        }
    }
}