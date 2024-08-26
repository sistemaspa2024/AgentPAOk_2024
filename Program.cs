using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using VtexIconTray.Servicios;

namespace VtexIconTray
{
    static class Program
    {
        private static System.Threading.Timer timer;

        [STAThread]
        static void Main()
        {
            // Configura el temporizador
            ScheduleDailyTask();

            // Inicializa y ejecuta la aplicación Windows Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PAperuApp());
        }

        private static void ScheduleDailyTask()
        {
            DateTime now = DateTime.Now;
            // COLOCAR LA HORA DE EJECUCION 
            DateTime nextRun = DateTime.Today.AddHours(9).AddMinutes(16);
            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(1);
            }

            TimeSpan initialDelay = nextRun - now;
            TimeSpan dailyInterval = TimeSpan.FromDays(1);

            timer = new System.Threading.Timer(ExecuteTask, null, initialDelay, dailyInterval);
        }

        private static void ExecuteTask(object state)
        {
            try
            {
                FtpUploader ftpUploader = new FtpUploader();
                string localFilePath = @"C:\Respaldos\paperu";
                string zipFilePath = @"C:\Respaldos\ArchivoComprimido.zip";
                string remoteFilePath = $"{Environment.MachineName}-trabajo.zip";

                // Verificar si las carpetas existen, si no, crearlas.
                if (!Directory.Exists(localFilePath))
                {
                    Directory.CreateDirectory(localFilePath);
                }

                ftpUploader.ComprimirCarpeta(localFilePath, zipFilePath);
                ftpUploader.CargarArchivo(zipFilePath, remoteFilePath);
                ftpUploader.EliminarArchivoLocal(zipFilePath);

                Console.WriteLine("Tarea ejecutada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar la tarea: {ex.Message}");
            }
        }
    }
}
