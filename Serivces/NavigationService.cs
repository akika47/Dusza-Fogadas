using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Template.Core;

namespace WPF_MVVM_Template.Serivces
{
    /// <summary>
    /// This is the interface for navigation different ViewModels
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// The current View Model that is presented on the screen
        /// </summary>
        public ViewModelBase CurrentViewModel { get; set; }
        /// <summary>
        /// Navigates to the specified ViewModel.
        /// </summary>
        /// <typeparam name="T">The View Model we want to navigate to</typeparam>
        public void NavigateTo<T>() where T : ViewModelBase;
    }
    /// <summary>
    /// This is the implementation of INavigationService
    /// </summary>
    /// <param name="factory">The viewModelFactory we pass to this class. This helps us automate
    /// the implementation for each ViewModel
    /// </param>
    public class NavigationService(Func<Type, ViewModelBase> factory) : ObservableObject, INavigationService
    {
        ViewModelBase _currentViewModel;
        Func<Type, ViewModelBase> _viewModelFactory = factory;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                //Set the current View Model to value
                _currentViewModel = value;
                // Redraw the UI, since it has been changed
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public void NavigateTo<T>() where T : ViewModelBase
        {
            // We get the current ViewModel from the factory
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(T));
            // Set the current ViewModel
            CurrentViewModel = viewModel;
        }
    }
}
