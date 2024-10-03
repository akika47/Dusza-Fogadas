using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_Template.Core
{
    /// <summary>
    /// This class is Responsible for recalling the UI once a property is changed.
    /// <note>This class implements INotifyPropertyChanged.</note>
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Fires the PropertyChanged event, that tells the app to redraw the UI
        /// </summary>
        /// <param name="property">The property that has been changed. It's good practice to write
        /// the property's name with the nameof() operator</param>
        protected void OnPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
