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

namespace WPF_Dusza.Pages
{
	/// <summary>
	/// Interaction logic for bettingPage.xaml
	/// </summary>
	public partial class bettingPage : Window
	{
		Game SelectedGame { get; set; }
		public bettingPage(Game game)
		{
			InitializeComponent();
			SelectedGame = game;
		}

		public async void PlaceBet(object sender, RoutedEventArgs e)
		{
			await Task.Delay(100);
		}
	}
}
