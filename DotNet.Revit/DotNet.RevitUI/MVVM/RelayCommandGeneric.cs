﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.RevitUI.MVVM
{
    /// <summary>
    /// 带参数通用命令类.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Windows.Input.ICommand" />
    class RelayCommand<T> : System.Windows.Input.ICommand
    {
        private readonly Action<T> m_Execute;
        private readonly Predicate<T> m_CanExecute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (m_CanExecute != null)
                    System.Windows.Input.CommandManager.RequerySuggested += value;

            }
            remove
            {
                if (m_CanExecute != null)
                    System.Windows.Input.CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {

        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            var flag = execute == null;
            if (flag)
                throw new ArgumentNullException("execute");

            m_Execute = execute;
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return m_CanExecute == null || m_CanExecute((T)((object)parameter));
        }

        public void Execute(object parameter)
        {
            m_Execute((T)((object)parameter));
        }
    }
}
