using log4net;
using MT.PrismApp.Services.AccountService;
using MT.PrismApp.Services.Interfaces;
using MT.PrismApp.ViewModels;
using MT.PrismApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Diagnostics;
using System.Windows;

namespace MT.PrismApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Info("Startup ===================>>>");
            base.OnStartup(e);
        }

        protected override void OnInitialized()
        {
            MainWindowViewModel mainWindowViewModel = MainWindow.DataContext as MainWindowViewModel;
            mainWindowViewModel.Show();
            //base.OnInitialized();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Log.Info("<<<======================End");
            base.OnExit(e);
        }
        protected override Window CreateShell()
        {    
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAccountService, AccountService>();

            containerRegistry.RegisterSingleton<LoginWindow>();
            containerRegistry.RegisterSingleton<LoginWindowViewModel>();
            containerRegistry.RegisterSingleton<MainWindow>();
            containerRegistry.RegisterSingleton<MainWindowViewModel>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}
