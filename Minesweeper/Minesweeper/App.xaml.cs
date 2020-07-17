using Minesweeper.Core;
using Minesweeper.Modules.ModuleName;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Opens the defined window on application startup
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<Game>();
        }

        /// <summary>
        /// Registers all the type we need in the application to the IoC
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Game, GameViewModel>();
            containerRegistry.RegisterDialog<GameEnd, GameEndViewModel>("GameEnd");
            containerRegistry.RegisterDialog<Settings, SettingsViewModel>("Settings");
            containerRegistry.RegisterDialog<Statistics, StatisticsViewModel>("Statistics");
            containerRegistry.RegisterDialog<SaveGame, SaveGameViewModel>("AskSave");
        }

        /// <summary>
        /// Custom naming convention for the Prism's ViewModelLocator
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            //Configuring the ViewModelLocator to look for view models in the Core project
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var prefix = viewType.FullName.Replace("Minesweeper", "Core");

                var viewModelAssemblyName = typeof(RegionNames).Assembly.FullName;

                var viewModelName = $"{prefix}ViewModel, {viewModelAssemblyName}";

                return Type.GetType(viewModelName);
            });
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}