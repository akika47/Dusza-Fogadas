using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public ObservableCollection<User> Users { get; set; }
               readonly BettingRepository _repo;
        User _selectedUser;
        public AdminPage(BettingRepository repo)
        {
            InitializeComponent();
            _repo = repo;

            Users = new ObservableCollection<User>();
            DataContext = this;

            LoadUsers();
        }

        private async void LoadUsers()
        {
            var result = await _repo.UserRepository.GetAllUsersAsync()
                .Where(x => x.Role == 1)
                .ToListAsync();

            Users.Clear();

            foreach (var user in result)
            {
                Users.Add(user);
            }

        }

        void AddUser(object sender, RoutedEventArgs e)
        {
           if(SameUser())
            {
                WindowUtils.DisplayErrorMessage("Ugyanazt a Szervezőt nem tudod hozzáadni");
                return;
            }
           User NewUser = _selectedUser with { Name = txtOrganizerName.Text, 
               Password = Hashing.HashPassword(txtOrganizerPassword.Password) };
           Users.Add(NewUser);  

        }
        bool SameUser() => _selectedUser.Name == txtOrganizerName.Text &&
              _selectedUser.Password == txtOrganizerPassword.Password;
        void ModifyUser(object sender, RoutedEventArgs e) 
        {
            if(SameUser())
            {
                WindowUtils.DisplayErrorMessage("A szervezőt nem módosítottad!");
                return;
            }
            User ModifiedUser = _selectedUser with {Name = txtOrganizerName.Text };
            Users[Users.IndexOf(_selectedUser)] = ModifiedUser;
        }
        void DeleteUser(object sender, RoutedEventArgs e)
        {
            if(!SameUser())
            {
                WindowUtils.DisplayErrorMessage("Hiba! Módosított szervezőt nem lehet törölni");
                return;
            }
            Users.Remove(_selectedUser);
        }
        private async void HandleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    await _repo.UserRepository.RegisterUserAsync(e.NewItems[0] as User);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    await _repo.UserRepository.ModifyUserAsync(e.NewItems[0] as User);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    await _repo.UserRepository.DeleteUserAsync((e.NewItems[0] as User)!.Id);
                    break;
            }
        }

        private void dtgOrganizers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem is (User))
            {
                _selectedUser = (User)(sender as DataGrid)!.SelectedItem;
                if (_selectedUser == null) return;
                txtOrganizerName.Text = _selectedUser.Name;
            }

        }
    }
}
