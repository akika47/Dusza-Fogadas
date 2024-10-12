using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF_Dusza.Pages
{
    /// <summary>
    /// Interaction logic for EventList.xaml
    /// </summary>
    public partial class EventList : Window
    {
        BettingRepository _repo;
        User? _currentUser;
        Window _window;
        public User? CurrentUser { get => _currentUser; }
        public ObservableCollection<Game> Games { get; set; }
        public EventList(BettingRepository repo, User? user)
        {
            _repo = repo;
            _currentUser = user;
            DataContext = this;
            InitializeComponent();
            Loaded += InitList;
            mi_logout.Click += (o, e) => WindowUtils.LogoutUser(_currentUser, this);

        }

        private async void InitList(object sender, RoutedEventArgs e)
        {
            var result = await _repo.GameRepository.GetGamesAsync().ToListAsync();
            Games = new(result);
            lvEvents.ItemsSource = Games;
        }

        private void GameSelected(object sender, MouseButtonEventArgs e)
        {

            Game selectedGame = ((ListViewItem)sender).Content as Game;
            if (selectedGame.IsGameOver) _window = new eventResults(_repo, selectedGame);
            else _window = new bettingPage(selectedGame, _repo, _currentUser);
            WindowUtils.ShowOtherWindow(this, _window);
        }
    }
}
