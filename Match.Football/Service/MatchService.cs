using Match.Football.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Match.Football.Service
{
    public class MatchService
    {
        private static List<MatchPbP> _listPbPMatch;

        static MatchService()
        {
            _listPbPMatch = InicializeList();
        }

        private static List<MatchPbP> InicializeList()
        {
            List<MatchPbP> list = new List<MatchPbP>() {
                new MatchPbP(){
                    TeamNumber = 0,
                   ActionType  = "Pre-comienzo",
                   SubType = "Entran los jugadores a la cancha."
                },
                new MatchPbP(){
                    TeamNumber = 0,
                    ActionType = "Pre-comienzo",
                    SubType  = "Intercambio de escudos."
                },
                new MatchPbP(){
                    TeamNumber = 0,
                    ActionType = "Pre-comienzo",
                    SubType  = "Se saludan los capitanes de ambas selecciones."
                },
                new MatchPbP(){
                    TeamNumber = 0,
                    ActionType = "Pre-comienzo",
                    SubType  = "Lanzamiento de moneda."
                },
                new MatchPbP(){
                    TeamNumber = 1,
                    ActionType  = "Sorteo pelota",
                    SubType = "Ganó"
                },
                new MatchPbP(){
                    TeamNumber = 2,
                    ActionType  = "Sorteo cancha.",
                    SubType = "Ganó"
                },
                new MatchPbP(){                   
                   ActionType  = "Comienzo",
                   SubType = "Inicio de partido."
                },
                new MatchPbP(){
                    PlayerName = "Messi",
                    TeamNumber = 1,
                   ActionType  = "Pase"
                },
                new MatchPbP(){
                    PlayerName = "Di María",
                   ActionType  = "Pase",
                   TeamNumber = 1
                },
                new MatchPbP(){
                   ActionType  = "Robo",
                   TeamNumber = 2,
                   PlayerName = "Miranda"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                   SubType = "Largo",
                   TeamNumber = 2,
                   PlayerName = "Miranda"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                   TeamNumber = 2,
                   PlayerName = "Paulinho"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                   SubType = "Centro",
                   TeamNumber = 2,
                   PlayerName = "Neymar",
                   Area = "Área chica"
                },
                new MatchPbP(){
                   ActionType  = "Salvada",
                   SubType = "Atrapada",
                   Qualifiers = "En las manos",
                   TeamNumber = 1,
                   PlayerName = "Romero"
                },
                new MatchPbP(){
                   ActionType  = "Saque de meta",
                   TeamNumber = 1,
                   PlayerName = "Romero"
                },
                new MatchPbP(){
                   ActionType  = "Recepción",
                   SubType = "de pecho",
                   TeamNumber = 1,
                   PlayerName = "Rojo"
                },

                new MatchPbP(){
                   ActionType  = "Ataque",
                   SubType = "avance",
                   TeamNumber = 1,
                   PlayerName = "Rojo",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                   SubType = "corto",
                   TeamNumber = 1,
                   PlayerName = "Rojo",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                   SubType = "corto",
                   TeamNumber = 1,
                   PlayerName = "Rojo",
                   Area = "Contraria"
                },

                new MatchPbP(){
                   ActionType  = "Ataque",
                   SubType = "Avance",
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Ataque",
                   SubType = "Avance",
                   Qualifiers = "Deja atrás a defensor",
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Ataque",
                   SubType = "Avance",
                   Qualifiers = "Deja atrás a defensor",
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Tiro",
                   SubType = "Media distancia",
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Area grande"
                },
                new MatchPbP(){
                   ActionType  = "Tiro",
                   SubType = "Media distancia",
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Area grande",
                   Qualifiers = "Palo derecho"
                },
                new MatchPbP(){
                   ActionType  = "Despeje",
                   SubType = "Largo",
                   TeamNumber = 2,
                   PlayerName = "Danilo",
                   Area = "Area chica"
                },
                new MatchPbP(){
                   ActionType  = "Recepción",
                   TeamNumber = 1,
                   PlayerName = "Messi"
                },

                new MatchPbP(){
                   ActionType  = "Ataque",
                   SubType = "Avance",
                   TeamNumber = 1,
                   PlayerName = "Messi",
                   Area = "Area contraria"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                   SubType = "profundo",
                   TeamNumber = 1,
                   PlayerName = "Messi",
                   Area = "Area grande"
                },
                new MatchPbP(){
                   ActionType  = "Recepción",
                   TeamNumber = 1,
                   PlayerName = "Higuaín",
                   Area = "Area chica"
                },

                new MatchPbP(){
                   ActionType  = "Ataque",
                   SubType = "Amague",
                   TeamNumber = 1,
                   PlayerName = "Higuaín",
                   Area = "Area chica"
                },
                new MatchPbP(){
                   ActionType  = "Gol",
                   SubType = "",
                   TeamNumber = 1,
                   PlayerName = "Higuaín",
                   Area = "Area chica",
                   Qualifiers = "A la primera sin errarla (increíble)"
                }
            };
            return list;
        }

        public static async Task PlayMatch()
        {
            foreach (var item in _listPbPMatch)
            {
                var json = JsonConvert.SerializeObject(item);
                Console.WriteLine(json);
                await Task.Delay(5000);
            }
        }
    }
}
