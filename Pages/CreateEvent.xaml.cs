using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateEvent.xaml
    /// </summary>
    public partial class CreateEvent : Window
    {
        public CreateEvent()
        {
            InitializeComponent();
        }

        private void btnCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            string eventName = txtEventName.Text;
            string organizerName = txtOrganizerName.Text;
            string player1Name = txtPlayerNames.Text.Split('-')[0];
            string player2Name = txtPlayerNames.Text.Split('-')[1];
            int multiplier = int.Parse(txtMultiplier.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void txtMultiplier_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
