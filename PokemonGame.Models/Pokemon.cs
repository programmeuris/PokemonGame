using System;

namespace PokemonGame.Models
{
    public class Pokemon
    {
        // constructor
        public Pokemon(
            string naam,
            string type,
            int hp,
            string aanvalsOmschrijving1,
            int aanvalsFactor1,
            string aanvalsOmschrijving2,
            int aanvalsFactor2,
            string zwakte,
            int zwakteFactor,
            string weerstand,
            int weerstandsFactor,
            string afbeelding)
        {
            Naam = naam;
            Type = type;
            Hp = hp;
            AanvalsOmschrijving1 = aanvalsOmschrijving1;
            AanvalsOmschrijving2 = aanvalsOmschrijving2;
            AanvalsFactor1 = aanvalsFactor1;
            AanvalsFactor2 = aanvalsFactor2;
            Zwakte = zwakte;
            ZwakteFactor = zwakteFactor;
            Weerstand = weerstand;
            WeerstandsFactor = weerstandsFactor;
            Afbeelding = afbeelding;
        }

        // public methods
        public int Aanvallen()
        {
            // decide on 1 or 2
            int factor = _random.Next(0, 2) == 0 ?
                AanvalsFactor1 : AanvalsFactor2;

            // critical hit on 1 / 5
            if (_random.Next(0, 5) == 5)
            {
                factor *= 2;
            }

            return factor;
        }

        public void Verdedigen(string aanvalstype, int attack)
        {
            int attackVal = attack;

            if (aanvalstype.Equals(Zwakte))
            {
                // super effective
                attackVal *= ZwakteFactor;
            }
            else if (aanvalstype.Equals(Weerstand))
            {
                // not very effective
                attackVal -= WeerstandsFactor;
            }

            Hp -= attackVal;
        }

        public string ToonGegevens() => IsAlive ?
            $"{Naam} is nog levend.{Environment.NewLine}Resterende HP: {Hp}" :
            $"{Naam} is dood.{Environment.NewLine}Resterende HP: {Hp}";

        // private variables
        private int _aanvalsFactor1;
        private int _aanvalsFactor2;
        private string _aanvalsOmschrijving1;
        private string _aanvalsOmschrijving2;
        private string _afbeelding;
        private int _hp;
        private string _naam;
        private string _type;
        private string _weerstand;
        private int _weerstandsFactor;
        private string _zwakte;
        private int _zwakteFactor;

        private Random _random = new Random();

        // public properties
        public bool IsAlive { get => Hp > 0; }

        public int AanvalsFactor1
        {
            get => _aanvalsFactor1;
            set => _aanvalsFactor1 = value;
        }

        public int AanvalsFactor2
        {
            get => _aanvalsFactor2;
            set => _aanvalsFactor2 = value;
        }

        public string AanvalsOmschrijving1
        {
            get => _aanvalsOmschrijving1;
            set => _aanvalsOmschrijving1 = value;
        }

        public string AanvalsOmschrijving2
        {
            get => _aanvalsOmschrijving2;
            set => _aanvalsOmschrijving2 = value;
        }

        public string Afbeelding
        {
            get => _afbeelding;
            set => _afbeelding = value;
        }

        public int Hp
        {
            get => _hp;
            set => _hp = value >= 0 ? value : 0;
        }

        public string Naam
        {
            get => _naam;
            set => _naam = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string Weerstand
        {
            get => _weerstand;
            set => _weerstand = value;
        }

        public int WeerstandsFactor
        {
            get => _weerstandsFactor;
            set => _weerstandsFactor = value;
        }

        public string Zwakte
        {
            get => _zwakte;
            set => _zwakte = value;
        }

        public int ZwakteFactor
        {
            get => _zwakteFactor;
            set => _zwakteFactor = value;
        }
    }
}
