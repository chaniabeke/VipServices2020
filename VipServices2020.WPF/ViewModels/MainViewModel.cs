using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.WPF.State.Navigators;

namespace VipServices2020.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
