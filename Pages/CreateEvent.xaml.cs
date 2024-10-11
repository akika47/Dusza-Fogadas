using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF_Dusza.Models;
using WPF_Dusza.Repo;
using WPF_Dusza.Utils;
using WPF_Dusza.Models;
using System.ComponentModel;

namespace WPF_Dusza.Pages
{
    /// <summary>
    /// Interaction logic for CreateEvent.xaml
    /// </summary>
    public partial class CreateEvent : Window
    {

        BettingRepository _repo;
        GameRow DisplayRow, NewRow;
        public CreateEvent(BettingRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            NewRow = new() { IsDisplay = false };
            //var EditableRow = new { RowItem = new GameRowItem(), CreateButton = new Button() { Content = "Újesemény létrehozása" } };
            //EditableRow.CreateButton.Click += async (o,e) => await CreateEventAsync();
            lvEvents.Items.Add(NewRow);

        }

        async void CreateEventAsync(object sender, RoutedEventArgs e)
        {
            await Dispatcher.BeginInvoke(() =>
            {
                DisplayRow = new GameRow
                {
                    GameName = NewRow.GameName,
                    OrganizerName = NewRow.OrganizerName,
                    Participants = NewRow.Participants,
                    Events = NewRow.Events,
                    IsDisplay = true
                };
                int firstIndex = lvEvents.Items.IndexOf(NewRow);
                lvEvents.Items.Insert(firstIndex, DisplayRow);
                DisplayRow = new() { IsDisplay = false };
            });
        }


    }
}
