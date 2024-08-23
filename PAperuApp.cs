using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using FluentFTP;

using Microsoft.Office.Interop.Excel;






namespace VtexIconTray
{

    public partial class PAperuApp : Form
    {
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        public static string ConnectionString = ""; 
        public static string UsuarioToken = "";
        public static string ClaveToken = "";
        public static string MachineName { get; }

        private RestClient Client { get; set; }

        public class Conexion
        {
            public string servidor { get; set; }
            public string usuario { get; set; }
            public string clave { get; set; }
            public string baseDeDatos { get; set; }

        }

        public string conectar(Conexion con)
        {

            SqlConnection conexion = new SqlConnection("server=" + con.servidor + " ; database=" + con.baseDeDatos + " ;user=" + con.usuario + ";password=" + con.clave + "");
            try
            {
                conexion.Open();
                return "conectado";
            }
            catch (SqlException ex)
            {
                return ex.Message.ToString();
            }
        }

        public class OrderCab_DSige
        {
            public string Id_Compra { get; set; }
            public string Almacen { get; set; }
            public string Localidad { get; set; }
            public string TipoDocumento { get; set; }
            public string NumeroDoc { get; set; }
            public string NroGuiaRemision { get; set; }
            public string OCompra { get; set; }
            public string Observacion { get; set; }
            public string FechaGuia { get; set; }
            public string FechaEmision { get; set; }
            public string Moneda { get; set; }
            public string Proveedor { get; set; }
            public double TipoCambio { get; set; }
            public List<OrderDet_DSige> DetalleCompras;

        }

        public class OrderDet_DSige
        {
            public string Id_Compra { get; set; }
            public string Id_Compra_Det { get; set; }
            public string CodigoArticulo { get; set; }
            public decimal Cantidad { get; set; }
            public decimal Precio { get; set; }
            public string Estado { get; set; }

        }

        public class ResultadosCarga
        {
            public Boolean ok { get; set; }
            public string data { get; set; }
            public string message { get; set; }


        }

        public class OrderSAP
        {
            public string CardCode { get; set; }
            public string CardName { get; set; }
            public string DocEntry { get; set; }
            public string DocDate { get; set; }
            public string NumAtCard { get; set; }
            public string DocCur { get; set; }
            public decimal DocTotal { get; set; }
            public string Ref1 { get; set; }
            public string Comments { get; set; }
            public string DocNum { get; set; }
            public string LicTradNum { get; set; }

            public OrderDetailsSAP[] Lines;

        }

        public class ProveedoresCentral
        {

            public string ruc { get; set; }
            public string nombre{ get; set; }
            public string direccion { get; set; }
            public string fono { get; set; }
            public string email { get; set; }
            public string contacto { get; set; }
            public string ubigeo { get; set; }
            public string ctacte { get; set; }
            public string moneda{ get; set; }
            public string tipocompra { get; set; }
            public string contratista { get; set; }
            public string banco { get; set; }
            public string pais { get; set; }

        }


        public class OrderDetailsSAP
        {
            public string LineNum { get; set; }
            public string ItemCode { get; set; }
            public string Dscription { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
        }

        public class ArticulosDsige
        {
            public string ID_Articulo { get; set; }
            public string Codigo { get; set; }
            public string Categoria { get; set; }
            public string Linea { get; set; }
            public string SubLinea { get; set; }
            public string Descripcion { get; set; }
            public string Abreviatura { get; set; }
            public string UnidadMedida { get; set; }
            public string StockMinimo { get; set; }
            public string TiempoVida { get; set; }
            public string ExigeNroSerieKardex { get; set; }
            public string Estado { get; set; }
        }

        public class ProveedoresDsige
        {
            public string ID_Proveedor { get; set; }
            public string Codigo { get; set; }
            public string NroRUC { get; set; }
            public string Estado { get; set; }
            public string RazonSocial { get; set; }
            public string Direccion { get; set; }
            public string Telefono1 { get; set; }
            public string Contacto { get; set; }
            public string Email { get; set; }
        }

        public class EmpleadosDsige
        {
            public string ID_Proveedor { get; set; }
            public string Codigo { get; set; }
            public string NroRUC { get; set; }
            public string Estado { get; set; }
            public string RazonSocial { get; set; }
            public string Direccion { get; set; }
            public string Telefono1 { get; set; }
            public string Contacto { get; set; }
            public string Email { get; set; }
        }

        public class ResumenCCosto
        {

            public string Codigo { get; set; }
            public string NumeroCuenta { get; set; }
            public string NombreCuenta { get; set; }
            public decimal M202201 { get; set; }
            public decimal M202202 { get; set; }
            public decimal M202203 { get; set; }
            public decimal M202204 { get; set; }
            public decimal M202205 { get; set; }
            public decimal M202206 { get; set; }
            public decimal M202207 { get; set; }
            public decimal M202208 { get; set; }
            public decimal M202209 { get; set; }
            public decimal M202210 { get; set; }
            public decimal M202211 { get; set; }
            public decimal M202212 { get; set; }


        }


        public PAperuApp()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            if (Environment.MachineName == "LAPGH2101")
            {
                txtServer.Text = "LAPGH2101";
            }
            */
            

            DtHoraProceso.ShowUpDown = true;

            NotifyIconVtex.BalloonTipIcon = ToolTipIcon.Info;
            NotifyIconVtex.BalloonTipText = "PA Interface!!";
            NotifyIconVtex.BalloonTipTitle = "Bienvenido";

            ConnectionString = "Data Source=" + txtServer.Text.Trim() + ";Initial Catalog=" + txtDatabase.Text.Trim() + ";User ID=" + txtUser.Text.Trim() + ";Password=" + txtClave.Text.Trim();
            UsuarioToken = txtApiKey.Text.Trim();
            ClaveToken = txtApiToken.Text.Trim();

            NotifyIconVtex.ShowBalloonTip(2000);
            Adiciona("Inicio del Proceso " + DateTime.Now.ToString("HH:mm:ss"));
            InitializeContextMenu();

            this.WindowState = FormWindowState.Minimized;
            this.Hide();

            TimerVtex.Enabled = true;
            TimerVtex.Start();
        }

        private void InitializeContextMenu()
        {


            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();

            this.contextMenu1.MenuItems.AddRange(
                    new System.Windows.Forms.MenuItem[] { this.menuItem1, this.menuItem2, this.menuItem3, this.menuItem4 });

            // Initialize menuItem1
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "A&brir";
            this.menuItem1.Click += new System.EventHandler(this.Eventos_Click);

            this.menuItem2.Index = 1;
            this.menuItem2.Text = "C&onfiguracion";
            this.menuItem2.Click += new System.EventHandler(this.Configuration_Click);

            this.menuItem3.Index = 2;
            this.menuItem3.Text = "Ac&erca De";
            this.menuItem3.Click += new System.EventHandler(this.Acerca_Click);

            this.menuItem4.Index = 3;
            this.menuItem4.Text = "E&xit";
            this.menuItem4.Click += new System.EventHandler(this.Salir_Click);

            NotifyIconVtex.ContextMenu = this.contextMenu1;

        }

        private void Eventos_Click(object Sender, EventArgs e)
        {
            Show();
            this.grpEventos.Visible = true;
            tabVtex.SelectedIndex = 1;
        }
 
        private void Configuration_Click(object Sender, EventArgs e)
        {
            Show();
            tabVtex.SelectedIndex = 2;
        }

        private void Acerca_Click(object Sender, EventArgs e)
        {
            Show();
            tabVtex.SelectedIndex = 3;
        }

        private void Salir_Click(object Sender, EventArgs e)
        {
            this.Close();
        }

        private void NotifyIconVtex_Click(object sender, System.EventArgs e)
        {
            /*

            System.Drawing.Size windowSize =
                SystemInformation.PrimaryMonitorMaximizedWindowSize;
            System.Drawing.Point menuPoint =
                new System.Drawing.Point(windowSize.Width - 180,
                windowSize.Height - 5);
            menuPoint = this.PointToClient(menuPoint);

            NotifyIconVtex.ContextMenu.Show(this, menuPoint);
            */
        }

        public void Adiciona(string Mensaje)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = Mensaje;
            LstVtex.Items.Add(lvi);
        }


        public void FTPConecta()

