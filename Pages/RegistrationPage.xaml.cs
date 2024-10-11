using System.Windows;
using WPF_Dusza.Models;
using WPF_Dusza.Repo;
using WPF_Dusza.Utils;

namespace WPF_Dusza.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        BettingRepository _repo;
        public RegistrationPage(BettingRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        async void RegisterUser(object sender, RoutedEventArgs e)
        {
            string Username = tb_UserName.Text, 
                Password = pb_Password.Password,
                Confirm = pb_Confirm.Password;
            User user = new()
            {
                Name = Username,
                Password = Hashing.HashPassword(Password),
                Points = 50,
                Role = 2
            };
            if (await _repo.UserRepository.GetAllUsersAsync().AnyAsync(x => x == user)) 
            {
                MessageBox.Show("Ez a felhasználó már létezik!\n");
                return;
            }
            if (Password != Confirm) 
            {
                MessageBox.Show("A jelszavak nem egyeznek!\n");
                return;
            }
            await _repo.UserRepository.RegisterUserAsync(user);
            if (MessageBox.Show("Felhasználó sikeresen regisztrálva", "Successfull registration",
               MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK) Close();
            
        }
    }
}
