using MT.PrismApp.Common.Consts;
using MT.PrismApp.ModuleDrawingExample.ViewModels;
using MT.PrismApp.ModuleDrawingExample.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MT.PrismApp.ModuleDrawingExample
{
    public class ModuleDrawingExampleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>(RegionNames.DrawingExample);
        }
    }
}