        {

            //string fileName = "SAIGNaLocal.exe";

            FtpClient client = new FtpClient("ftp.paoperaciones.com", "adm_ti@paoperaciones.com", "M13ld@2022");

            string strDireccionDestino = "internas";

            string strFTP = strDireccionDestino;

            string strSubdirectorios_FTP = strFTP;

            string strDireccionLocal = "D:\\Neo\\Internas\\"; // "D:\Neolo\test\";
            string errormen = "";


            try
            {
                client.Connect();
                foreach (FtpListItem xitem in client.GetListing(strSubdirectorios_FTP, FtpListOption.Size))
                    {
                        try
                        {
                            if (xitem.Size > 0)
                            {
                            client.DownloadFile(strDireccionLocal + xitem.Name, xitem.FullName, FtpLocalExists.Overwrite);

                            client.DeleteFile(xitem.FullName);

                            }
                        }
                        catch (WebException exa2)
                        {
                            errormen = exa2.Message;
                        }

                    }
                client.Disconnect();

            }
            catch (WebException exa)
            {
                // Handle error
                errormen = exa.Message;


            }



        }
    /*----------------------------- Proceso de actualizacion de archivos ---------------------------------*/
    public void CoyFilesService (string RutaOrigen, string RutaDestino, string NombreArchivo)
        {
            // Copiado de Archivos //
            string fileName = "SAIGNaLocal.exe";
            string sourcePath = @"\\Desktop-1h3jppp\escaner";
            string targetPath = @"C:\Program Files\SAIGNa";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            try
            {
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            catch (WebException ex)
            {
                // Handle error
                string g = ex.Message;


            }

        }

        /* Ejecuta en el Timer */
        private void TimerTick(object sender, EventArgs e)
        {
            string Hora = DateTime.Now.ToString("HH:mm");
            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");


            DtFecha.Value = DateTime.Parse(Fecha) ;
            



            if (LstVtex.Items.Count >= 900000000)
            {
                LstVtex.Items.Clear();
            }


            Adiciona("tiempo transcurrido " + DateTime.Now.ToString("HH:mm:ss"));

            BtnManual.Enabled = false;
            BtnManual.Refresh();
            TimerVtex.Stop();


            string Ordenes;
            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            Adiciona("Descarga Resumen " + Fecha + " " + Hora);
            Ordenes = GetResumenCCosto();



            if (Hora == DtHoraProceso.Text)
            {
                /* Procesa los Diarios programados */
                //ProcesosDiarios();
            }

            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            Adiciona("Copiado de FTP - Inicio " + Fecha + " " + Hora);
            FTPConecta();

   
            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Adiciona("Copiado de FTP - finalizacion " + Fecha + " " + Hora);



            ///******** DESCARGA LAS ORDENES DE COMPRA ********/

            ////string Ordenes;
            //Hora = DateTime.Now.ToString("HH:mm:ss");
            //Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            //Adiciona("Descarga Ordenes - Inicio " + Fecha + " " + Hora);
            ////Ordenes = GetOrdersALL_DSIGE();
            //Hora = DateTime.Now.ToString("HH:mm:ss");
            //Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            //Adiciona("Descarga Ordenes - finalizacion " + Fecha + " " + Hora);


            ///******** CARGA LOS PROVEEDORES OK ********/
            //Hora = DateTime.Now.ToString("HH:mm:ss");
            //Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            //Adiciona("Crea Proveedores inicio " + Fecha + " " + Hora);
            ////String Proveedores;

            ////Proveedores = GetProveedoresALL_DSIGE();

            //Hora = DateTime.Now.ToString("HH:mm:ss");
            //Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            //Adiciona("Crea Proveedores - Fin " + Fecha + " " + Hora);



            ///******** CARGA LOS PRODUCTOS OK ********/
            //Hora = DateTime.Now.ToString("HH:mm:ss");
            //Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            //Adiciona("Crea Productos inicio " + Fecha + " " + Hora);
            ////String Articulos;

            ////Articulos = GetArticulosALL_DSIGE();

            //Hora = DateTime.Now.ToString("HH:mm:ss");
            //Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            //Adiciona("Crea productos - Fin " + Fecha + " " + Hora);


            /***********************************************/
            TimerVtex.Start();
            //ImgProceso.Visible = false;
            BtnManual.Enabled = true;
        }

        /*
        public void ProcesosDiarios()
        {
            // CARGA STOCKS

        string Hora = DateTime.Now.ToString("HH:mm:ss");
            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            //string misproductos = "";
            Adiciona("Actualiza Stocks inicio" + Fecha + " " + Hora);
            UpdateStock();
            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Adiciona("Actualiza Stocks finalizacion" + Fecha + " " + Hora);

            // ******** CARGA PRECIOS ******
            //string misproductos = "";
            Adiciona("Actualiza Precios - Inicio " + Fecha + " " + Hora);
            //UpdateStock();
            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Adiciona("Actualiza Precios - Fin" + Fecha + " " + Hora);
        }

        
        public void TestProduct()
        {
        var client = new RestClient("https://api.XXXXX.nl/oauth/token");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Authorization", "Basic   N2I1YTM4************************************jI0YzJhNDg=");
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("grant_type", "password");
        request.AddParameter("username", "development+XXXXXXXX-admin@XXXXXXX.XXXX");
        request.AddParameter("password", "XXXXXXXXXXXXX");

            try
            {
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                string xerror = ex.Message;

            }


        }

        public void UpdatePrices()
        {

            SqlConnection connection;
            SqlCommand command;

            string sql = null;
            SqlDataReader dataReader;

            string Hora = DateTime.Now.ToString("HH:mm:ss");
            var text = "SQL " + Hora;

            sql = "Select * from V_STOCK_ACTUAL_WEB_v1";


            connection = new SqlConnection(ConnectionString);
            connection.Open();

            command = new SqlCommand(sql, connection);
            command.CommandType = System.Data.CommandType.Text;

            string _Token;
            _Token = GetToken(UsuarioToken, ClaveToken);

            string Token = _Token.Substring(1, 32); // "bh1fcplw5141jnc172hz0l6cjm51w1gx";

            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {

                var update_this_sku = new SkuUpdate();

                var sku = new StockItem();
                sku.Qty = Convert.ToInt32(dataReader.GetValue(1));
                sku.ItemId = 1;
                sku.IsInStock = true;

                update_this_sku.StockItem = sku;
                //CreateRequest("/rest/V1/products/" + skuName + "/stockItems/" + update.StockItem.ItemId, Method.PUT, Token);
                string skuName = Convert.ToString(dataReader.GetValue(0));



                var request = CreateRequest("/rest/V1/products/" + skuName + "/stockItems/" + update_this_sku.StockItem.ItemId, Method.PUT, Token);

                //CreateRequest("/rest/V1/products", Method.POST, Token);


                Client = new RestClient("https://8bb3af77c1.nxcli.net");

                string json = JsonConvert.SerializeObject(update_this_sku, Formatting.Indented);

                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = Client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //path = @"c:\json\creaOk" + product.Sku + ".json";
                }
                else
                {
                    //path = @"c:\json\creaErr" + product.Sku + ".json";
                }
                //System.IO.File.WriteAllText(path, json);

            }
        }

        public void UpdateStock()
        {

            SqlConnection connection;
            SqlCommand command;

            string sql = null;
            SqlDataReader dataReader;

            string Hora = DateTime.Now.ToString("HH:mm:ss");
            var text = "SQL " + Hora;

            sql = "Select stock.* from V_STOCK_ACTUAL_WEB_v1 stock ";
            sql = sql + " inner join PRODUCTOS_WEB_ENVIADOS web on web.codrefSku = stock.sku  ";
            sql = sql + " where estado='E' ";



            connection = new SqlConnection(ConnectionString);
            connection.Open();

            command = new SqlCommand(sql, connection);
            command.CommandType = System.Data.CommandType.Text;

            string _Token;
            _Token = GetToken(UsuarioToken, ClaveToken);

            string Token = _Token.Substring(1, 32); 

            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {

                var update_this_sku = new SkuUpdate();

                var sku = new StockItem();
                sku.Qty = Convert.ToInt32(dataReader.GetValue(1));
                sku.ItemId = 1;  // Cambiar el Almacen
                sku.IsInStock = true;

                update_this_sku.StockItem = sku;
                string skuName = Convert.ToString(dataReader.GetValue(0));

                var request = CreateRequest("/rest/V1/products/" + skuName + "/stockItems/" + update_this_sku.StockItem.ItemId, Method.PUT, Token); 
                Client = new RestClient("https://8bb3af77c1.nxcli.net");

                string json = JsonConvert.SerializeObject(update_this_sku, Formatting.Indented);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                var response = Client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //path = @"c:\json\creaOk" + product.Sku + ".json";
                }
                else
                {
                    //path = @"c:\json\creaErr" + product.Sku + ".json";
                }
                //System.IO.File.WriteAllText(path, json);

            }
        }
        
        public void UpdateProduct()
        {

            SqlConnection connection;
            SqlCommand command;

            string sql = null;
            SqlDataReader dataReader;

            string Hora = DateTime.Now.ToString("HH:mm:ss");
            var text = "SQL " + Hora;

            //sql = "Select sku,desc1, from v_Articulos_1";

            sql = "select art.sku, art.desc1 [name], 9 attribute_set_id, art.retailprice price, 1 [status], 1 visibility, ";
            sql = sql + "'simple' [type_id], '' created_at, '' updated_at, 1 [weight] from v_articulos_v1 art ";
            sql = sql + " inner join PRODUCTOS_WEB_ENVIADOS web on web.codrefSku = art.sku  ";


            connection = new SqlConnection(ConnectionString);
            connection.Open();

            command = new SqlCommand(sql, connection);
            command.CommandType = System.Data.CommandType.Text;

            string _Token;
            _Token = GetToken(UsuarioToken, ClaveToken);
            string Token = _Token.Substring(1, 32); 

            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {

                var request = CreateRequest("/rest/V1/products/"+ Convert.ToString(dataReader.GetValue(0)), Method.PUT, Token);
                var product = new M2Product();
                product.Sku = Convert.ToString(dataReader.GetValue(0));
                product.Name = Convert.ToString(dataReader.GetValue(1));
                product.AttributeSetId = Convert.ToInt16(dataReader.GetValue(2));
                product.Price = 999;  
                // Convert.ToInt16(dataReader.GetValue(3));
                product.Status = Convert.ToInt16(dataReader.GetValue(4));
                product.Visibility = Convert.ToInt16(dataReader.GetValue(5));
                product.TypeId = Convert.ToString(dataReader.GetValue(6)); // "simple";
                product.Weight = Convert.ToDecimal(dataReader.GetValue(9));


                //product.

                Client = new RestClient("https://8bb3af77c1.nxcli.net");
                string json = JsonConvert.SerializeObject(product);

                //json = "{\"product\":" + json + ",\"saveOptions\": true}";


                json = "{\"" + "product\"" + ":" + json + ",\"saveOptions\": true}";

                string path = "";
                request.AddParameter("application/json", json,
                           ParameterType.RequestBody);

                var response = Client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    path = @"c:\json\creaOk" + product.Sku + ".json";
                }
                else
                {
                    path = @"c:\json\creaErr" + product.Sku + ".json";
                }
                System.IO.File.WriteAllText(path, json);

            }
        }

        public void ListaProducts()
        {

            SqlConnection connection;
            SqlConnection connectionActualiza;

            SqlCommand command;
            SqlCommand commandActualiza;
            string sql = null;
            SqlDataReader dataReader;

            string Hora = DateTime.Now.ToString("HH:mm:ss");
            var text = "SQL " + Hora;

            sql = "Select CodRefSku sku from PRODUCTOS_WEB_NO_ENVIADOS where estado='A'";

            connection = new SqlConnection(ConnectionString);
            connection.Open();

            command = new SqlCommand(sql, connection);
            command.CommandType = System.Data.CommandType.Text;


            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string _Sku = Convert.ToString(dataReader.GetValue(0));

            }
        }
        */

        //public string CreateProduct(string _Sku)
        //{

        //    SqlConnection connection;
        //    SqlCommand command;

        //    string sql = null;
        //    SqlDataReader dataReader;

        //    string Resultado = "";

        //    string Hora = DateTime.Now.ToString("HH:mm:ss");
        //    var text = "SQL " + Hora;

        //    sql = "Select sku,desc1, from v_Articulos_1";

        //    sql = "select sku,desc1[name],9 attribute_set_id, retailprice price, 1[status], 1 visibility, 'simple' [type_id], '' created_at, '' updated_at, 30 [weight] from v_articulos_v1 where sku = '"+_Sku+"'";

        //    connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    command = new SqlCommand(sql, connection);
        //    command.CommandType = System.Data.CommandType.Text;

        //    string _Token;
        //    _Token = GetToken(UsuarioToken, ClaveToken);

        //    string Token = _Token.Substring(1, 32); // "bh1fcplw5141jnc172hz0l6cjm51w1gx";
        //    //string Stylo = "";

        //    dataReader = command.ExecuteReader();
        //    while (dataReader.Read())
        //    {
        //        var request = CreateRequest("/rest/V1/products", Method.POST, Token);
        //        var product = new M2Product();
        //        product.Sku = Convert.ToString(dataReader.GetValue(0));
        //        product.Name = Convert.ToString(dataReader.GetValue(1));
        //        product.AttributeSetId = Convert.ToInt16(dataReader.GetValue(2));
        //        product.Price = Convert.ToInt16(dataReader.GetValue(3));
        //        product.Status = Convert.ToInt16(dataReader.GetValue(4));
        //        product.Visibility = Convert.ToInt16(dataReader.GetValue(5));
        //        product.TypeId = Convert.ToString(dataReader.GetValue(6)); // "simple";
        //        product.Weight = Convert.ToDecimal(dataReader.GetValue(9));


        //        //var MediaTmp = new M2Product().MediaGalleryEntries;

        //        DirectoryInfo di = new DirectoryInfo(@"C:\appeco\imagenes");
        //        FileInfo[] files = di.GetFiles("*"+ product.Sku+"*.JPG");
        //        //string str = "";
        //        foreach (FileInfo file in files)
        //        {
        //            //product.MediaGalleryEntries.Add("null");


        //            //--MediaTmp. )    
        //        }


        //        //var productImage = new M2Product().MediaGalleryEntries;

        //        //productImage.id = image.id;
        //        //productImage.media_type = image.media_type;
        //        //productImage.label = image.label;
        //        //productImage.disabled = image.disabled;
        //        //productImage.position = image.position;
        //        //productImage.types = image.types;
        //        //productImage.file = image.file;
        //        //productImage.content = new REST.JsonObject.Request.CatalogProductRepositoryV1SavePostBody.Content();
        //        //productImage.content.type = GetMimeType(image.file.RemoveBeforeLast("/").RemoveBeforeFirst("."));
        //        //productImage.content.name = image.file.RemoveBeforeLast("/");
        //        //product.media_gallery_entries.Add(productImage);


        //        //"media_gallery_entries":[
        //        //{
        //        //   "id":null,
        //        //   "mediaType":"image",
        //        //   "label":"Foto-0-ATLANTICSTARSCALZATURESALDIUOMOANTARESBPB63NCAMOSCIONYLONFDOSPORT-000257.010-41",
        //        //   "position":0,
        //        //   "disabled":false,
        //        //   "types":[
        //        //      "image",
        //        //      "small_image",
        //        //      "thumbnail"
        //        //   ],
        //        //   "file":null,
        //        //   "content":{
        //        //               "Base64EncodedData":"/9j/4RDKRXhpZgAATU0AKgAAAAgAEgEAAA ... ",
        //        //      "Type":"image/jpeg",
        //        //      "Name":"DSE_4063jpg"
        //        //   },
        //        //   "extensionAttributes":null
        //        //},

        //        Client = new RestClient("https://8bb3af77c1.nxcli.net");
        //        string json = JsonConvert.SerializeObject(product);

        //        json = "{\"" + "product\"" + ":" + json + "}";

        //        string path = "";
        //        request.AddParameter("application/json", json,
        //                   ParameterType.RequestBody);

        //        try
        //        {
        //            var response = Client.Execute(request);

        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                path = @"c:\json\creaOk" + product.Sku + ".json";
        //                Resultado = "OK";
        //            }
        //            else
        //            {
        //                path = @"c:\json\creaErr" + product.Sku + ".json";
        //                Resultado = "Error " + _Sku + " " + response.StatusCode + " " + response.StatusDescription;
        //            }
        //            System.IO.File.WriteAllText(path, json);

        //        }
        //        catch (FormatException e)
        //        {
        //            Resultado = "Error " + _Sku + " " +e.Message ;
        //        }

        //        return Resultado;
        //    }
        //}

        private RestRequest CreateRequest(string endPoint, Method method, string token)
        {
            var request = new RestRequest(endPoint, method);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Accept", "application/json");
            return request;
        }
        /*
        public void GeneraJSON()
        {
            var product = new Product_test
            {
                Name = "Apple",
                ExpiryDate = new DateTime(2008, 12, 28),
                Price = 3.99M,
                Sizes = new[] { "Small", "Medium", "Large" }
            };
            string json = JsonConvert.SerializeObject(product);
            string path = @"c:\json\product.json";
            System.IO.File.WriteAllText(path, json);
        }
        */
        
        private static string GetToken(string _Usuario, string _Password)
        {
            var url = $"https://8bb3af77c1.nxcli.net/rest/all/V1/integration/admin/token?username="+_Usuario+"&password="+_Password ;


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            string path = @"c:\json\TokenOk.json";
                            System.IO.File.WriteAllText(path, responseBody);

                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                string ff = ex.Message;
            }
            return "";
        }

