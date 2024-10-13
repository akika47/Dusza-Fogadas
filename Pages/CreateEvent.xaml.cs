using Org.BouncyCastle.Math.EC.Endo;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        GameRow DisplayRow, NewRow, SelectedRow;
        readonly User? _currentUser;
        public CreateEvent(User? user, BettingRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            _currentUser = user;
            NewRow = new() { IsDisplay = false };
            Loaded += InitCreatedGames;
            lvEvents.Items.Add(NewRow);
            lvEvents.MouseDown += LvEvents_MouseDown1;
        }

        private void LvEvents_MouseDown1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var clickedItem = (ListViewItem)VisualTreeHelper.GetParent((DependencyObject)e.OriginalSource);
                SelectedRow = (GameRow)clickedItem.Content;
            }
        }

        private void LvEvents_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        async void InitCreatedGames(object sender, RoutedEventArgs e)
        {
            List<GameRow> rows = await _repo.GameRepository.GetGamesAsync(_currentUser!);
            foreach (GameRow row in rows) 
            {  
                lvEvents.Items.Add(row);
            }

        }

        async void CreateEventAsync(object sender, RoutedEventArgs e)
        {
            int LastID = await _repo.GameRepository.GetGamesAsync().MaxAsync(x => x.Id);
            List<Participant> participants = NewRow.Participants.Split('\n')
                .Select(x => new Participant { Name = x }).ToList();
            if (participants.Count > 2)
            {
                WindowUtils.DisplayErrorMessage("Több, mint 2 játékost nem lehet megadni");
                return;
            }
            Game NewGame = new()
            {
                Id = LastID + 1,
                Name = NewRow.GameName,
                OrganizerName = NewRow.OrganizerName,
                Participants = participants,
                IsGameOver = false
            };
            List<Event> events = NewRow.Events.Replace("\r", "").Split('\n').Select(x => new Event { Name = x }).ToList();
            await _repo.GameRepository.CreateNewGameAsync(_currentUser!, NewGame, events, participants);
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
        async void CloseGame(object sender, RoutedEventArgs e)
        {
            int Id = await _repo.GameRepository.GetGamesAsync().Where(x => x.Name == SelectedRow.GameName).Select(x => x.Id).FirstAsync();
            Game game = new()
            {
                Id = Id,
                Name = SelectedRow.GameName,
                OrganizerName = SelectedRow.OrganizerName,
            };
            await _repo.GameRepository.CloseGameAsync(game);
        }

    }
}
