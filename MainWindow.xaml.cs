
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
        RaceRepository _repo;
        // TODO: átadni _usert a többi oldalhoz
        User? _user;
        public MainWindow()
        {
            InitializeComponent();
            _repo = new();
        }

        void ValidateLogin(object sender, RoutedEventArgs e)
        {
            string Username = tb_UserName.Text, Password = Hashing.HashPassword(pb_Password.Password);

            _user = _repo.GetAllUsers().FirstOrDefault(x => x.Name == Username && x.Password == Password);
            if (_user == null)
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó");
                return;
            }
            //TODO: Átirányítani a megfelelő felületre
            switch (_user.Role) 
            {
                case 0:
                case 1:
                case 2:
                default:
                    break;
            }
        }

        void RegisterUser(object sender, RoutedEventArgs e) => new RegistrationPage(_repo).ShowDialog();

    }
}
