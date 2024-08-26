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
            DateTime nextRun = DateTime.Today.AddHours(12).AddMinutes(04);
            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(1);
            }

            TimeSpan initialDelay = nextRun - now;
            TimeSpan dailyInterval = TimeSpan.FromDays(1);

            timer = new System.Threading.Timer(ExecuteTask, null, initialDelay, dailyInterval);
        }

        /*private static void ExecuteTask(object state)
        {
            try
            {
                FtpUploader ftpUploader = new FtpUploader();
                //ARCHIVO LOCAL DONDE TRABAJARAN
                string localFilePath = @"C:\paperua\paperu";
                //ARCHIVO DONDE SE GUARDRAN LOS ARCHIVOS TRABAJADOS
                string zipFilePath = @"C:\respaldos\ArchivoComprimido.zip";
                //CALCULAR LA HORA
                string fechaHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                //COMO SE LLAMARA EL ARCHIVO SUBIDO EN EL HOSTING 
                //CALCULAR EL NOMBRE DE LA MAQUINA Y SACAR LA HORA
                string remoteFilePath = $"{Environment.MachineName}-{fechaHora}-.zip";

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
        }*/

        private static void ExecuteTask(object state)
        {
            try
            {
                FtpUploader ftpUploader = new FtpUploader();
                // ARCHIVO LOCAL DONDE TRABAJARÁN
                string localFilePath = @"C:\paperua\paperu";
                // ARCHIVO DONDE SE GUARDARÁN LOS ARCHIVOS TRABAJADOS
                string zipFilePath = @"C:\respaldos\ArchivoComprimido.zip";
                // CALCULAR LA HORA
                string fechaHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                // OBTENER EL NOMBRE DE LA MÁQUINA
                string machineName = Environment.MachineName;
                // RUTA REMOTA CON EL NOMBRE DE LA MÁQUINA
                string remoteDirectory = $"{machineName}/";
                // NOMBRE DEL ARCHIVO SUBIDO EN EL HOSTING
                string remoteFilePath = $"{remoteDirectory}{machineName}-{fechaHora}.zip";

                // Verificar si las carpetas existen, si no, crearlas.
                if (!Directory.Exists(localFilePath))
                {
                    Directory.CreateDirectory(localFilePath);
                }

                // Crear carpeta en el servidor FTP con el nombre de la máquina si no existe
                ftpUploader.CrearDirectorioEnServidor(remoteDirectory);

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
