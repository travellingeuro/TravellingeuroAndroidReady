using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace travellingeuro.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public INavigationService navigationService { get; private set; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
