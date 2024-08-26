using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VtexIconTray.Servicios
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net;


    public class FtpUploader
    {
        private static string ftpServidor = "ftp://ftp.sysmaticweb.com"; // SERVIDOR DE CONEXION sysmaticweb.com         
        private static string ftpUsuario = "joshuapereda@sysmaticweb.com";
        private static string ftpContraseña = "[{Hdez-SljH)";

        public void ComprimirCarpeta(string folderPath, string zipPath)
        {
            try
            {
                // Crear un archivo ZIP en la ruta especificada
                ZipFile.CreateFromDirectory(folderPath, zipPath, CompressionLevel.Fastest, true);
                Console.WriteLine("Carpeta comprimida exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al comprimir la carpeta: {ex.Message}");
            }
        }

        public void EliminarArchivoLocal(string filePath)
        {
            try
            {
                // Verificar si el archivo existe antes de intentar eliminarlo
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine("Archivo ZIP eliminado localmente.");
                }
                else
                {
                    Console.WriteLine("El archivo ZIP no existe.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el archivo ZIP: {ex.Message}");
            }
        }

        public void CargarArchivo(string filePath, string remoteFilePath)
        {
            try
            {
                // Crear la URI completa para el archivo remoto
                string remoteUri = $"{ftpServidor.TrimEnd('/')}/{remoteFilePath.TrimStart('/')}";
                Console.WriteLine($"URI Remota: {remoteUri}"); // Depuración

                var request = (FtpWebRequest)WebRequest.Create(new Uri(remoteUri));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUsuario, ftpContraseña);

                byte[] fileContents;
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    fileContents = binaryReader.ReadBytes((int)fileStream.Length);
                }

                request.ContentLength = fileContents.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
            }
            catch (UriFormatException uriEx)
            {
                Console.WriteLine($"Error en la URI: {uriEx.Message}");
            }
            catch (WebException webEx)
            {
                Console.WriteLine($"Error de red: {webEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
