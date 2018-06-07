using Match.Football.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR.Client;
using Match.Football.Service.Interface;

namespace Match.Football.Service
{
    public class MatchService
    {
        private List<MatchPbP> _listPbPMatch;
        private HubConnection _connectionHub;

        public MatchService(IServiceHub connection)
        {
            _connectionHub = connection.ConfigurationService();
            _listPbPMatch = InicializeList();
        }
        
        private List<MatchPbP> InicializeList()
        {
            List<MatchPbP> list = new List<MatchPbP>() {
                new MatchPbP(){
                    TeamNumber = 0,
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    ActionType  = "Pre-comienzo",
                    SubType = "Entran los jugadores a la cancha."
                },
                new MatchPbP(){
                    TeamNumber = 0,
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    ActionType = "Pre-comienzo",
                    SubType  = "Intercambio de escudos."
                },
                new MatchPbP(){
                    TeamNumber = 0,
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    ActionType = "Pre-comienzo",
                    SubType  = "Se saludan los capitanes de ambas selecciones."
                },
                new MatchPbP(){
                    TeamNumber = 0,
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    ActionType = "Pre-comienzo",
                    SubType  = "Lanzamiento de moneda."
                },
                new MatchPbP(){
                    TeamNumber = 1,
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    ActionType  = "Sorteo pelota",
                    SubType = "Ganó"
                },
                new MatchPbP(){
                    TeamNumber = 2,
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    ActionType  = "Sorteo cancha.",
                    SubType = "Ganó"
                },
                new MatchPbP(){
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    ActionType  = "Comienzo",
                    SubType = "Inicio de partido."
                },
                new MatchPbP(){
                    PlayerName = "Messi",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    TeamNumber = 1,
                   ActionType  = "Pase"
                },
                new MatchPbP(){
                    PlayerName = "Di María",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   ActionType  = "Pase",
                   TeamNumber = 1
                },
                new MatchPbP(){
                    ActionType  = "Robo",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                    TeamNumber = 2,
                    PlayerName = "Miranda"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "Largo",
                   TeamNumber = 2,
                   PlayerName = "Miranda"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   TeamNumber = 2,
                   PlayerName = "Paulinho"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "Centro",
                   TeamNumber = 2,
                   PlayerName = "Neymar",
                   Area = "Área chica"
                },
                new MatchPbP(){
                   ActionType  = "Salvada",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "Atrapada",
                   Qualifiers = "En las manos",
                   TeamNumber = 1,
                   PlayerName = "Romero"
                },
                new MatchPbP(){
                   ActionType  = "Saque de meta",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   TeamNumber = 1,
                   PlayerName = "Romero"
                },
                new MatchPbP(){
                   ActionType  = "Recepción",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "de pecho",
                   TeamNumber = 1,
                   PlayerName = "Rojo"
                },

                new MatchPbP(){
                   ActionType  = "Ataque",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "avance",
                   TeamNumber = 1,
                   PlayerName = "Rojo",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "corto",
                   TeamNumber = 1,
                   PlayerName = "Rojo",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                   SubType = "corto",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   TeamNumber = 1,
                   PlayerName = "Rojo",
                   Area = "Contraria"
                },

                new MatchPbP(){
                   ActionType  = "Ataque",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "Avance",
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Ataque",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "Avance",
                   Qualifiers = "Deja atrás a defensor",
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Ataque",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "Avance",
                   Qualifiers = "Deja atrás a defensor",
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Contraria"
                },
                new MatchPbP(){
                   ActionType  = "Tiro",
                   SubType = "Media distancia",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   TeamNumber = 1,
                   PlayerName = "Di María",
                   Area = "Area grande"
                },
                new MatchPbP(){
                   ActionType  = "Tiro",
                   SubType = "Media distancia",
                   TeamNumber = 1,
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   PlayerName = "Di María",
                   Area = "Area grande",
                   Qualifiers = "Palo derecho"
                },
                new MatchPbP(){
                   ActionType  = "Despeje",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "Largo",
                   TeamNumber = 2,
                   PlayerName = "Danilo",
                   Area = "Area chica"
                },
                new MatchPbP(){
                   ActionType  = "Recepción",
                   TeamNumber = 1,
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   PlayerName = "Messi"
                },

                new MatchPbP(){
                   ActionType  = "Ataque",
                   SubType = "Avance",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   TeamNumber = 1,
                   PlayerName = "Messi",
                   Area = "Area contraria"
                },
                new MatchPbP(){
                   ActionType  = "Pase",
                   SubType = "profundo",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   TeamNumber = 1,
                   PlayerName = "Messi",
                   Area = "Area grande"
                },
                new MatchPbP(){
                   ActionType  = "Recepción",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   TeamNumber = 1,
                   PlayerName = "Higuaín",
                   Area = "Area chica"
                },

                new MatchPbP(){
                   ActionType  = "Ataque",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "Amague",
                   TeamNumber = 1,
                   PlayerName = "Higuaín",
                   Area = "Area chica"
                },
                new MatchPbP(){
                   ActionType  = "Gol",
                    TimeActual = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd hh:mm:ss"),
                   SubType = "",
                   TeamNumber = 1,
                   PlayerName = "Higuaín",
                   Area = "Area chica",
                   Qualifiers = "A la primera sin errarla (increíble)"
                }
            };
            return list;
        }

        public async Task PlayMatch()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (var item in _listPbPMatch)
            {
                item.ActionType = "action";
                item.ShotClock = watch.Elapsed.ToString("mm\\:ss\\:ff");
                try
                {
                    await _connectionHub.StartAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }


                try
                {
                    await _connectionHub.InvokeAsync("SendPlayByPlay",
                        item);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }
                await Task.Delay(100);

                var result = await _connectionHub.InvokeAsync<MatchPbP>("ReceivePlayByPlay");
                var json = JsonConvert.SerializeObject(result);
                Console.WriteLine(json);
            }

            Console.WriteLine("Partido de prueba finalizado!");
        }
    }
}
