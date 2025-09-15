using System;
namespace OSK.MVVM
{
    public interface ICommand
    {
        void Execute(object param = null);
        bool CanExecute(object param = null);
        event Action CanExecuteChanged;
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public event Action CanExecuteChanged;

        public RelayCommand(Action<object> exec, Func<object, bool> canExec = null)
        {
            _execute = exec;
            _canExecute = canExec;
        }

        public void Execute(object param = null) => _execute?.Invoke(param);
        public bool CanExecute(object param = null) => _canExecute == null || _canExecute(param);
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke();
    }
}