using System.Windows.Input;

namespace WPF_MVVM_Template.Core
{
    /// <summary>
    /// This class is responsible for the click event in buttons
    /// </summary>
    /// <param name="execute">the function that this class executes. This Action takes in an object
    /// as parameter</param>
    /// <param name="canexecute">the condition, in which the execute function executes if the 
    /// condition is true. If no condition is set, the function can execute everytime it's callled</param>
    public class RelayCommand(Action<object?> execute, Predicate<object?> canexecute) : ICommand
    {
        // we can't run nothing, so we'll throw an ArgumentNullException
        Action<object?> _execute = execute ?? throw new ArgumentNullException();
        Predicate<object?> _canexecute = canexecute;
        public RelayCommand(Action<object?> execute) : this(execute, null) { }
        public event EventHandler? CanExecuteChanged
        {
            // This ensures the condition is checked everytime it's changed
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        

        public bool CanExecute(object? parameter)
        {
            return _canexecute == null || _canexecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