        private static string GetProductSku(string _Sku)
        {
            var url = $"https://8bb3af77c1.nxcli.net/rest/all/V1/products/"+_Sku;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            string _Token;
            _Token = GetToken(UsuarioToken, ClaveToken);

            string Token = _Token.Substring(1, 32); // "bh1fcplw5141jnc172hz0l6cjm51w1gx";
            request.Headers.Add("Authorization", "Bearer " + Token);
            request.PreAuthenticate = true;

            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            //string json = JsonConvert.SerializeObject(product);
                            string path = @"c:\json\productOk.json";
                            System.IO.File.WriteAllText(path, responseBody);

                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                string ff = ex.Message;
            }
            return "";
        }

        private static string GetProveedoresALL_DSIGE()
        {

            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            var url = $"http://157.90.80.58:9040/PAServices/api/ProveedoresDsige?ItemCode="+Fecha;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.PreAuthenticate = false;
            request.ContentType = "application/json";
            request.Accept = "application/json";

            var url1 = $"http://209.45.50.65/production/WebApi_PA_Peru/api/Migration/saveUpdateProvider";


            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            try
                            {
                                List<ProveedoresDsige> ListaOrdenesTotal = JsonConvert.DeserializeObject<List<ProveedoresDsige>>(responseBody);

                                for (int i = 0; i <= ListaOrdenesTotal.Count()-1; i++)
                                {
                                    string ID_Proveedor = ListaOrdenesTotal[i].ID_Proveedor;
                                    string Codigo = ListaOrdenesTotal[i].Codigo;
                                    string NroRUC = ListaOrdenesTotal[i].NroRUC;
                                    string Estado = ListaOrdenesTotal[i].Estado;
                                    string RazonSocial = ListaOrdenesTotal[i].RazonSocial;
                                    string Direccion = ListaOrdenesTotal[i].Direccion;
                                    string Telefono1 = ListaOrdenesTotal[i].Telefono1;
                                    string Contacto = ListaOrdenesTotal[i].Contacto;
                                    string Email = ListaOrdenesTotal[i].Email;


                                    var Items_Load = new ProveedoresDsige
                                    {
                                        ID_Proveedor = ID_Proveedor,
                                        Codigo = Codigo,
                                        NroRUC = NroRUC,
                                        Estado = Estado,
                                        RazonSocial = RazonSocial,
                                        Direccion = Direccion,
                                        Telefono1 = Telefono1,
                                        Contacto = Contacto,
                                        Email = Email
                                    };


                                    string json = JsonConvert.SerializeObject(Items_Load);
                                    //string path = @"c:\json\ordenes.json";
                                    //System.IO.File.WriteAllText(path, json);

                                    string Data = json; //nuevo

                                    try
                                    {
                                        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url1);

                                        request1.PreAuthenticate = false;
                                        request1.Method = "POST";
                                        request1.KeepAlive = true; //nuevo
                                        request1.ContentType = "application/json";
                                        request1.Accept = "application/json";

                                        byte[] postBytes = Encoding.UTF8.GetBytes(Data); //nuevo
                                        Stream requestStream = request1.GetRequestStream(); //nuevo
                                        requestStream.Write(postBytes, 0, postBytes.Length); //nuevo
                                        requestStream.Close(); //nuevo


                                        HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                                        Stream resStream = response1.GetResponseStream();
                                        var sr = new StreamReader(response1.GetResponseStream());
                                        string responseText = sr.ReadToEnd();
                                    }


                                    catch (Exception e)
                                    {
                                        Console.Out.WriteLine(e.ToString());

                                    }

                                }

                            }


                            catch (Exception e)
                            {
                                Console.Out.WriteLine(e.ToString());

                            }


                            /*
                             * string path = @"c:\json\orderOk.json";
                            System.IO.File.WriteAllText(path, responseBody);
                            */
                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                string ff = ex.Message;
            }
            return "";
        }


        private static string GetArticulosALL_DSIGE()
        {

            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            var url = $"http://157.90.80.58:9040/PAServices/api/ArticulosDsige?ItemCode=" + Fecha;

            url = $"http://157.90.80.58:9040/PAServices/api/ArticulosDsige" ;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.PreAuthenticate = false;
            request.ContentType = "application/json";
            request.Accept = "application/json";


            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            try
                            {


                                //using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                                //{
                                List<ArticulosDsige> ListaOrdenesTotal = JsonConvert.DeserializeObject<List<ArticulosDsige>>(responseBody);

                                for (int i = 0; i <= ListaOrdenesTotal.Count()-1; i++)
                                {

                                    string ID_Articulo = ListaOrdenesTotal[i].ID_Articulo;
                                    string Codigo = ListaOrdenesTotal[i].Codigo;
                                    string Categoria = ListaOrdenesTotal[i].Categoria;
                                    string Linea = ListaOrdenesTotal[i].Linea;
                                    string SubLinea = ListaOrdenesTotal[i].SubLinea;
                                    string Descripcion = ListaOrdenesTotal[i].Descripcion;
                                    string Abreviatura = ListaOrdenesTotal[i].Abreviatura;
                                    string UnidadMedida = ListaOrdenesTotal[i].UnidadMedida;
                                    string StockMinimo = ListaOrdenesTotal[i].StockMinimo;
                                    string TiempoVida = ListaOrdenesTotal[i].TiempoVida;
                                    string ExigeNroSerieKardex = ListaOrdenesTotal[i].ExigeNroSerieKardex;
                                    string Estado = ListaOrdenesTotal[i].Estado;





                                    var Items_Load = new ArticulosDsige 
                                    {
                                        ID_Articulo = ID_Articulo,
                                        Codigo = Codigo,
                                        Categoria = Categoria,
                                        Linea = Linea,
                                        SubLinea = SubLinea,
                                        Descripcion = Descripcion,
                                        Abreviatura = Abreviatura,
                                        UnidadMedida = UnidadMedida,
                                        StockMinimo = StockMinimo,
                                        TiempoVida = TiempoVida,
                                        ExigeNroSerieKardex = ExigeNroSerieKardex,
                                        Estado = Estado

                                    };


                                    string json = JsonConvert.SerializeObject(Items_Load);
                                    //string path = @"c:\json\ordenes.json";
                                    //System.IO.File.WriteAllText(path, json);

                                    string Data = json; //nuevo

                                    try
                                    {

                                        var url1 = $"http://209.45.50.65/production/WebApi_PA_Peru/api/Migration/saveUpdateArticle";


                                        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url1);

                                        request1.PreAuthenticate = false;
                                        request1.Method = "POST";
                                        request1.KeepAlive = true; //nuevo
                                        request1.ContentType = "application/json";
                                        request1.Accept = "application/json";

                                        byte[] postBytes = Encoding.UTF8.GetBytes(Data); //nuevo
                                        Stream requestStream = request1.GetRequestStream(); //nuevo
                                        requestStream.Write(postBytes, 0, postBytes.Length); //nuevo
                                        requestStream.Close(); //nuevo


                                        HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                                        Stream resStream = response1.GetResponseStream();
                                        var sr = new StreamReader(response1.GetResponseStream());
                                        string responseText = sr.ReadToEnd();
                                    }


                                    catch (Exception e)
                                    {
                                        Console.Out.WriteLine(e.ToString());

                                    }

                                }
                               
                            }


                            catch (Exception e)
                            {
                                Console.Out.WriteLine(e.ToString());
                                
                            }


                            /*
                             * string path = @"c:\json\orderOk.json";
                            System.IO.File.WriteAllText(path, responseBody);
                            */
                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                string ff = ex.Message;
            }
            return "";
        }
        
        private static string GetProveedoresCentral()
        {
            //string _Token;
            //_Token = GetToken(UsuarioToken, ClaveToken);
            //string Fecha = ""; // DtFecha.Value.ToString;


            //DtFecha.Value.ToString("dd/MM/yyyy");
            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            var url = $"https://apirest.detroit.pe/BUX/v1/" ;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


            //string token = _Token.Substring(1, 32);

            //request.Headers.Add("Authorization", "Bearer " + token);

            // request.Headers.Add("searchCriteria[filterGroups][0][filters][0][field]", "status");
            // request.Headers.Add("searchCriteria[filterGroups][0][filters][0][value]", "pending");

            var postData = "rs=getProveedores";
            //postData += "&thing2=world";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.UserAgent = "PostmanRuntime/7.33.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            var sp = request.ServicePoint;
            var prop = sp.GetType().GetProperty("HttpBehaviour",
                                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            prop.SetValue(sp, (byte)0, null);

            request.PreAuthenticate = false;
            //request.ContentType = "application/json";
            request.Accept = "application/json";

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }


            try
            {
                var response1a = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response1a.GetResponseStream()).ReadToEnd();

                List<ProveedoresCentral> ListaProveedores= JsonConvert.DeserializeObject<List<ProveedoresCentral>>(responseString);

                for (int i = 0; i <= ListaProveedores.Count() - 1; i++)
                {
                    string Ruc = "";
                    Ruc = ListaProveedores[i].ruc;


                }



                    using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            try
                            {


                                //using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                                //{
                                List<OrderSAP> ListaOrdenesTotal = JsonConvert.DeserializeObject<List<OrderSAP>>(responseBody);

                                for (int i = 0; i <= ListaOrdenesTotal.Count() - 1; i++)
                                {





                                    string Id_Compra = ListaOrdenesTotal[i].DocNum;
                                    string Almacen = "ALMENEL"; // ListaOrdenesTotal.DocNum;
                                    string Localidad = ""; // ListaOrdenesTotal.DocNum;
                                    string TipoDocumento = "PO"; // ListaOrdenesTotal.DocNum;
                                    string NumeroDoc = ListaOrdenesTotal[i].DocNum;
                                    string OCompra = ListaOrdenesTotal[i].DocNum;
                                    string NroGuiaRemision = ListaOrdenesTotal[i].DocNum;
                                    string Observacion = ListaOrdenesTotal[i].Comments;
                                    string FechaGuia = ListaOrdenesTotal[i].DocDate;
                                    string FechaEmision = ListaOrdenesTotal[i].DocDate;
                                    string Moneda = "";
                                    string Proveedor = ListaOrdenesTotal[i].CardCode;
                                    double TipoCambio = 3.60; // ListaOrdenesTotal.Cambio ;

                                    /*        GENERA JSON         */

                                    //string json = JsonConvert.SerializeObject(product);
                                    //string path = @"c:\json\product.json";
                                    //System.IO.File.WriteAllText(path, json);

                                    /*------------------------------------------------*/

                                    if (ListaOrdenesTotal[i].DocCur == "PEN")
                                    {
                                        Moneda = "Soles";
                                    }
                                    else
                                    {
                                        Moneda = "Dolares";
                                    }

                                    List<OrderDet_DSige> Items = new List<OrderDet_DSige>();

                                    //OrderDet_DSige[] Itemsa;

                                    for (int lineas = 0; lineas <= ListaOrdenesTotal[i].Lines.Count() - 1; lineas++)
                                    {
                                        var xItem = new OrderDet_DSige
                                        {
                                            Id_Compra = Id_Compra,
                                            Id_Compra_Det = ListaOrdenesTotal[i].Lines[lineas].LineNum,
                                            CodigoArticulo = ListaOrdenesTotal[i].Lines[lineas].ItemCode,
                                            Cantidad = ListaOrdenesTotal[i].Lines[lineas].Quantity,
                                            Precio = ListaOrdenesTotal[i].Lines[lineas].Price,
                                            Estado = "A"
                                        };
                                        Items.Add(xItem);

                                    }

                                    var PO_Load = new OrderCab_DSige
                                    {
                                        Id_Compra = Id_Compra,
                                        Almacen = Almacen,
                                        Localidad = Localidad,
                                        TipoDocumento = TipoDocumento,
                                        NumeroDoc = NumeroDoc,
                                        NroGuiaRemision = NroGuiaRemision,
                                        OCompra = OCompra,
                                        Observacion = Observacion,
                                        FechaGuia = FechaGuia,
                                        FechaEmision = FechaEmision,
                                        Moneda = Moneda,
                                        Proveedor = Proveedor,
                                        TipoCambio = TipoCambio,
                                        DetalleCompras = Items
                                    };


                                    try
                                    {

                                        string json = JsonConvert.SerializeObject(PO_Load);
                                        string path = @"c:\json\o" + Id_Compra + ".json";
                                        System.IO.File.WriteAllText(path, json);

                                        string Data = json; //nuevo

                                        var url1 = $"http://209.45.50.65/production/WebApi_PA_Peru/api/Migration/saveUpdatePurchaseOrder";

                                        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url1);

                                        request1.PreAuthenticate = false;
                                        request1.Method = "POST";
                                        request1.KeepAlive = true; //nuevo
                                        request1.ContentType = "application/json";
                                        request1.Accept = "application/json";

                                        byte[] postBytes = Encoding.UTF8.GetBytes(Data); //nuevo
                                        Stream requestStream = request1.GetRequestStream(); //nuevo
                                        requestStream.Write(postBytes, 0, postBytes.Length); //nuevo
                                        requestStream.Close(); //nuevo


                                        HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                                        Stream resStream = response1.GetResponseStream();
                                        var sr = new StreamReader(response1.GetResponseStream());
                                        string responseText = sr.ReadToEnd();


                                        //string resultado = JsonConvert.SerializeObject(PO_Load);
                                        ResultadosCarga ResultadoProceso = JsonConvert.DeserializeObject<ResultadosCarga>(responseText);

                                        /*
                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\json\order1.txt", true))
                                        {
                                            file.WriteLine("orden" + Id_Compra);
                                            file.WriteLine("|");
                                            file.WriteLine(ResultadoProceso.ok );
                                            file.WriteLine("|");
                                            file.WriteLine(ResultadoProceso.data);
                                            file.WriteLine("|");
                                            file.WriteLine(ResultadoProceso.message);

                                        }
                                        */
                                    }


                                    catch (Exception e)
                                    {
                                        Console.Out.WriteLine(e.ToString());

                                    }


                                }






                                /* llamar al WS POS */
                                /*
                                 http://209.45.50.65/production/WebApi_PA_Peru /api/Migration/saveUpdatePurchaseOrder
                                    {
                                        "Id_Compra": "1",
                                        "Almacen": "Almacén A",
                                        "Localidad": "Ciudad A",
                                        "TipoDocumento": "Factura",
                                        "NumeroDoc": "F001",
                                        "NroGuiaRemision": "G001",
                                        "OCompra": "OC001",
                                        "Observacion": "Observación 1",
                                        "FechaGuia": "2022-01-01",
                                        "FechaEmision": "2022-01-02",
                                        "Moneda": "Soles",
                                        "Proveedor": "Proveedor 1",
                                        "TipoCambio": "3.5",
                                        "DetalleCompras": [
                                            {
                                            "Id_Compra": "1",
                                            "Id_Compra_Det": "1",
                                            "CodigoArticulo": "CA001",
                                            "Cantidad": "10",
                                            "Precio": "100.00",
                                            "Estado": "Activo"
                                            },
                                            {
                                            "Id_Compra": "1",
                                            "Id_Compra_Det": "2",
                                            "CodigoArticulo": "CA002",
                                            "Cantidad": "20",
                                            "Precio": "50.00",
                                            "Estado": "Activo"
                                            }
                                        ]
                                    }


                                 */


                            }


                            catch (Exception e)
                            {
                                Console.Out.WriteLine(e.ToString());
                            }


                            /*
                             * string path = @"c:\json\orderOk.json";
                            System.IO.File.WriteAllText(path, responseBody);
                            */
                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                string ff = ex.Message;
            }
            return "";
        }

        

        private static string GetResumenCCosto()
        {
            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            //var url = $"http://157.90.80.58:9040/PAServices/api/Orders?CardCode=" + Fecha;

            var url = $"http://157.90.80.58:9040/PAServices/api/CostosResumen?Agrupa='940103'&Periodo=2024-01";




            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //string token = _Token.Substring(1, 32);

            //request.Headers.Add("Authorization", "Bearer " + token);

            // request.Headers.Add("searchCriteria[filterGroups][0][filters][0][field]", "status");
            // request.Headers.Add("searchCriteria[filterGroups][0][filters][0][value]", "pending");

            request.PreAuthenticate = false;
            request.ContentType = "application/json";
            request.Accept = "application/json";


            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            try
                            {


                                //using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                                //{
                                List<ResumenCCosto> ListaOrdenesTotal = JsonConvert.DeserializeObject<List<ResumenCCosto>>(responseBody);



                                
                                var cta70 = ListaOrdenesTotal.Where(ResumenCCosto => ResumenCCosto.Codigo == "70");
                                /*

                                foreach (var n in smallNumbers)
                                    Console.WriteLine(n);

                                */

                                int Fila = 4;
                                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                                excelApp.Visible = true;
                                excelApp.Workbooks.Add();

                                //Microsoft.Office.Interop.Excel.Worksheets Hojas;



 
                                Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.ActiveSheet;
                                workSheet.Name = "Resumen";

                                excelApp.Worksheets.Add();

                                Microsoft.Office.Interop.Excel.Worksheet workSheet1 = excelApp.ActiveSheet;
                                workSheet1.Name = "Detalle";


                                workSheet.Cells[Fila, "B"] = "Rubro";
                                workSheet.Cells[Fila, "C"] = "Ene";
                                workSheet.Cells[Fila, "D"] = "Feb";
                                workSheet.Cells[Fila, "E"] = "Mar";
                                workSheet.Cells[Fila, "F"] = "Abr";
                                workSheet.Cells[Fila, "G"] = "May";
                                workSheet.Cells[Fila, "H"] = "Jun";
                                workSheet.Cells[Fila, "I"] = "Jul";
                                workSheet.Cells[Fila, "J"] = "Ago";
                                workSheet.Cells[Fila, "K"] = "Set";
                                workSheet.Cells[Fila, "M"] = "Nov";
                                workSheet.Cells[Fila, "N"] = "Dic";
                                workSheet.Cells[Fila, "P"] = "Variación %";
                                workSheet.Cells[Fila, "Q"] = "Variación S/";

                                string Quiebre = "";

                                Fila = Fila + 1;
                                for (int i = 0; i <= ListaOrdenesTotal.Count() - 1; i++)
                                {

                                    //ListaOrdenesTotal.Where(ListaOrdenesTotal.Codigo="70")

                                    string Codigo = ListaOrdenesTotal[i].Codigo;
                                    string NumeroCuenta = ListaOrdenesTotal[i].NumeroCuenta;
                                    string NombreCuenta = ListaOrdenesTotal[i].NombreCuenta;
                                    decimal M202201 = ListaOrdenesTotal[i].M202201;
                                    decimal M202202 = ListaOrdenesTotal[i].M202202;
                                    decimal M202203 = ListaOrdenesTotal[i].M202203;
                                    decimal M202204 = ListaOrdenesTotal[i].M202204;
                                    decimal M202205 = ListaOrdenesTotal[i].M202205;
                                    decimal M202206 = ListaOrdenesTotal[i].M202206;
                                    decimal M202207 = ListaOrdenesTotal[i].M202207;
                                    decimal M202208 = ListaOrdenesTotal[i].M202208;
                                    decimal M202209 = ListaOrdenesTotal[i].M202209;
                                    decimal M202210 = ListaOrdenesTotal[i].M202210;
                                    decimal M202211 = ListaOrdenesTotal[i].M202211;
                                    decimal M202212 = ListaOrdenesTotal[i].M202212;

                                    if (Quiebre == "")
                                    {
                                        Quiebre = "01";
                                        Fila = Fila + 1;

                                        workSheet.Cells[Fila, "B"] = "01 VENTAS";
                                        /*
                                        workSheet.Cells[Fila, "C"] = "Ene";
                                        workSheet.Cells[Fila, "D"] = "Feb";
                                        workSheet.Cells[Fila, "E"] = "Mar";
                                        workSheet.Cells[Fila, "F"] = "Abr";
                                        workSheet.Cells[Fila, "G"] = "May";
                                        workSheet.Cells[Fila, "H"] = "Jun";
                                        workSheet.Cells[Fila, "I"] = "Jul";
                                        workSheet.Cells[Fila, "J"] = "Ago";
                                        workSheet.Cells[Fila, "K"] = "Set";
                                        workSheet.Cells[Fila, "M"] = "Nov";
                                        workSheet.Cells[Fila, "N"] = "Dic";
                                        workSheet.Cells[Fila, "P"] = "Variación %";
                                        workSheet.Cells[Fila, "Q"] = "Variación S/";
                                        */

                                    }

                                    Fila = Fila + 1;

                                    workSheet.Cells[Fila, "B"] = "01 VENTAS";
                                    workSheet.Cells[Fila, "C"] = ListaOrdenesTotal[i].NombreCuenta;
                                    workSheet.Cells[Fila, "D"] = ListaOrdenesTotal[i].M202201;
                                    workSheet.Cells[Fila, "E"] = ListaOrdenesTotal[i].M202202;
                                    workSheet.Cells[Fila, "F"] = ListaOrdenesTotal[i].M202203;
                                    workSheet.Cells[Fila, "G"] = ListaOrdenesTotal[i].M202204;
                                    workSheet.Cells[Fila, "H"] = ListaOrdenesTotal[i].M202205;
                                    workSheet.Cells[Fila, "I"] = ListaOrdenesTotal[i].M202206;
                                    workSheet.Cells[Fila, "J"] = ListaOrdenesTotal[i].M202207;
                                    workSheet.Cells[Fila, "K"] = ListaOrdenesTotal[i].M202208;
                                    workSheet.Cells[Fila, "L"] = ListaOrdenesTotal[i].M202209;
                                    workSheet.Cells[Fila, "M"] = ListaOrdenesTotal[i].M202210;
                                    workSheet.Cells[Fila, "N"] = ListaOrdenesTotal[i].M202211;

                                    //worksheet.Cells["B12"].Formula = "=COUNT(B1:B11)";
                                    workSheet.Cells[Fila, "P"].Formula = "=SUMA(D"+Fila+":D"+Fila+")";

                                    Microsoft.Office.Interop.Excel.Range xlRange = workSheet.UsedRange;

                                    workSheet.Cells[Fila, "Q"] = ListaOrdenesTotal[i].M202212;

                                    workSheet.Range["D"+Fila+":D"+Fila].Font.Size = 20;
                                    workSheet.Range["D" + Fila + ":O" + Fila].NumberFormat = "0.00";
                                    workSheet.Range["D" + Fila + ":O" + Fila].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


                                    //workSheet("Hoja1").Range("A1:Q1").Columns.AutoFit();

                                    workSheet.Columns[1].AutoFit();
                                    workSheet.Columns[2].AutoFit();
                                    workSheet.Columns[3].AutoFit();
                                    workSheet.Columns[4].AutoFit();


                                    /*
                                    excelApp.Workbooks.Add();

                                    Microsoft.Office.Interop.Excel._Worksheet workSheetDetalle = excelApp.ActiveSheet;

                                    workSheetDetalle.Name = "Detalle";
                                    */

                                    // workbook.CalculateFormula();


                                    /*
                                    workSheet.Cells[1, "A"] = "Listado de Empleados";
                                    workSheet.Range["A1", "C1"].Merge();
                                    workSheet.Range["A1", "C1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                    workSheet.Range["A1", "C1"].Font.Bold = true;
                                    workSheet.Range["A1", "C1"].Font.Size = 20;
                                var row = 3;
                                foreach (var acct in accounts)
                                {
                                    row++;
                                    workSheet.Cells[row, "A"] = acct.ID;
                                    workSheet.Cells[row, "B"] = acct.Nombre;
                                    workSheet.Cells[row, "C"] = acct.Salario;
                                }
                                workSheet.Columns[1].AutoFit();
                                workSheet.Columns[2].AutoFit();
                                workSheet.Columns[3].AutoFit();
                                    */
                                    workSheet.Range["A3", "C5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);




                                /*        GENERA JSON         */

                                //string json = JsonConvert.SerializeObject(product);
                                //string path = @"c:\json\product.json";
                                //System.IO.File.WriteAllText(path, json);

                                /*------------------------------------------------*/


                                /*

                                try
                                {

                                    string json = JsonConvert.SerializeObject(PO_Load);
                                    string path = @"c:\json\o" + Id_Compra + ".json";
                                    System.IO.File.WriteAllText(path, json);

                                    string Data = json; //nuevo

                                    var url1 = $"http://209.45.50.65/production/WebApi_PA_Peru/api/Migration/saveUpdatePurchaseOrder";

                                    HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url1);

                                    request1.PreAuthenticate = false;
                                    request1.Method = "POST";
                                    request1.KeepAlive = true; //nuevo
                                    request1.ContentType = "application/json";
                                    request1.Accept = "application/json";

                                    byte[] postBytes = Encoding.UTF8.GetBytes(Data); //nuevo
                                    Stream requestStream = request1.GetRequestStream(); //nuevo
                                    requestStream.Write(postBytes, 0, postBytes.Length); //nuevo
                                    requestStream.Close(); //nuevo


                                    HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                                    Stream resStream = response1.GetResponseStream();
                                    var sr = new StreamReader(response1.GetResponseStream());
                                    string responseText = sr.ReadToEnd();


                                    //string resultado = JsonConvert.SerializeObject(PO_Load);
                                    ResultadosCarga ResultadoProceso = JsonConvert.DeserializeObject<ResultadosCarga>(responseText);


                                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\json\order1.txt", true))
                                    //{
                                    //    file.WriteLine("orden" + Id_Compra);
                                    //    file.WriteLine("|");
                                    //    file.WriteLine(ResultadoProceso.ok );
                                    //    file.WriteLine("|");
                                    //    file.WriteLine(ResultadoProceso.data);
                                    //    file.WriteLine("|");
                                    //    file.WriteLine(ResultadoProceso.message);

                                    //}

                                }


                                catch (Exception e)
                                {
                                    Console.Out.WriteLine(e.ToString());

                                }
                                */



                            }






                                /* llamar al WS POS */
                                /*
                                 http://209.45.50.65/production/WebApi_PA_Peru /api/Migration/saveUpdatePurchaseOrder
                                    {
                                        "Id_Compra": "1",
                                        "Almacen": "Almacén A",
                                        "Localidad": "Ciudad A",
                                        "TipoDocumento": "Factura",
                                        "NumeroDoc": "F001",
                                        "NroGuiaRemision": "G001",
                                        "OCompra": "OC001",
                                        "Observacion": "Observación 1",
                                        "FechaGuia": "2022-01-01",
                                        "FechaEmision": "2022-01-02",
                                        "Moneda": "Soles",
                                        "Proveedor": "Proveedor 1",
                                        "TipoCambio": "3.5",
                                        "DetalleCompras": [
                                            {
                                            "Id_Compra": "1",
                                            "Id_Compra_Det": "1",
                                            "CodigoArticulo": "CA001",
                                            "Cantidad": "10",
                                            "Precio": "100.00",
                                            "Estado": "Activo"
                                            },
                                            {
                                            "Id_Compra": "1",
                                            "Id_Compra_Det": "2",
                                            "CodigoArticulo": "CA002",
                                            "Cantidad": "20",
                                            "Precio": "50.00",
                                            "Estado": "Activo"
                                            }
                                        ]
                                    }


                                 */


                            }


                            catch (Exception e)
                            {
                                Console.Out.WriteLine(e.ToString());
                            }


                            /*
                             * string path = @"c:\json\orderOk.json";
                            System.IO.File.WriteAllText(path, responseBody);
                            */
                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                string ff = ex.Message;
            }
            return "";
        }



        private static string GetOrdersALL_DSIGE()
        {
            //string _Token;
            //_Token = GetToken(UsuarioToken, ClaveToken);
            //string Fecha = ""; // DtFecha.Value.ToString;


            //DtFecha.Value.ToString("dd/MM/yyyy");
            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            var url = $"http://157.90.80.58:9040/PAServices/api/Orders?CardCode="+Fecha;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //string token = _Token.Substring(1, 32);

            //request.Headers.Add("Authorization", "Bearer " + token);

            // request.Headers.Add("searchCriteria[filterGroups][0][filters][0][field]", "status");
            // request.Headers.Add("searchCriteria[filterGroups][0][filters][0][value]", "pending");

            request.PreAuthenticate = false;
            request.ContentType = "application/json";
            request.Accept = "application/json";


            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            try
                            {


                                //using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                                //{
                                    List<OrderSAP> ListaOrdenesTotal = JsonConvert.DeserializeObject<List<OrderSAP>>(responseBody);

                                    for (int i = 0; i <= ListaOrdenesTotal.Count()-1; i++)
                                    {





                                        string Id_Compra = ListaOrdenesTotal[i].DocNum;
                                        string Almacen = "ALMENEL"; // ListaOrdenesTotal.DocNum;
                                        string Localidad = ""; // ListaOrdenesTotal.DocNum;
                                        string TipoDocumento = "PO"; // ListaOrdenesTotal.DocNum;
                                        string NumeroDoc = ListaOrdenesTotal[i].DocNum;
                                        string OCompra = ListaOrdenesTotal[i].DocNum;
                                        string NroGuiaRemision = ListaOrdenesTotal[i].DocNum;
                                        string Observacion = ListaOrdenesTotal[i].Comments;
                                        string FechaGuia = ListaOrdenesTotal[i].DocDate;
                                        string FechaEmision = ListaOrdenesTotal[i].DocDate;
                                        string Moneda = "";
                                        string Proveedor = ListaOrdenesTotal[i].CardCode;
                                        double TipoCambio = 3.60; // ListaOrdenesTotal.Cambio ;

                                        /*        GENERA JSON         */

                                        //string json = JsonConvert.SerializeObject(product);
                                        //string path = @"c:\json\product.json";
                                        //System.IO.File.WriteAllText(path, json);

                                        /*------------------------------------------------*/

                                        if (ListaOrdenesTotal[i].DocCur == "PEN")
                                        {
                                            Moneda = "Soles";
                                        }
                                        else
                                        {
                                            Moneda = "Dolares";
                                        }

                                        List<OrderDet_DSige> Items = new List<OrderDet_DSige>();

                                        //OrderDet_DSige[] Itemsa;

                                        for (int lineas = 0; lineas <= ListaOrdenesTotal[i].Lines.Count() - 1; lineas++)
                                        {
                                            var xItem = new OrderDet_DSige
                                            {
                                                Id_Compra = Id_Compra,
                                                Id_Compra_Det = ListaOrdenesTotal[i].Lines[lineas].LineNum,
                                                CodigoArticulo = ListaOrdenesTotal[i].Lines[lineas].ItemCode,
                                                Cantidad = ListaOrdenesTotal[i].Lines[lineas].Quantity,
                                                Precio = ListaOrdenesTotal[i].Lines[lineas].Price,
                                                Estado = "A"
                                            };
                                            Items.Add(xItem);

                                        }

                                        var PO_Load = new OrderCab_DSige
                                        {
                                            Id_Compra = Id_Compra,
                                            Almacen = Almacen,
                                            Localidad = Localidad,
                                            TipoDocumento = TipoDocumento,
                                            NumeroDoc = NumeroDoc,
                                            NroGuiaRemision = NroGuiaRemision,
                                            OCompra = OCompra,
                                            Observacion = Observacion,
                                            FechaGuia = FechaGuia,
                                            FechaEmision = FechaEmision,
                                            Moneda = Moneda,
                                            Proveedor = Proveedor,
                                            TipoCambio = TipoCambio,
                                            DetalleCompras = Items
                                        };


                                    try
                                    {

                                        string json = JsonConvert.SerializeObject(PO_Load);
                                        string path = @"c:\json\o"+ Id_Compra + ".json";
                                        System.IO.File.WriteAllText(path, json);

                                        string Data = json; //nuevo

                                        var url1 = $"http://209.45.50.65/production/WebApi_PA_Peru/api/Migration/saveUpdatePurchaseOrder";

                                        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url1);

                                        request1.PreAuthenticate = false;
                                        request1.Method = "POST";
                                        request1.KeepAlive = true; //nuevo
                                        request1.ContentType = "application/json";
                                        request1.Accept = "application/json";

                                        byte[] postBytes = Encoding.UTF8.GetBytes(Data); //nuevo
                                        Stream requestStream = request1.GetRequestStream(); //nuevo
                                        requestStream.Write(postBytes, 0, postBytes.Length); //nuevo
                                        requestStream.Close(); //nuevo


                                        HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                                        Stream resStream = response1.GetResponseStream();
                                        var sr = new StreamReader(response1.GetResponseStream());
                                        string responseText = sr.ReadToEnd();


                                        //string resultado = JsonConvert.SerializeObject(PO_Load);
                                        ResultadosCarga ResultadoProceso = JsonConvert.DeserializeObject<ResultadosCarga>(responseText);

                                        /*
                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\json\order1.txt", true))
                                        {
                                            file.WriteLine("orden" + Id_Compra);
                                            file.WriteLine("|");
                                            file.WriteLine(ResultadoProceso.ok );
                                            file.WriteLine("|");
                                            file.WriteLine(ResultadoProceso.data);
                                            file.WriteLine("|");
                                            file.WriteLine(ResultadoProceso.message);

                                        }
                                        */
                                    }


                                    catch (Exception e)
                                    {
                                        Console.Out.WriteLine(e.ToString());

                                    }


                                }

                               



                                    
                                    /* llamar al WS POS */
                                    /*
                                     http://209.45.50.65/production/WebApi_PA_Peru /api/Migration/saveUpdatePurchaseOrder
                                        {
                                            "Id_Compra": "1",
                                            "Almacen": "Almacén A",
                                            "Localidad": "Ciudad A",
                                            "TipoDocumento": "Factura",
                                            "NumeroDoc": "F001",
                                            "NroGuiaRemision": "G001",
                                            "OCompra": "OC001",
                                            "Observacion": "Observación 1",
                                            "FechaGuia": "2022-01-01",
                                            "FechaEmision": "2022-01-02",
                                            "Moneda": "Soles",
                                            "Proveedor": "Proveedor 1",
                                            "TipoCambio": "3.5",
                                            "DetalleCompras": [
                                                {
                                                "Id_Compra": "1",
                                                "Id_Compra_Det": "1",
                                                "CodigoArticulo": "CA001",
                                                "Cantidad": "10",
                                                "Precio": "100.00",
                                                "Estado": "Activo"
                                                },
                                                {
                                                "Id_Compra": "1",
                                                "Id_Compra_Det": "2",
                                                "CodigoArticulo": "CA002",
                                                "Cantidad": "20",
                                                "Precio": "50.00",
                                                "Estado": "Activo"
                                                }
                                            ]
                                        }
 
                                     
                                     */


                            }


                            catch (Exception e)
                            {
                                Console.Out.WriteLine(e.ToString());
                            }


                            /*
                             * string path = @"c:\json\orderOk.json";
                            System.IO.File.WriteAllText(path, responseBody);
                            */
                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                string ff = ex.Message;
            }
            return "";
        }




        private void BtnActualiza_Click(object sender, EventArgs e)
        {

        }

        private void BtnTestConection_Click(object sender, EventArgs e)
        {

        }

        private void LstVtex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool conectar()
        {
            bool result = false;

            return result;
        }

        private void NotifyIconVtex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            InitializeContextMenu();
            Show();
           
            this.WindowState = FormWindowState.Normal;
           // MessageBox.Show("dsvds");
        }

        private void VtexApp_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                NotifyIconVtex.Visible = true;
            }
        }

        private void LstVtex_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {


            Conexion con = new Conexion();
            if (txtServer.Text.Trim() != "" && txtUser.Text.Trim() != "" && txtClave.Text.Trim() != "" && txtDatabase.Text.Trim() != "")

            {
                try
                {
                    con.baseDeDatos = txtDatabase.Text.Trim();
                    con.usuario = txtUser.Text.Trim();
                    con.clave = txtClave.Text.Trim();
                    con.servidor = txtServer.Text.Trim();

                    SqlConnection conexion = new SqlConnection("server=" + con.servidor + " ; database=" + con.baseDeDatos + " ;user=" + con.usuario + ";password=" + con.clave + "");

                    conexion.Open();
                    conexion.Close();
                    MessageBox.Show("Conexion Correcta, revise!!", "Test de Datos");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error de conexion, revise!!" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos");
            }

        }
        /*
        public class StockItem
        {

            [JsonProperty("item_id", NullValueHandling = NullValueHandling.Ignore)]
            public int? ItemId { get; set; }

            [JsonProperty("product_id", NullValueHandling = NullValueHandling.Ignore)]
            public int? ProductId { get; set; }

            [JsonProperty("stock_id", NullValueHandling = NullValueHandling.Ignore)]
            public int? StockId { get; set; }

            [JsonProperty("qty", NullValueHandling = NullValueHandling.Ignore)]
            public int Qty { get; set; }

            [JsonProperty("is_in_stock", NullValueHandling = NullValueHandling.Ignore)]
            public bool IsInStock { get; set; }

            [JsonProperty("is_qty_decimal", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsQtyDecimal { get; set; }

            [JsonProperty("show_default_notification_message", NullValueHandling = NullValueHandling.Ignore)]
            public bool? ShowDefaultNotificationMessage { get; set; }

            [JsonProperty("use_config_min_qty", NullValueHandling = NullValueHandling.Ignore)]
            public bool? UseConfigMinQty { get; set; }

            [JsonProperty("min_qty", NullValueHandling = NullValueHandling.Ignore)]
            public int? MinQty { get; set; }

            [JsonProperty("use_config_min_sale_qty", NullValueHandling = NullValueHandling.Ignore)]
            public int? UseConfigMinSaleQty { get; set; }

            [JsonProperty("min_sale_qty", NullValueHandling = NullValueHandling.Ignore)]
            public int? MinSaleQty { get; set; }

            [JsonProperty("use_config_max_sale_qty", NullValueHandling = NullValueHandling.Ignore)]
            public bool? UseConfigMaxSaleQty { get; set; }

            [JsonProperty("max_sale_qty", NullValueHandling = NullValueHandling.Ignore)]
            public int? MaxSaleQty { get; set; }

            [JsonProperty("use_config_backorders", NullValueHandling = NullValueHandling.Ignore)]
            public bool? UseConfigBackorders { get; set; }

            [JsonProperty("backorders", NullValueHandling = NullValueHandling.Ignore)]
            public int? Backorders { get; set; }

            [JsonProperty("use_config_notify_stock_qty", NullValueHandling = NullValueHandling.Ignore)]
            public bool? UseConfigNotifyStockQty { get; set; }

            [JsonProperty("notify_stock_qty", NullValueHandling = NullValueHandling.Ignore)]
            public int? NotifyStockQty { get; set; }

            [JsonProperty("use_config_qty_increments", NullValueHandling = NullValueHandling.Ignore)]
            public bool? UseConfigQtyIncrements { get; set; }

            [JsonProperty("qty_increments", NullValueHandling = NullValueHandling.Ignore)]
            public int? QtyIncrements { get; set; }

            [JsonProperty("use_config_enable_qty_inc", NullValueHandling = NullValueHandling.Ignore)]
            public bool? UseConfigEnableQtyInc { get; set; }

            [JsonProperty("enable_qty_increments", NullValueHandling = NullValueHandling.Ignore)]
            public bool? EnableQtyIncrements { get; set; }

            [JsonProperty("use_config_manage_stock", NullValueHandling = NullValueHandling.Ignore)]
            public bool? UseConfigManageStock { get; set; }

            [JsonProperty("manage_stock", NullValueHandling = NullValueHandling.Ignore)]
            public bool? ManageStock { get; set; }

            [JsonProperty("low_stock_date", NullValueHandling = NullValueHandling.Ignore)]
            public object LowStockDate { get; set; }

            [JsonProperty("is_decimal_divided", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsDecimalDivided { get; set; }

            [JsonProperty("stock_status_changed_auto", NullValueHandling = NullValueHandling.Ignore)]
            public int? StockStatusChangedAuto { get; set; }
        }

        public class ExtensionAttributesa
        {

            [JsonProperty("stock_item")]
            public StockItem StockItem { get; set; }
        }

        public class CustomAttribute
        {

            [JsonProperty("attribute_code")]
            public string AttributeCode { get; set; }

            [JsonProperty("value")]
            public object Value { get; set; }
        }

        public class M2Product
        {

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("sku")]
            public string Sku { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("attribute_set_id")]
            public int AttributeSetId { get; set; }

            [JsonProperty("price")]
            public int Price { get; set; }

            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("visibility")]
            public int Visibility { get; set; }

            [JsonProperty("type_id")]
            public string TypeId { get; set; }

            [JsonProperty("created_at")]
            public string CreatedAt { get; set; }

            [JsonProperty("updated_at")]
            public string UpdatedAt { get; set; }

            [JsonProperty("weight")]
            public decimal Weight { get; set; }



        public class MediaGallery
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("media_type")]
            public object Media_Type { get; set; }
        }

        public class SkuUpdate
        {
            [JsonProperty("stockItem")]
            public StockItem StockItem { get; set; }
        }

        public class Category
        {

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("parent_id")]
            public int ParentId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("is_active")]
            public bool IsActive { get; set; }

            [JsonProperty("position")]
            public int Position { get; set; }

            [JsonProperty("level")]
            public int Level { get; set; }

            [JsonProperty("children")]
            public string Children { get; set; }

            [JsonProperty("created_at")]
            public string CreatedAt { get; set; }

            [JsonProperty("updated_at")]
            public string UpdatedAt { get; set; }

            [JsonProperty("path")]
            public string Path { get; set; }

            [JsonProperty("available_sort_by")]
            public IList<string> AvailableSortBy { get; set; }

            [JsonProperty("include_in_menu")]
            public bool IncludeInMenu { get; set; }

        }

        public class ProductCategory
        {
            [JsonProperty("category")]
            public Category Category { get; set; }
        }
        */

        public static string ConexionMysql()
        {
            string servidor = "192.185.52.160"; //Nombre o ip del servidor de MySQL
            string bd = "srifai_appeco"; //Nombre de la base de datos
            string usuario = "srifai_appeco"; //Usuario de acceso a MySQL
            string password = "Delfin=2902"; //Contraseña de usuario de acceso a MySQL
            string datos = null; //Variable para almacenar el resultado
            //Crearemos la cadena de conexión concatenando las variables
            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";
            //Instancia para conexión a MySQL, recibe la cadena de conexión
            MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
            MySqlDataReader reader = null; //Variable para leer el resultado de la consulta
            //Agregamos try-catch para capturar posibles errores de conexión o sintaxis.
            try
            {
                string consulta = "Select * From CONFIG_API"; //Consulta a MySQL (Muestra las bases de datos que tiene el servidor)
                MySqlCommand comando = new MySqlCommand(consulta); //Declaración SQL para ejecutar contra una base de datos MySQL
                comando.Connection = conexionBD; //Establece la MySqlConnection utilizada por esta instancia de MySqlCommand
                conexionBD.Open(); //Abre la conexión
                reader = comando.ExecuteReader(); //Ejecuta la consulta y crea un MySqlDataReader
                while (reader.Read()) //Avanza MySqlDataReader al siguiente registro
                {
                    datos += reader.GetString(0) + "\n"; //Almacena cada registro con un salto de linea
                }
                MessageBox.Show(datos); //Imprime en cuadro de dialogo el resultado
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message); //Si existe un error aquí muestra el mensaje
            }
            finally
            {
                conexionBD.Close(); //Cierra la conexión a MySQL
            }
            return "Ok";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ConexionMysql();
        }



        private void BtnManual_Click(object sender, EventArgs e)
        {
            // Ejecuta Manual
            string proceso = "";
            string Hora = DateTime.Now.ToString("HH:mm:ss");
            string Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            TimerVtex.Stop();
            BtnManual.Enabled = false;

            string Ordenes1;
            Ordenes1 = "";


            Adiciona("Descarga Proveedores - Inicio " + Fecha + " " + Hora);
            //Ordenes1 = GetProveedoresCentral();




            proceso = "Migracion manual ";
            Adiciona( proceso + Fecha + " " + Hora);

            Adiciona("Descarga Ordenes - inicio " + Fecha + " " + Hora);

            string Ordenes;
            Ordenes = "";

            Adiciona("Descarga Ordenes - Inicio " + Fecha + " " + Hora);
            //Ordenes = GetOrdersALL_DSIGE();
            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Adiciona("Descarga Ordenes - finalizacion " + Fecha + " " + Hora);


            /******** CARGA LOS PROVEEDORES OK ********/
            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Adiciona("Crea Proveedores inicio " + Fecha + " " + Hora);
            String Proveedores;
            Proveedores = "";


            //Proveedores = GetProveedoresALL_DSIGE();

            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Adiciona("Crea Proveedores - Fin " + Fecha + " " + Hora);



            /******** CARGA LOS PRODUCTOS OK ********/
            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Adiciona("Crea Productos inicio " + Fecha + " " + Hora);
            String Articulos;
            Articulos = "";


            //Articulos = GetArticulosALL_DSIGE();

            Hora = DateTime.Now.ToString("HH:mm:ss");
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
            Adiciona("Crea productos - Fin " + Fecha + " " + Hora);

            try
            {
                //sql = "update MAG_PROCESO_MANUAL set estado='E' where proceso='" + proceso + "'";
                //SqlConnection connectionActualiza;
                //SqlCommand commandActualiza;
                //connectionActualiza = new SqlConnection(ConnectionString);
                //connectionActualiza.Open();
                //commandActualiza = new SqlCommand(sql, connectionActualiza);
                //commandActualiza.CommandType = System.Data.CommandType.Text;
                //commandActualiza.ExecuteNonQuery();
                //connectionActualiza.Close();


                Adiciona("Se actualiza el proceso manual " + Fecha + " " + Hora + " " + proceso);
            }
            catch (MySqlException ex)
            {
                string gx = ex.Message;


                Hora = DateTime.Now.ToString("HH:mm:ss");
                Fecha = DateTime.Now.ToString("dd/MM/yyyy");
                //Adiciona("Error en el proceso de OC Manual " + Fecha + " " + Hora + " " + ex.Message);
            }
            finally
            {
                //conexionBD.Close(); //Cierra la conexión a MySQL
            }




            BtnManual.Enabled = true;
            TimerVtex.Start();
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            int tiempo = 0;
            tiempo = Convert.ToInt32(txtTime.Text );
            TimerVtex.Interval = (tiempo * 60) * 1000;
        }
    }  
    
}

