using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arneb.Common.Common;
using Arneb.WebJobs.Live.Entitities;
using Microsoft.Azure;
using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;

namespace Arneb.WebJobs.Live.Services
{
    public class LogService
    {

        private static List<LogEntity> listLogs { get; set; }
        private static string _logId;

        static LogService()
        {
            listLogs = new List<LogEntity>();
            _logId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Recibe como parámetro el mensaje del log para
        /// guardarlo en una lista de colecciones de tipo LogEntity
        /// </summary>
        /// <param name="message"></param>
        public static void AddLog(string message)
        {
            var _idRow = Guid.NewGuid().ToString();
            var _dateLog = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            listLogs.Add(new LogEntity(_logId, _idRow) {
                MessageLog = message,
                Date = _dateLog
            });
        }

        /// <summary>
        /// Servicio que almacena en una table storage los log del webjobs.live
        /// en Azure
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task SaveLog()
        {
            try
            {
                var tableName = ConfigurationManager.AppSettings["tableStorageName"];
                // Recupero la cuenta de Storage en Azure de la cadena de conexión.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Creo la tabla cliente.
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Recibe la referencia de la tabla en Azure.
                CloudTable table = tableClient.GetTableReference(tableName);

                // Creo la tabla en caso de que no exista.
                await table.CreateIfNotExistsAsync();

                if (listLogs.Count != 0)
                {
                    // Create the batch operation.
                    TableBatchOperation batchOperation = new TableBatchOperation();

                    foreach (var item in listLogs)
                    {
                        batchOperation.Insert(item);
                    }
                    // Limpio la lista de objetos generados.
                    listLogs.Clear();

                    // Ejecuta la operación batch de operaciones
                    await table.ExecuteBatchAsync(batchOperation);
                }
            }
            catch(StorageException ex)
            {
                Notifications.OutputConsole($"Ocurrió una execepción no manejada de tipo Azure Storage: {ex}");
            }
            catch (Exception e)
            {
                Notifications.OutputConsole($"Ocurrió un error en el servicio de logs: {e.Message}");
            }
        }

        [Conditional("DEBUG")]
        /// <summary>
        /// Método exclusivo para pruebas de Table Storage
        /// </summary>
        /// <returns></returns>
        public static void Test()
        {
            AddLog("este es un mensaje");
            AddLog("este es otro mensaje");
            AddLog("este es un último mensaje");
            SaveLog().Wait();
        }
    }
}
