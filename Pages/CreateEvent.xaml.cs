using Org.BouncyCastle.Math.EC.Endo;
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
        public CreateEvent(User? user, BettingRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            _currentUser = user;
            NewRow = new() { IsDisplay = false };
            Loaded += InitCreatedGames;
            lvEvents.Items.Add(NewRow);
        }

        async void InitCreatedGames(object sender, RoutedEventArgs e)
        {
            var createdGames = _repo.GameRepository.GetGamesAsync().Where(x => x.OrganizerName == _currentUser.Name);
            await foreach (Game game in createdGames)
            {
                GameRow row = new()
                {
                    GameName = game.Name,
                    OrganizerName = game.OrganizerName,
                    Participants = game.DisplayParticipants,
                    Events = ,
                    IsDisplay = true
                };
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


    }
}
