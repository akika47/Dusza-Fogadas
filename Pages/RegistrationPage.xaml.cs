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
            User newUser = new()
            {
                Id = 1,
                Name = "Test",
                Password = "123",
                Points = 100,
                Role = 0
            };
            await _repo.RegisterUserAsync(newUser);
        }
    }
}
