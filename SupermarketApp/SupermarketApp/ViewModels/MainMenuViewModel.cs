using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        private Navigation navigation;

        public MainMenuViewModel() { }

        public MainMenuViewModel(Navigation navigation)
        {
            this.navigation = navigation;
        }
    }
}
