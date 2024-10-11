
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
using WPF_Dusza.Pages;
using WPF_Dusza.Repo;
using WPF_Dusza.Utils;


namespace WPF_Dusza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BettingRepository _repo;
        User? _user;
        Window _currentWindow;
        public MainWindow()
        {
            InitializeComponent();
            _repo = new();
            _user = null;
        }

        async void ValidateLogin(object sender, RoutedEventArgs e)
        {
            string Username = tb_UserName.Text, 
            Password = Hashing.HashPassword(pb_Password.Password);
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) 
            {
                WindowUtils.DisplayErrorMessage("Hibás felhasználónév vagy jelszó");
                return;
            }
            _user = await _repo.UserRepository.GetAllUsersAsync().FirstOrDefaultAsync(x => x.Name == Username && x.Password == Password);
            if (_user == null)
            {
                WindowUtils.DisplayErrorMessage("Hibás felhasználónév vagy jelszó");
                return;
            }
            switch (_user.Role) 
            {
                case 0:
                    //user is admin
                    _currentWindow = new AdminPage();
                    break;
                case 1:
                    //user is organizer
                    _currentWindow = new CreateEvent(_repo); 
                    break;
                case 2:
                    //user is betting
                    _currentWindow = new EventList(_repo, _user);
                    break;
            }
            WindowUtils.ShowOtherWindow(this, _currentWindow);
        }

        void RegisterUser(object sender, RoutedEventArgs e) 
        {
            _currentWindow = new RegistrationPage(_repo);
            WindowUtils.ShowOtherWindow(this, _currentWindow);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _currentWindow = new bettingPage();
            WindowUtils.ShowOtherWindow(this, _currentWindow);
        }
    }
}
