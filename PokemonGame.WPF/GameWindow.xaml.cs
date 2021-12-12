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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly MockDb _db = new MockDb();
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

            if (_selectedP1.IsAlive && _selectedP2.IsAlive)
            {
                // attack and update defending poke
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
            _pokemon1 = _db.GetPokemon("pokemon1.txt");
            _pokemon2 = _db.GetPokemon("pokemon2.txt");

            // init cbobox items
            cmbSpeler1.ItemsSource = _pokemon1;
            cmbSpeler2.ItemsSource = _pokemon2;

            UpdateBattleState();
        }

        private void UpdatePlayer1()
        {
            if (_selectedP1 != null)
            {
                lblPok1.Content = _selectedP1.ToonGegevens();
                if (_selectedP1.IsAlive)
                {
                    // only switch on death
                    cmbSpeler1.IsEnabled = false;
                }
                else
                {
                    // remove dead poke from team
                    _pokemon1.Remove(_selectedP1);

                    if (_pokemon1.Count == 0)
                    {
                        // win game when opponent runs out of pokes
                        WinGame("Speler2");
                    }
                    else
                    {
                        // update cbobox
                        cmbSpeler1.ItemsSource = null;
                        cmbSpeler1.ItemsSource = _pokemon1;
                    }

                    // allow player to pick new poke when one dies
                    cmbSpeler1.IsEnabled = true;
                }
            }

            UpdateBattleState();
        }

        private void UpdatePlayer2()
        {
            if (_selectedP2 != null)
            {
                lblPok2.Content = _selectedP2.ToonGegevens();
                if (_selectedP2.IsAlive)
                {
                    // only switch on death
                    cmbSpeler2.IsEnabled = false;
                }
                else
                {
                    // remove dead poke from team
                    _pokemon2.Remove(_selectedP2);

                    if (_pokemon2.Count == 0)
                    {
                        // win game when opponent runs out of pokes
                        WinGame("Speler1");
                    }
                    else
                    {
                        // update cbobox
                        cmbSpeler2.ItemsSource = null;
                        cmbSpeler2.ItemsSource = _pokemon2;
                    }

                    // allow player to pick new poke when one dies
                    cmbSpeler2.IsEnabled = true;
                }
            }

            UpdateBattleState();
        }

        private void UpdateBattleState()
        {
            // enable buttons when both poke are alive
            if ((_selectedP1 != null && _selectedP2 != null) &&
                (_selectedP1.IsAlive && _selectedP2.IsAlive))
            {
                btnP1.IsEnabled = true;
                btnP2.IsEnabled = true;
            }
            else
            {
                // defaults to disabled buttons
                btnP1.IsEnabled = false;
                btnP2.IsEnabled = false;
            }
        }

        private void WinGame(string pName)
        {
            if (MessageBox.Show($"{pName} wins the game!", "Omae wa mou shindeiru!") == MessageBoxResult.OK)
            {
                App.Current.Shutdown();
            }
        }

        private void Poke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbo = sender as ComboBox;
            switch (cbo.Name)
            {
                case "cmbSpeler1":
                    if (cmbSpeler1.SelectedIndex != -1)
                    {
                        // load in new poke when selecting one from combobox
                        _selectedP1 = cmbSpeler1.SelectedItem as Models.Pokemon;
                        string uri1 = @"\images\" + _selectedP1.Afbeelding;
                        imgSpeler1.Source = new BitmapImage(new Uri(uri1, UriKind.Relative));
                    }

                    UpdatePlayer1();
                    break;
                case "cmbSpeler2":
                    if (cmbSpeler2.SelectedIndex != -1)
                    {
                        // load in new poke when selecting one from combobox
                        _selectedP2 = cmbSpeler2.SelectedItem as Models.Pokemon;
                        string uri2 = @"\images\" + _selectedP2.Afbeelding;
                        imgSpeler2.Source = new BitmapImage(new Uri(uri2, UriKind.Relative));
                    }

                    UpdatePlayer2();
                    break;
            }
        }
    }
}