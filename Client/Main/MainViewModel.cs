using Client.Infrastructure;

namespace Client.Main
{
    internal class MainViewModel : BaseViewModel
    {
        private ApplicationController controller;

        public MainViewModel(ApplicationController controller)
        {
            this.controller = controller;
        }
    }
}