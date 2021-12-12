using System;
using System.Collections.Generic;
using System.IO;
using PokemonGame.Models;

namespace PokemonGame.DAL
{
    public class MockDb
    {
        // if file exists, return all lines in file as array
        // if not, return null
        private string[] ReadArr(string fName) => File.Exists(fName) ? 
            File.ReadAllLines(fName) : null;

        // if file exists, generate a list of pokemon
        // if not, return null
        public List<Pokemon> GetPokemon(string fName)
        {
            try
            {
                var lines = ReadArr(fName);
                int len = lines.Length;
                //int linesPeRPokemon = 6;

                var pokemon = new List<Pokemon>();

                for (int i = 0; i < len; i++)
                {
                    // name, type, hp
                    var nameTypeHp = lines[i++].Split(',');
                    string name = char.ToUpper(nameTypeHp[0][0]) + nameTypeHp[0][1..].ToString();
                    string type = char.ToUpper(nameTypeHp[1][0]) + nameTypeHp[1][1..].ToString();
                    int hp = -1;
                    int.TryParse(nameTypeHp[2], out hp);

                    // attack 1 name and val
                    var attack1 = lines[i++].Split(',');
                    string att1Name = char.ToUpper(attack1[0][0]) + attack1[0][1..];
                    int att1Val = -1;
                    int.TryParse(attack1[1], out att1Val);

                    // attack 2 name and val
                    var attack2 = lines[i++].Split(',');
                    string att2Name = char.ToUpper(attack2[0][0]) + attack2[0][1..];
                    int att2Val = -1;
                    int.TryParse(attack2[1], out att2Val);

                    // weakness
                    var weakNameVal = lines[i++].Split(',');
                    string weakName = char.ToUpper(weakNameVal[0][0]) + weakNameVal[0][1..];
                    int weakVal = -1;
                    int.TryParse(weakNameVal[1], out weakVal);

                    // resistance
                    var resNameVal = lines[i++].Split(',');
                    string resName = char.ToUpper(resNameVal[0][0]) + resNameVal[0][1..];
                    int resVal = -1;
                    int.TryParse(resNameVal[1], out resVal);

                    // image
                    string image = lines[i].Replace(" ", "%20")
                                           .ToLower();

                    // add to list
                    var poke = new Pokemon(
                        name, type, hp,
                        att1Name, att1Val,
                        att2Name, att2Val,
                        weakName, weakVal,
                        resName, resVal,
                        image);

                    pokemon.Add(poke);
                }

                return pokemon.Count > 0 ?
                    pokemon : null;
            }
            catch (Exception ex)
            {
                return null;
                // throw ex;
            }
        }
    }
}
