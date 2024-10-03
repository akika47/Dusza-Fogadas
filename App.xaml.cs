using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WPF_MVVM_Template.Core;
using WPF_MVVM_Template.MVVM.View;
using WPF_MVVM_Template.Serivces;

namespace WPF_MVVM_Template
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// This is a service provider
        /// Service providers gets Added services from the servicecollection as needed
        /// </summary>
        IServiceProvider _provider;
        public App()
        {
            // This is a serviceCollection
            // ServiceCollections are Dependency Injection Container, which
            // Which auto-wires all the classes's dependencies
            // Like the main window has a INavigationService dependency, which the serviceCollection
            // automatically wires it to the main window
            IServiceCollection services = new ServiceCollection();
            //Add the MainWindow as a service, which get's the main viewmodel
            // Sets it's datacontext to MainViewModel
            services.AddSingleton<MainWindow>(provider => new MainWindow() 
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            // This adds all the necessary ViewModels here
            services.AddSingleton<MainViewModel>();
            // Add more ViewModels here
            // ...
            // This is a delegate, which takes in a type and outputs a ViewModel
            // provider => viewmodel => -- means we're passing a provider and a type as an argument
            services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewmodel => (ViewModelBase)provider.GetRequiredService(viewmodel));
            // Add the NavigationService to the service
            // Addsingleton<TService, TProvider> Means we're adding an interface called TService
            // Which TProvider implements on it's own
            services.AddSingleton<INavigationService, NavigationService>();
            // Build up the provider
            _provider = services.BuildServiceProvider();
        }
        /// <summary>
        /// Override the Application's basic startup routine
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Get the Main Window from the provider
            MainWindow window = _provider.GetRequiredService<MainWindow>();
            // Show the Main Window on the screen
            window.Show();
            base.OnStartup(e);
        }
    }

}
