using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Infrastructure
{
    public class ChangePageMessage
    {
        public BaseViewModel ViewModel { get; set; }

        public ChangePageMessage(BaseViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
