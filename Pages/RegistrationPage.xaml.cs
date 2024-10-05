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

namespace WPF_Dusza.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        RaceRepository _repo;
        public RegistrationPage(RaceRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        async void RegisterUser()
        {
            string Username = tb_UserName.Text, 
                Password = pb_Password.Password,
                Confirm = pb_Confirm.Password;
            User user = new()
            {
                Name = Username,
                Password = Hashing.HashPassword(Password),
                Points = 100,
                Role = 2
            };
            if (_repo.GetAllUsers().Any(x => x == user)) 
            {
                MessageBox.Show("Ez a felhasználó már létezik!\n");
                return;
            }
            if (Password != Confirm) 
            {
                MessageBox.Show("A jelszavak nem egyeznek!\n");
                return;
            }
            await _repo.RegisterUserAsync(user);
        }
    }
}
