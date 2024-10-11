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
using WPF_Dusza.Models;
using WPF_Dusza.Repo;
using WPF_Dusza.Utils;
using WPF_Dusza.Models;

namespace WPF_Dusza.Pages
{
	/// <summary>
	/// Interaction logic for bettingPage.xaml
	/// </summary>
	public partial class bettingPage : Window
	{
		Game SelectedGame { get; set; }
		BettingRepository _repo;
		User? _currentUser;
		Event _selectedEvent;
		Participant _selectedParticipant;
		public bettingPage(Game game, BettingRepository repo, User? user)
		{
			InitializeComponent();
			SelectedGame = game;
			_repo = repo;
			_currentUser = user;
			this.Loaded += async (o, e) => await FillComboboxes();
			cbxEvents.MouseDoubleClick += SelectEvent;
		}

        private void SelectEvent(object sender, MouseButtonEventArgs e)
        {
            if(sender is ComboBoxItem item)
			{
				_selectedEvent = (Event)item.Content;
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
			if(!int.TryParse(txtBetAmount.Text, out betAmount))
			{
				WindowUtils.DisplayErrorMessage("Hiba, a pontok csak számok lehetnek");
				return;
			}
			Bet bet = new Bet
			{
				EventId = _selectedEvent.Id,
				UserID = _currentUser!.Id,
				ParticipantId = _selectedParticipant!.Id,
				Prediction = txtPrediction.Text,
				BetAmount = betAmount
			};
			await _repo.BetRepository.PlaceBetAsync(bet);
		}
	}
}
