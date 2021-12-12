using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokemonGame.DAL
{
    public class FileReaderALaPokemonHerhalingOef3
    {
        // if file exists, return all lines in file as array
        // if not, return null
        private string[] ReadArr(string fName) => File.Exists(fName) ?
            File.ReadAllLines(fName) : null;

        // if file exists, generate a list of pokemon
        // if not, return null
        public List<Models.Pokemon> GetPokemon(string fName)
        {
            // get all lines in file, if file exists
            var lines = ReadArr(fName);
            var pokemon = new List<Models.Pokemon>();

            // turn each line into a pokemon
            foreach (string line in lines)
            {
                // split lines
                var split = line.Split(';');

                // name type hp
                string name = split[0];
                string type = split[1];
                int hp = -1;
                int.TryParse(split[2], out hp);

                // att1 and att2
                string att1Name = split[3];
                int att1Val = -1;
                int.TryParse(split[4], out hp);
                string att2Name = split[5];
                int att2Val = -1;
                int.TryParse(split[6], out hp);

                // resistance and weakness
                string weakName = split[7];
                int weakVal = -1;
                int.TryParse(split[8], out hp);
                string resName = split[9];
                int resVal = -1;
                int.TryParse(split[10], out hp);

                // image, replacing the space is not strictly necessary but meh
                string img = split[11].Replace(" ", "%20").ToLower();

                var poke = new Models.Pokemon(
                    name, type, hp,
                    att1Name, att1Val,
                    att2Name, att2Val,
                    weakName, weakVal,
                    resName, resVal,
                    img);

                pokemon.Add(poke);
            }

            return pokemon.Count > 0 ?
                pokemon : null;
        }
    }
}
