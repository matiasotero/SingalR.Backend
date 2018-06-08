
using Arneb.WebJobs.Live.Services;
using FluentScheduler;
using System;
using Arneb.Common.Common;
using System.Linq;
using Arneb.Common;
using Arneb.Common.Services;
using System.Configuration;


namespace Arneb.WebJobs.Live
{
    
    class Program
    {
        /// <summary>
        /// Metodo Principal del servicio.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Notifications.OutputConsole("Inicio de proceso version 2.0.18!!");
            //PostGameAccessService.GetListMatchesTest().Wait();
            JobManager.Initialize(new MyRegistry());
            //LogService.Test();
            Console.Read();
        }

        /// <summary>
        /// Esta clase contiene todos las tareas que se van a ejecutar y se define en cuanto tiempo se ejucata cada una.
        /// </summary>
        public class MyRegistry : FluentScheduler.Registry
        {
            public static int count = 0;
            public MyRegistry()
            {
                //Programación de las tareas.
                Schedule(() => StartProcess()).WithName("StartProcess").NonReentrant().ToRunEvery(30).Seconds();
                count++;
            }
            #region Methods

            /// <summary>
            /// Metodo inicial, desde aca esta toda la logica de cuando los servicios inician y en que orden.
            /// </summary>
            private void StartProcess()
            {
                string message = "Todavia no hay partidos para comenzar";
                try
                {
                   var result = CurrentGameService.GetCurrentGame($"{ConfigurationManager.AppSettings["restAPI"]}/api/Match/TodayMatch").Result;

                    bool _StartStream = false;
                    
                    if (result.MatchId != null)
                    {
                        DateTime _toDay = DateTime.Now.ToUniversalTime();
                        DateTime minDate = result.DateGame.Subtract(new TimeSpan(0, 2, 0));
                        _StartStream = (_toDay >= minDate.ToUniversalTime());
                        message = "Proxima transmicion es: "+minDate.ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");

                        if (_StartStream && (result.Status == "FINISHED" || result.Status == "COMPLETE"))
                        {
                            _StartStream = false;
                            message = "El partido ya termino.";
                        }

                    }
                    
                    if (_StartStream)
                    {
                        var listJob = JobManager.RunningSchedules;
                        
                        if (!listJob.Any(x => x.Name == "StreamServices"))
                        {
                            Notifications.OutputConsole("Generando tarea StreamServices.");
                            JobManager.AddJob(()=>JobStreamServices(result.MatchId.Value, result.Status),(s) => s.WithName("StreamServices").NonReentrant().ToRunNow());
                            Notifications.OutputConsole("Stopeado StartServices.");
                            JobManager.Stop();
                        }
                        else
                        {
                           Notifications.OutputConsole("Ya el proceso esta corriendo");
                        }
                    }
                    else
                    {
                       Notifications.OutputConsole(message);
                    }
                }
                catch (Exception e)
                {
                   Notifications.OutputConsole("startProcess "+e.Message);
                }
                finally
                {
                    LogService.SaveLog().Wait();
                }

            }
#endregion

        }
       
        public static void JobStreamServices(int matchID, string status)
        {
          
            var streamingService = new StreamingService();
            try
            {
                Notifications.OutputConsole("Procesando.");
                StreamingService.ProcessGame(matchID, status);
            }
            catch (Exception e)
            {
               Notifications.OutputConsole("JobStreamServices"+e.Message);
            }
            finally
            {
               JobManager.Start();
                Notifications.OutputConsole("StartServices Iniciado.");

            }
        }
      

    }
}
