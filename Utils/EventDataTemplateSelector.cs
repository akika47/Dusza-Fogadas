using System.Windows;
using System.Windows.Controls;
using WPF_Dusza.Models;

namespace WPF_Dusza.Utils
{
    public class EventDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = (FrameworkElement)container;
            GameRow row = (GameRow)item;
            if(row.IsDisplay)
            {
                return (DataTemplate)element.FindResource("DisplayTemplate");
            }
            else
            {
                return (DataTemplate)element.FindResource("RowTemplate");
            }
        }
    }
}
