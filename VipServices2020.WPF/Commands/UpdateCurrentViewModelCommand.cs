using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using VipServices2020.WPF.State.Navigators;
using VipServices2020.WPF.ViewModels;

namespace VipServices2020.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.SearchReservation:
                        _navigator.CurrentViewModel = new SearchReservationViewModel();
                        break;
                    case ViewType.AddReservation:
                        _navigator.CurrentViewModel = new AddReservationViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}