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
	/// Interaction logic for eventResults.xaml
	/// </summary>
	public partial class eventResults : Window
	{
		Game _selectedGame;
		BettingRepository _repo;
		public Game SelectedGame { get => _selectedGame; }
		public List<Result> Results { get; set; } = [];
		
		public eventResults(BettingRepository repo,Game game)
		{
			_repo = repo;
			_selectedGame = game;
			InitializeComponent();
			Task.Run(async () =>
			{
				Results = await _repo.ResultRepository.GetResultsAsync(_selectedGame).ToListAsync();
			});

			DataContext = this;
		}
	}
}
