using MT.PrismApp.Common.Consts;
using MT.PrismApp.ModuleWorkbench.ViewModels;
using MT.PrismApp.ModuleWorkbench.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MT.PrismApp.ModuleWorkbench
{
    public class ModuleWorkbenchModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>(RegionNames.Workbench);
        }
    }
}