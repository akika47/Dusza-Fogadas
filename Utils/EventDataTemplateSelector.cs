using System.Windows;
using System.Windows.Controls;
using WPF_Dusza.Models;

namespace WPF_Dusza.Utils
{
    public class EventDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            GameRow row = item as GameRow;
            if(row.IsDisplay)
            {
                return element.FindResource("DisplayTemplate") as DataTemplate;
            }
            else
            {
                return element.FindResource("RowTemplate") as DataTemplate;
            }
        }
    }
}
