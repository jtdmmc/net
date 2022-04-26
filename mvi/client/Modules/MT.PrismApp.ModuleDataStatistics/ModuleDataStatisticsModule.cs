using MT.PrismApp.Common.Consts;
using MT.PrismApp.ModuleDataStatistics.ViewModels;
using MT.PrismApp.ModuleDataStatistics.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MT.PrismApp.ModuleDataStatistics
{
    public class ModuleDataStatisticsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>(RegionNames.DataStatistics);
        }
    }
}