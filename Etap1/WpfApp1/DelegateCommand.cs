﻿using System;
using System.Windows.Input;

namespace WpfApp1
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action<Object> action, Predicate<Object> predicate)
        {
        }
        public DelegateCommand(Action<Object> action) : this(action, null)
        {

        }

        public DelegateCommand(Action action)
        {
            _action = action;
        }
        public void Execute(object parameter)
        {
            _action();
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
    }
}