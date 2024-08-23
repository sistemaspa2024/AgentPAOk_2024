namespace VtexIconTray
{
    partial class PAperuApp
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PAperuApp));
            this.NotifyIconVtex = new System.Windows.Forms.NotifyIcon(this.components);
            this.TimerVtex = new System.Windows.Forms.Timer(this.components);
            this.tabVtex = new System.Windows.Forms.TabControl();
            this.TabEventos = new System.Windows.Forms.TabPage();
            this.grpEventos = new System.Windows.Forms.GroupBox();
            this.LstVtex = new System.Windows.Forms.ListView();
            this.Descripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabConfiguration = new System.Windows.Forms.TabPage();
            this.grpConfiguration = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtApiToken = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.btnCancelConfig = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.tabDatabase = new System.Windows.Forms.TabPage();
            this.grpDatabase = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.tabManual = new System.Windows.Forms.TabPage();
            this.DtFecha = new System.Windows.Forms.DateTimePicker();
            this.DtHoraProceso = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnManual = new System.Windows.Forms.Button();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BtnCloseAbout = new System.Windows.Forms.Button();
            this.tabVtex.SuspendLayout();
            this.TabEventos.SuspendLayout();
            this.grpEventos.SuspendLayout();
            this.TabConfiguration.SuspendLayout();
            this.grpConfiguration.SuspendLayout();
            this.tabDatabase.SuspendLayout();
            this.grpDatabase.SuspendLayout();
            this.tabManual.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIconVtex
            // 
            this.NotifyIconVtex.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIconVtex.Icon")));
            this.NotifyIconVtex.Text = "Agente PA v1";
            this.NotifyIconVtex.Visible = true;
            this.NotifyIconVtex.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIconVtex_MouseDoubleClick);
            // 
            // TimerVtex
            // 
            this.TimerVtex.Interval = 60000;
            this.TimerVtex.Tick += new System.EventHandler(this.TimerTick);
            // 
            // tabVtex
            // 
            this.tabVtex.Controls.Add(this.TabEventos);
            this.tabVtex.Controls.Add(this.TabConfiguration);
            this.tabVtex.Controls.Add(this.tabDatabase);
            this.tabVtex.Controls.Add(this.tabManual);
            this.tabVtex.Controls.Add(this.tabAbout);
            this.tabVtex.Location = new System.Drawing.Point(3, 4);
            this.tabVtex.Margin = new System.Windows.Forms.Padding(4);
            this.tabVtex.Name = "tabVtex";
            this.tabVtex.SelectedIndex = 0;
            this.tabVtex.Size = new System.Drawing.Size(868, 357);
            this.tabVtex.TabIndex = 14;
            // 
            // TabEventos
            // 
            this.TabEventos.Controls.Add(this.grpEventos);
            this.TabEventos.Location = new System.Drawing.Point(4, 25);
            this.TabEventos.Margin = new System.Windows.Forms.Padding(4);
            this.TabEventos.Name = "TabEventos";
            this.TabEventos.Padding = new System.Windows.Forms.Padding(4);
            this.TabEventos.Size = new System.Drawing.Size(860, 328);
            this.TabEventos.TabIndex = 0;
            this.TabEventos.Text = "Eventos";
            this.TabEventos.UseVisualStyleBackColor = true;
            // 
            // grpEventos
            // 
            this.grpEventos.Controls.Add(this.LstVtex);
            this.grpEventos.Location = new System.Drawing.Point(8, 7);
            this.grpEventos.Margin = new System.Windows.Forms.Padding(4);
            this.grpEventos.Name = "grpEventos";
            this.grpEventos.Padding = new System.Windows.Forms.Padding(4);
            this.grpEventos.Size = new System.Drawing.Size(843, 314);
            this.grpEventos.TabIndex = 4;
            this.grpEventos.TabStop = false;
            this.grpEventos.Text = "Eventos";
            // 
            // LstVtex
            // 
            this.LstVtex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Descripcion});
            this.LstVtex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstVtex.FullRowSelect = true;
            this.LstVtex.GridLines = true;
            this.LstVtex.HideSelection = false;
            this.LstVtex.Location = new System.Drawing.Point(7, 21);
            this.LstVtex.Margin = new System.Windows.Forms.Padding(4);
            this.LstVtex.MultiSelect = false;
            this.LstVtex.Name = "LstVtex";
            this.LstVtex.Size = new System.Drawing.Size(829, 250);
            this.LstVtex.TabIndex = 1;
            this.LstVtex.UseCompatibleStateImageBehavior = false;
            this.LstVtex.View = System.Windows.Forms.View.Details;
            this.LstVtex.SelectedIndexChanged += new System.EventHandler(this.LstVtex_SelectedIndexChanged_1);
            // 
            // Descripcion
            // 
            this.Descripcion.Text = "Descripcion";
            this.Descripcion.Width = 615;
            // 
            // TabConfiguration
            // 
            this.TabConfiguration.Controls.Add(this.grpConfiguration);
            this.TabConfiguration.Location = new System.Drawing.Point(4, 25);
            this.TabConfiguration.Margin = new System.Windows.Forms.Padding(4);
            this.TabConfiguration.Name = "TabConfiguration";
            this.TabConfiguration.Padding = new System.Windows.Forms.Padding(4);
            this.TabConfiguration.Size = new System.Drawing.Size(860, 328);
            this.TabConfiguration.TabIndex = 1;
            this.TabConfiguration.Text = "Configuracion";
            this.TabConfiguration.UseVisualStyleBackColor = true;
            // 
            // grpConfiguration
            // 
            this.grpConfiguration.Controls.Add(this.label6);
            this.grpConfiguration.Controls.Add(this.txtApiToken);
            this.grpConfiguration.Controls.Add(this.label5);
            this.grpConfiguration.Controls.Add(this.txtApiKey);
            this.grpConfiguration.Controls.Add(this.btnCancelConfig);
            this.grpConfiguration.Controls.Add(this.btnSaveConfig);
            this.grpConfiguration.Controls.Add(this.label8);
            this.grpConfiguration.Controls.Add(this.txtTime);
            this.grpConfiguration.Location = new System.Drawing.Point(8, 7);
            this.grpConfiguration.Margin = new System.Windows.Forms.Padding(4);
            this.grpConfiguration.Name = "grpConfiguration";
            this.grpConfiguration.Padding = new System.Windows.Forms.Padding(4);
            this.grpConfiguration.Size = new System.Drawing.Size(841, 310);
            this.grpConfiguration.TabIndex = 13;
            this.grpConfiguration.TabStop = false;
            this.grpConfiguration.Text = "Configuracion";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 114);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Clave";
            // 
            // txtApiToken
            // 
            this.txtApiToken.Location = new System.Drawing.Point(189, 110);
            this.txtApiToken.Margin = new System.Windows.Forms.Padding(4);
            this.txtApiToken.MaxLength = 80;
            this.txtApiToken.Name = "txtApiToken";
            this.txtApiToken.PasswordChar = '*';
            this.txtApiToken.Size = new System.Drawing.Size(492, 22);
            this.txtApiToken.TabIndex = 13;
            this.txtApiToken.Text = "uY63waFBxX929Ej";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 82);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Usuario";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(189, 78);
            this.txtApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtApiKey.MaxLength = 50;
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(492, 22);
            this.txtApiKey.TabIndex = 11;
            this.txtApiKey.Text = "testuser";
            // 
            // btnCancelConfig
            // 
            this.btnCancelConfig.Location = new System.Drawing.Point(359, 230);
            this.btnCancelConfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelConfig.Name = "btnCancelConfig";
            this.btnCancelConfig.Size = new System.Drawing.Size(139, 28);
            this.btnCancelConfig.TabIndex = 10;
            this.btnCancelConfig.Text = "Cancela";
            this.btnCancelConfig.UseVisualStyleBackColor = true;
            this.btnCancelConfig.Visible = false;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(152, 230);
            this.btnSaveConfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(139, 28);
            this.btnSaveConfig.TabIndex = 9;
            this.btnSaveConfig.Text = "Actualiza";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 44);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Tiempo Ejecucion";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(189, 41);
            this.txtTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtTime.MaxLength = 4;
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(39, 22);
            this.txtTime.TabIndex = 0;
            this.txtTime.Text = "1";
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabDatabase
            // 
            this.tabDatabase.Controls.Add(this.grpDatabase);
            this.tabDatabase.Location = new System.Drawing.Point(4, 25);
            this.tabDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Padding = new System.Windows.Forms.Padding(4);
            this.tabDatabase.Size = new System.Drawing.Size(860, 328);
            this.tabDatabase.TabIndex = 2;
            this.tabDatabase.Text = "Base de Datos";
            this.tabDatabase.UseVisualStyleBackColor = true;
            // 
            // grpDatabase
            // 
            this.grpDatabase.Controls.Add(this.btnSave);
            this.grpDatabase.Controls.Add(this.btnTest);
            this.grpDatabase.Controls.Add(this.txtUser);
            this.grpDatabase.Controls.Add(this.txtClave);
            this.grpDatabase.Controls.Add(this.label4);
            this.grpDatabase.Controls.Add(this.label3);
            this.grpDatabase.Controls.Add(this.txtDatabase);
            this.grpDatabase.Controls.Add(this.label2);
            this.grpDatabase.Controls.Add(this.label1);
            this.grpDatabase.Controls.Add(this.txtServer);
            this.grpDatabase.Location = new System.Drawing.Point(8, 7);
            this.grpDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.Padding = new System.Windows.Forms.Padding(4);
            this.grpDatabase.Size = new System.Drawing.Size(841, 310);
            this.grpDatabase.TabIndex = 14;
            this.grpDatabase.TabStop = false;
            this.grpDatabase.Text = "Base de Datos";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(493, 230);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 28);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Actualiza";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(658, 230);
            this.btnTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(139, 28);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "Test Conexion";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(189, 126);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtUser.MaxLength = 20;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(144, 22);
            this.txtUser.TabIndex = 7;
            this.txtUser.Text = "adm_sapdsige";
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(189, 172);
            this.txtClave.Margin = new System.Windows.Forms.Padding(4);
            this.txtClave.MaxLength = 20;
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(144, 22);
            this.txtClave.TabIndex = 6;
            this.txtClave.Text = "Admin*4321";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 176);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Clave";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Usuario";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(189, 80);
            this.txtDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.txtDatabase.MaxLength = 40;
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(306, 22);
            this.txtDatabase.TabIndex = 3;
            this.txtDatabase.Text = "INTERFACE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Base de Datos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Servidor";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(189, 41);
            this.txtServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtServer.MaxLength = 40;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(306, 22);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "10.30.18.253";
            // 
            // tabManual
            // 
            this.tabManual.Controls.Add(this.DtFecha);
            this.tabManual.Controls.Add(this.DtHoraProceso);
            this.tabManual.Controls.Add(this.label7);
            this.tabManual.Controls.Add(this.BtnManual);
            this.tabManual.Location = new System.Drawing.Point(4, 25);
            this.tabManual.Margin = new System.Windows.Forms.Padding(4);
            this.tabManual.Name = "tabManual";
            this.tabManual.Padding = new System.Windows.Forms.Padding(4);
            this.tabManual.Size = new System.Drawing.Size(860, 328);
            this.tabManual.TabIndex = 4;
            this.tabManual.Text = "Procesos Manuales";
            this.tabManual.UseVisualStyleBackColor = true;
            // 
            // DtFecha
            // 
            this.DtFecha.Location = new System.Drawing.Point(347, 89);
            this.DtFecha.Name = "DtFecha";
            this.DtFecha.Size = new System.Drawing.Size(200, 22);
            this.DtFecha.TabIndex = 5;
            // 
            // DtHoraProceso
            // 
            this.DtHoraProceso.CustomFormat = "HH:MM";
            this.DtHoraProceso.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.DtHoraProceso.Enabled = false;
            this.DtHoraProceso.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtHoraProceso.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DtHoraProceso.Location = new System.Drawing.Point(290, 247);
            this.DtHoraProceso.Margin = new System.Windows.Forms.Padding(4);
            this.DtHoraProceso.Name = "DtHoraProceso";
            this.DtHoraProceso.ShowUpDown = true;
            this.DtHoraProceso.Size = new System.Drawing.Size(113, 22);
            this.DtHoraProceso.TabIndex = 4;
            this.DtHoraProceso.Value = new System.DateTime(2021, 11, 1, 22, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 247);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(224, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Hora de ejecucion Procesos Diarios";
            // 
            // BtnManual
            // 
            this.BtnManual.Location = new System.Drawing.Point(70, 73);
            this.BtnManual.Margin = new System.Windows.Forms.Padding(4);
            this.BtnManual.Name = "BtnManual";
            this.BtnManual.Size = new System.Drawing.Size(179, 58);
            this.BtnManual.TabIndex = 0;
            this.BtnManual.Text = "Proceso Manual";
            this.BtnManual.UseVisualStyleBackColor = true;
            this.BtnManual.Click += new System.EventHandler(this.BtnManual_Click);
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.groupBox1);
            this.tabAbout.Location = new System.Drawing.Point(4, 25);
            this.tabAbout.Margin = new System.Windows.Forms.Padding(4);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(4);
            this.tabAbout.Size = new System.Drawing.Size(860, 328);
            this.tabAbout.TabIndex = 3;
            this.tabAbout.Text = "Acerca de";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.BtnCloseAbout);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(841, 310);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PA PERU SAC";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.textBox1.Location = new System.Drawing.Point(56, 48);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(729, 74);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "El sistema de interfases permite la carga y descarga de la informacion de SAP y D" +
    "sige, de ida y vuelta";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // BtnCloseAbout
            // 
            this.BtnCloseAbout.Location = new System.Drawing.Point(647, 238);
            this.BtnCloseAbout.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCloseAbout.Name = "BtnCloseAbout";
            this.BtnCloseAbout.Size = new System.Drawing.Size(139, 28);
            this.BtnCloseAbout.TabIndex = 9;
            this.BtnCloseAbout.Text = "Cierra";
            this.BtnCloseAbout.UseVisualStyleBackColor = true;
            this.BtnCloseAbout.Click += new System.EventHandler(this.button2_Click);
            // 
            // VtexApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 368);
            this.Controls.Add(this.tabVtex);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VtexApp";
            this.Text = "Aplicacion de Transferencia de SAP - DSIGE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.VtexApp_Resize);
            this.tabVtex.ResumeLayout(false);
            this.TabEventos.ResumeLayout(false);
            this.grpEventos.ResumeLayout(false);
            this.TabConfiguration.ResumeLayout(false);
            this.grpConfiguration.ResumeLayout(false);
            this.grpConfiguration.PerformLayout();
            this.tabDatabase.ResumeLayout(false);
            this.grpDatabase.ResumeLayout(false);
            this.grpDatabase.PerformLayout();
            this.tabManual.ResumeLayout(false);
            this.tabManual.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIconVtex;
        private System.Windows.Forms.Timer TimerVtex;
        private System.Windows.Forms.TabControl tabVtex;
        private System.Windows.Forms.TabPage TabEventos;
        private System.Windows.Forms.GroupBox grpEventos;
        private System.Windows.Forms.ListView LstVtex;
        private System.Windows.Forms.ColumnHeader Descripcion;
        private System.Windows.Forms.TabPage TabConfiguration;
        private System.Windows.Forms.GroupBox grpConfiguration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtApiToken;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Button btnCancelConfig;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TabPage tabDatabase;
        private System.Windows.Forms.GroupBox grpDatabase;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BtnCloseAbout;
        private System.Windows.Forms.TabPage tabManual;
        private System.Windows.Forms.Button BtnManual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker DtHoraProceso;
        private System.Windows.Forms.DateTimePicker DtFecha;
    }
}

