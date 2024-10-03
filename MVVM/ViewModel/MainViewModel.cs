using WPF_MVVM_Template.Core;
using WPF_MVVM_Template.Serivces;

namespace WPF_MVVM_Template.MVVM.ViewModel
{
    /// <summary>
    /// This is the MainViewModel.
    /// This is the code-behind for the Main View.
    /// </summary>
    /// <param name="service">The NavigationService's single instance</param>
    public class MainViewModel(INavigationService service) : ViewModelBase
    {
        INavigationService _navigationService = service;
        public INavigationService Navigation { get => _navigationService; }
    }
}
