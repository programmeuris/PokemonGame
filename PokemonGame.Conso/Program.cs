using System;
using PokemonGame.DAL;
using PokemonGame.Conso;

namespace PokemonGame.Conso
{
    class Program
    {
        static void Main(string[] args)
        {
            //var _db = new FileReaderALaPokemonHerhalingOef3();
            var _db = new MockDb();
            var pokes = _db.GetPokemon("pokemonkaarten1.txt");

            foreach (var poke in pokes)
            {
                Console.WriteLine(poke.Naam);
            }
        }
    }
}
