using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using VipServices2020.WPF.ViewModels;

namespace VipServices2020.WPF.State.Navigators
{
    public enum ViewType
    {
        SearchReservation,
        AddReservation
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModel { get; }
    }
}
