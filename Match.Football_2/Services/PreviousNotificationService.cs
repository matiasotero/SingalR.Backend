using Arneb.Common;
using Arneb.Common.Common;
using Arneb.Common.Entitties;
using Arneb.Common.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arneb.Common.Entitties.NotificationPush;

namespace Arneb.WebJobs.Live.Services
{
    public class PreviousNotificationService
    {
        private static string _apiUrl;

        static PreviousNotificationService()
        {
            _apiUrl = ConfigurationManager.AppSettings["restAPI"];
        }

        public async static Task SendPreviousNotificationMatch()
        {
            try
            {
                var _todayMatch = await ApiRestServices<Calendar>.Get(String.Concat(_apiUrl, "api/v2.0/Match/TodayMatchWithCalendar"));
                var _dateMatch = new DateTime();
                string _key = "";

                try
                {
                    var _params = await ApiRestServices<Params>.Get(String.Concat(_apiUrl, "api/v2.0/Parameters/ByParamKey/", Constants.Parameters.KeyApiNotificationPush.ToString()));
                    _key = _params.Value;
                }
                catch (Exception ex)
                {
                    Notifications.OutputConsole($"Error al realizar la petición REST de la api params: {ex.Message}");
                }


                var _KeyDecrypted = NotificationService.DecryptPass(_key);
                var _keyEncrypted = NotificationService.EncryptPass(NotificationService.EncryptPass(String.Concat(NotificationService.EncryptPass("@bR@s8@skET2017"), _KeyDecrypted)));

                var _rivalTeamName = _todayMatch.VisitingTeam.Name.ToLower() != "obras" ? _todayMatch.VisitingTeam.Name : _todayMatch.LocalTeam.Name;


                if (DateTime.TryParse(_todayMatch.Date, out _dateMatch))
                {
                    if (_dateMatch.Hour == DateTime.UtcNow.Hour && (_dateMatch.Minute - 15) == DateTime.UtcNow.Minute)
                    {
                        var _message = "En minutos empieza el partido de Obras!!";

                        try
                        {
                            await ApiRestServices<NotificationPushViewModel>.Post(String.Concat(_apiUrl, "api/v2.0/NotificationPush"), new NotificationPushViewModel()
                            {
                                Key = _keyEncrypted,
                                Message = _message
                            });
                            Notifications.OutputConsole("Envio de notificacion correcta.");

                        }
                        catch (Exception ex)
                        {
                            Notifications.OutputConsole($"Ocurrió un error al intentar enviar notificación: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Notifications.OutputConsole("Ocurrió un error al intentar dar formato tipo datetime al partido del día de hoy.");
                }
            }
            catch (Exception ex)
            {
                Notifications.OutputConsole($"Ocurrió un error en el proceso de envió de notificación previa al partido: {ex.Message}");
            }
           
        }
    }
}
