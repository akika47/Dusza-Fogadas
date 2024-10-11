using System.Windows;
using WPF_Dusza.Models;
using WPF_Dusza.Repo;
using WPF_Dusza.Utils;

namespace WPF_Dusza.Pages
{
    /// <summary>
    /// Interaction logic for CreateEvent.xaml
    /// </summary>
    public partial class CreateEvent : Window
    {

        readonly BettingRepository _repo;
        GameRow DisplayRow, NewRow;
        readonly User? _currentUser;
        public CreateEvent(BettingRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            NewRow = new() { IsDisplay = false };
            lvEvents.Items.Add(NewRow);
        }

        async void CreateEventAsync(object sender, RoutedEventArgs e)
        {
            List<Participant> participants = NewRow.Participants.Split('\n')
                .Select(x => new Participant { Name = x}).ToList();
            if(participants.Count > 2)
            {
                WindowUtils.DisplayErrorMessage("Több, mint 2 játékost nem lehet megadni");
                return;
            }
            Game NewGame = new()
            {
                Name = NewRow.GameName,
                OrganizerName = NewRow.OrganizerName,
                Participants = participants,
                IsGameOver = false
            };
            List<Event> events = NewRow.Events.Split('\n').Select(x => new Event { Name = x}).ToList();
            await _repo.GameRepository.CreateNewGameAsync(_currentUser!, NewGame, events);
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
