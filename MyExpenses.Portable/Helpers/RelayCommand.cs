﻿//The MIT License (MIT)
//Copyright (c) 2015 Xamarin
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of
//this software and associated documentation files (the "Software"), to deal in
//the Software without restriction, including without limitation the rights to
//use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//the Software, and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//    FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//    COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//    IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//    CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Windows.Input;

namespace MyExpenses.Portable.Helpers
{
  public class RelayCommand : ICommand
  {
    private readonly Action handler;
    private bool isEnabled;
    private readonly Func<bool> canExecute;

    public RelayCommand(Action handler, Func<bool> canExecute = null)
    {
      this.handler = handler;
      this.canExecute = canExecute;
      if (canExecute == null)
        isEnabled = true;
    }


    public bool IsEnabled
    {
      get { return isEnabled; }
      set
      {
        if (value != isEnabled)
        {
          isEnabled = value;
          if (CanExecuteChanged != null)
          {
            CanExecuteChanged(this, EventArgs.Empty);
          }
        }
      }
    }

    public bool CanExecute(object parameter)
    {
      if (canExecute != null)
        IsEnabled = canExecute();

      return IsEnabled;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      handler();
    }

    /// <summary>
    /// Method used to raise the <see cref="CanExecuteChanged"/> event
    /// to indicate that the return value of the <see cref="CanExecute"/>
    /// method has changed.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
      var handler = CanExecuteChanged;
      if (handler != null)
      {
        handler(this, EventArgs.Empty);
      }
    }
  }

  public class RelayCommand<T> : ICommand
  {
    private readonly Action<T> handler;
    private bool isEnabled = true;

    private readonly Func<T, bool> canExecute;

    public RelayCommand(Action<T> handler, Func<T, bool> canExecute = null)
    {
      this.handler = handler;
      this.canExecute = canExecute;
      if (canExecute == null)
        isEnabled = true;
    }

    public bool IsEnabled
    {
      get { return isEnabled; }
      set
      {
        if (value != isEnabled)
        {
          isEnabled = value;
          if (CanExecuteChanged != null)
          {
            CanExecuteChanged(this, EventArgs.Empty);
          }
        }
      }
    }

    public bool CanExecute(object parameter)
    {
      if (canExecute != null)
        IsEnabled = canExecute((T)parameter);

      return IsEnabled;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      handler((T)parameter);
    }

    /// <summary>
    /// Method used to raise the <see cref="CanExecuteChanged"/> event
    /// to indicate that the return value of the <see cref="CanExecute"/>
    /// method has changed.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
      var handler = CanExecuteChanged;
      if (handler != null)
      {
        handler(this, EventArgs.Empty);
      }
    }
  }
}
