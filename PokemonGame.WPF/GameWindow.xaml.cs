using System;
using System.Collections.Generic;
// using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PokemonGame.Models;
using PokemonGame.DAL;

namespace PokemonGame.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private MockDb _db = new MockDb();
        private List<Pokemon> _pokemon1 = new List<Pokemon>();
        private List<Pokemon> _pokemon2 = new List<Pokemon>();
        private Models.Pokemon _selectedP1;
        private Models.Pokemon _selectedP2;

        public GameWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void Attack(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (_selectedP1.Levend && _selectedP2.Levend)
            {
                switch (btn.Name)
                {
                    case "btnP1":
                        _selectedP2.Verdedigen(_selectedP1.Type, _selectedP1.Aanvallen());
                        UpdatePlayer2();
                        break;
                    case "btnP2":
                        _selectedP1.Verdedigen(_selectedP2.Type, _selectedP2.Aanvallen());
                        UpdatePlayer1();
                        break;
                }
            }
        }

        private void InitData()
        {
            // read pokemon from txt
            var _pokemon = _db.GetPokemon("pokemon.txt");

            //split into teams
            for (int i = 0; i < _pokemon.Count / 2; i++)
            {
                _pokemon1.Add(_pokemon[i]);
                _pokemon2.Add(_pokemon[i + _pokemon.Count / 2]);
            }

            cmbSpeler1.ItemsSource = _pokemon1;
            cmbSpeler2.ItemsSource = _pokemon2;

            UpdateBattleState();
        }

        private void UpdatePlayer1()
        {
            if (cmbSpeler1.SelectedIndex != -1)
            {
                _selectedP1 = cmbSpeler1.SelectedItem as Models.Pokemon;
                string uri1 = @"\images\" + _selectedP1.Afbeelding;
                imgSpeler1.Source = new BitmapImage(new Uri(uri1, UriKind.Relative));
            }

            if (_selectedP1 != null)
            {
                lblPok1.Content = _selectedP1.ToonGegevens();
                if (_selectedP1.Levend)
                {
                    cmbSpeler1.IsEnabled = false;
                }
                else
                {
                    _pokemon1.Remove(_selectedP1);

                    if (_pokemon1.Count == 0)
                    {
                        WinGame("Speler2");
                    }
                    else
                    {
                        cmbSpeler1.ItemsSource = null;
                        cmbSpeler1.ItemsSource = _pokemon1;
                    }

                    cmbSpeler1.IsEnabled = true;
                }
            }

            UpdateBattleState();
        }

        private void UpdatePlayer2()
        {
            if (cmbSpeler2.SelectedIndex != -1)
            {
                _selectedP2 = cmbSpeler2.SelectedItem as Models.Pokemon;
                string uri2 = @"\images\" + _selectedP2.Afbeelding;
                imgSpeler2.Source = new BitmapImage(new Uri(uri2, UriKind.Relative));
            }

            if (_selectedP2 != null)
            {
                lblPok2.Content = _selectedP2.ToonGegevens();
                if (_selectedP2.Levend)
                {
                    cmbSpeler2.IsEnabled = false;
                }
                else
                {
                    _pokemon2.Remove(_selectedP2);

                    if (_pokemon2.Count == 0)
                    {
                        WinGame("Speler1");
                    }
                    else
                    {
                        cmbSpeler2.ItemsSource = null;
                        cmbSpeler2.ItemsSource = _pokemon2;
                    }

                    cmbSpeler2.IsEnabled = true;
                }
            }

            UpdateBattleState();
        }

        private void UpdateBattleState()
        {
            btnP1.IsEnabled = false;
            btnP2.IsEnabled = false;

            if (_selectedP1 != null && _selectedP2 != null)
            {
                if (_selectedP1.Levend && _selectedP2.Levend)
                {
                    btnP1.IsEnabled = true;
                    btnP2.IsEnabled = true;
                }
            }
        }

        private void WinGame(string pName)
        {
            if (MessageBox.Show($"{pName} wins the game!", "Omae wa mou shindeiru!") == MessageBoxResult.OK)
            {
                App.Current.Shutdown();
            }
        }

        private void Poke1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePlayer1();
        }

        private void Poke2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePlayer2();
        }
    }
}