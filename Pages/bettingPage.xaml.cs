using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Dusza.Models;
using WPF_Dusza.Repo;
using WPF_Dusza.Utils;

namespace WPF_Dusza.Pages
{
	/// <summary>
	/// Interaction logic for bettingPage.xaml
	/// </summary>
	public partial class bettingPage : Window
	{
		public Game SelectedGame { get; set; }
		readonly BettingRepository _repo;
		User? _currentUser;
		Event _selectedEvent;
		Participant _selectedParticipant;
		public bettingPage(Game game, BettingRepository repo, User? user)
		{
			InitializeComponent();
			SelectedGame = game;
			DataContext = this;
			_repo = repo;
			_currentUser = user;
			Loaded += async (o, e) => await FillComboboxes();
		}



        private void SelectEvent(object sender, SelectionChangedEventArgs e)
        {
            if (cbxEvents.SelectedItem is Event selectedEvent)
            {
                _selectedEvent = selectedEvent;
            }
        }
        private void SelectParticipant(object sender, SelectionChangedEventArgs e)
        {
            if (cbxParticipants.SelectedItem is Participant selectedParticipant)
            {
                _selectedParticipant = selectedParticipant;
            }
        }
        async Task FillComboboxes()
		{
			List<Event> events = await _repo.EventRepository.GetEventsAsync(SelectedGame).ToListAsync();
			events.ForEach(e => cbxEvents.Items.Add(e));
			 SelectedGame.Participants.ForEach(x => cbxParticipants.Items.Add(x));
		}
        public async void PlaceBet(object sender, RoutedEventArgs e)
        {
            int betAmount;
            if (!int.TryParse(txtBetAmount.Text, out betAmount))
            {
                WindowUtils.DisplayErrorMessage("Hiba, a pontok csak számok lehetnek");
                return;
            }

            if (_selectedEvent == null)
            {
                WindowUtils.DisplayErrorMessage("Please select an event");
                return;
            }

            if (_selectedParticipant == null)
            {
                WindowUtils.DisplayErrorMessage("Please select a participant");
                return;
            }

            Bet bet = new()
            {
                EventId = _selectedEvent.Id,
                UserID = _currentUser!.Id,
                ParticipantId = _selectedParticipant!.Id,
                Prediction = txtPrediction.Text,
                BetAmount = betAmount
            };

            User modifiedUser = _currentUser with { Points = _currentUser.Points - betAmount };
            await _repo.BetRepository.PlaceBetAsync(bet);
            await _repo.UserRepository.ModifyUserAsync(modifiedUser);
            _currentUser = modifiedUser;

            MessageBox.Show("Bet placed successfully!");

            this.Close();
            var eventListPage = new EventList(_repo, _currentUser);
            eventListPage.ShowDialog();
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
