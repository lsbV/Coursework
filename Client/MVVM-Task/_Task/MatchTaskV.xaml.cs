using Client.MVVM_Task._Answer;
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

namespace Client.MVVM_Task._Task
{
    /// <summary>
    /// Interaction logic for MatchTaskV.xaml
    /// </summary>
    public partial class MatchTaskV : UserControl
    {
        public MatchTaskV()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (left.SelectedItem != null && right.SelectedItem != null)
            //{
            //    var leftVM = (MatchAnswerVM)left.SelectedItem;
            //    var rightVM = (MatchAnswerVM)right.SelectedItem;

            //    //(leftVM.Content as MatchAnswerVM)!.JoinCommand.Execute(rightVM.Content);


            //    var height = Math.Max(leftVM.ActualHeight, rightVM.ActualHeight);

            //    leftVM.Height = height;
            //    rightVM.Height = height;
            //}
        }
    }
}
