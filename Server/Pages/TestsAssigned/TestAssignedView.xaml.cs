using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server.Pages.TestsAssigned
{
    /// <summary>
    /// Interaction logic for TestAssignedView.xaml
    /// </summary>
    public partial class TestAssignedView : UserControl
    {
        public TestAssignedView()
        {
            InitializeComponent();
        }
       
       
        public override void EndInit()
        {
            base.EndInit();
            if(DataContext is not null )
            {
                if(DataContext is TestAssignedViewModel vm)
                {
                    day.Value = vm.ActiveTime.Days;
                    hour.Value = vm.ActiveTime.Hours;
                    minute.Value = vm.ActiveTime.Minutes;
                }
                else
                {
                    throw new ArgumentException("DataContext is not TestAssignedViewModel");
                }
            }
        }
        private void TimeSpan_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(DataContext is not null 
                && DataContext is TestAssignedViewModel vm 
                && day.Value is not null 
                && hour.Value is not null 
                && minute.Value is not null)
            {
                vm.ActiveTime = new TimeSpan((int)day.Value, (int)hour.Value, (int)minute.Value, 0);
            }
        }
    }
}
