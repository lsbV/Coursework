// Ignore Spelling: MVVM

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
using TestLib.Abstractions;

namespace Client.MVVM_Task._Task
{
    /// <summary>
    /// Interaction logic for ChooseFromListV.xaml
    /// </summary>
    public partial class ChooseFromListV : UserControl
    {
        public ChooseFromListV()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                ListBoxItem listBoxItem = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItem);

                RadioButton? radioButton = FindVisualChild<RadioButton>(listBoxItem);
                if (radioButton != null)
                {
                    radioButton.IsChecked = true;
                }
            }
        }

        private T? FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
            {
                return null;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                if (child is T result)
                {
                    return result;
                }

                T? childItem = FindVisualChild<T>(child);
                if (childItem != null)
                {
                    return childItem;
                }
            }
            return null;
        }
    }
}
