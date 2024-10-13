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
using System.Windows.Shapes;

namespace WPF_Dusza.Pages
{
    /// <summary>
    /// Interaction logic for CloseEvent.xaml
    /// </summary>
    public partial class CloseEvent : Window
    {
        public CloseEvent()
        {
            InitializeComponent();
        }
		private void TextBox_MouseEnter(object sender, MouseEventArgs e)
		{
			var textBox = (TextBox)sender;
			if (textBox.ToolTip != null)
			{
				var toolTip = textBox.ToolTip as ToolTip;
				if (toolTip != null)
				{
					toolTip.IsOpen = true;
				}
			}
		}

		private void TextBox_MouseLeave(object sender, MouseEventArgs e)
		{
			var textBox = (TextBox)sender;
			if (textBox.ToolTip != null)
			{
				var toolTip = textBox.ToolTip as ToolTip;
				if (toolTip != null)
				{
					toolTip.IsOpen = false;
				}
			}
		}
	}
}
