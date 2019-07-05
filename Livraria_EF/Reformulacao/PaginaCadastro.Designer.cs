namespace Livraria_AdoNet
{
    partial class PaginaCadastro
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaginaCadastro));
            this.btnFechar = new System.Windows.Forms.Button();
            this.Top = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtNascimento = new System.Windows.Forms.DateTimePicker();
            this.mbEstado = new System.Windows.Forms.MaskedTextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbTipoCadastro = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mbTelefone = new System.Windows.Forms.MaskedTextBox();
            this.mbCpf = new System.Windows.Forms.MaskedTextBox();
            this.txtNumeroCasa = new System.Windows.Forms.TextBox();
            this.txtRua = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSexo = new System.Windows.Forms.ComboBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFechar
            // 
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.Image")));
            this.btnFechar.Location = new System.Drawing.Point(827, 6);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(70, 61);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // Top
            // 
            this.Top.Controls.Add(this.btnVoltar);
            this.Top.Controls.Add(this.btnMinimizar);
            this.Top.Controls.Add(this.pictureBox2);
            this.Top.Controls.Add(this.label1);
            this.Top.Controls.Add(this.btnFechar);
            this.Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Top.Location = new System.Drawing.Point(0, 0);
            this.Top.Name = "Top";
            this.Top.Size = new System.Drawing.Size(900, 79);
            this.Top.TabIndex = 3;
            this.Top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Top_MouseDown);
            this.Top.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Top_MouseMove);
            this.Top.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Top_MouseUp);
            // 
            // btnVoltar
            // 
            this.btnVoltar.FlatAppearance.BorderSize = 0;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.Image = ((System.Drawing.Image)(resources.GetObject("btnVoltar.Image")));
            this.btnVoltar.Location = new System.Drawing.Point(4, 6);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(70, 61);
            this.btnVoltar.TabIndex = 8;
            this.btnVoltar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVoltar.Click += new System.EventHandler(this.BtnVoltar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.Image")));
            this.btnMinimizar.Location = new System.Drawing.Point(760, 6);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(70, 61);
            this.btnMinimizar.TabIndex = 7;
            this.btnMinimizar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMinimizar.Click += new System.EventHandler(this.BtnMinimizar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(167, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(70, 70);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Freestyle Script", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(243, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 58);
            this.label1.TabIndex = 5;
            this.label1.Text = "BOOK STORE";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtNascimento);
            this.panel3.Controls.Add(this.mbEstado);
            this.panel3.Controls.Add(this.lblID);
            this.panel3.Controls.Add(this.txtID);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.txtCidade);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.cbTipoCadastro);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.mbTelefone);
            this.panel3.Controls.Add(this.mbCpf);
            this.panel3.Controls.Add(this.txtNumeroCasa);
            this.panel3.Controls.Add(this.txtRua);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cbSexo);
            this.panel3.Controls.Add(this.txtNome);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(25, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(844, 400);
            this.panel3.TabIndex = 4;
            // 
            // dtNascimento
            // 
            this.dtNascimento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dtNascimento.CalendarFont = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNascimento.CalendarForeColor = System.Drawing.Color.White;
            this.dtNascimento.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.dtNascimento.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.dtNascimento.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtNascimento.CalendarTrailingForeColor = System.Drawing.Color.Silver;
            this.dtNascimento.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.dtNascimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNascimento.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dtNascimento.Location = new System.Drawing.Point(287, 167);
            this.dtNascimento.Name = "dtNascimento";
            this.dtNascimento.Size = new System.Drawing.Size(133, 27);
            this.dtNascimento.TabIndex = 3;
            // 
            // mbEstado
            // 
            this.mbEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.mbEstado.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbEstado.ForeColor = System.Drawing.Color.White;
            this.mbEstado.Location = new System.Drawing.Point(346, 345);
            this.mbEstado.Mask = "AA";
            this.mbEstado.Name = "mbEstado";
            this.mbEstado.PromptChar = '*';
            this.mbEstado.Size = new System.Drawing.Size(38, 27);
            this.mbEstado.TabIndex = 124;
            this.mbEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(502, 21);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(92, 22);
            this.lblID.TabIndex = 123;
            this.lblID.Text = "ID Assoc.";
            this.lblID.Visible = false;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.ForeColor = System.Drawing.Color.White;
            this.txtID.Location = new System.Drawing.Point(600, 21);
            this.txtID.MaxLength = 50;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(63, 27);
            this.txtID.TabIndex = 122;
            this.txtID.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(342, 318);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 21);
            this.label14.TabIndex = 121;
            this.label14.Text = "* UF";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(132, 318);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 21);
            this.label13.TabIndex = 119;
            this.label13.Text = "* Cidade";
            // 
            // txtCidade
            // 
            this.txtCidade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.txtCidade.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCidade.ForeColor = System.Drawing.Color.White;
            this.txtCidade.Location = new System.Drawing.Point(136, 342);
            this.txtCidade.MaxLength = 40;
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(185, 27);
            this.txtCidade.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(132, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 21);
            this.label10.TabIndex = 117;
            this.label10.Text = "* Rua";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(131, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(240, 22);
            this.label9.TabIndex = 116;
            this.label9.Text = "INFORMAÇÔES PESSOAIS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(499, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 22);
            this.label7.TabIndex = 115;
            this.label7.Text = "* Sexo";
            // 
            // cbTipoCadastro
            // 
            this.cbTipoCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.cbTipoCadastro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoCadastro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTipoCadastro.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoCadastro.ForeColor = System.Drawing.Color.White;
            this.cbTipoCadastro.FormattingEnabled = true;
            this.cbTipoCadastro.Items.AddRange(new object[] {
            "Cliente",
            "Funcionário"});
            this.cbTipoCadastro.Location = new System.Drawing.Point(500, 342);
            this.cbTipoCadastro.Name = "cbTipoCadastro";
            this.cbTipoCadastro.Size = new System.Drawing.Size(163, 27);
            this.cbTipoCadastro.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(495, 318);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 21);
            this.label6.TabIndex = 113;
            this.label6.Text = "* Cadastro";
            // 
            // mbTelefone
            // 
            this.mbTelefone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.mbTelefone.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbTelefone.ForeColor = System.Drawing.Color.White;
            this.mbTelefone.Location = new System.Drawing.Point(503, 167);
            this.mbTelefone.Mask = "+00(00)00000-0000";
            this.mbTelefone.Name = "mbTelefone";
            this.mbTelefone.Size = new System.Drawing.Size(162, 27);
            this.mbTelefone.TabIndex = 4;
            // 
            // mbCpf
            // 
            this.mbCpf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.mbCpf.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbCpf.ForeColor = System.Drawing.Color.White;
            this.mbCpf.Location = new System.Drawing.Point(135, 167);
            this.mbCpf.Mask = "000.000.000-00";
            this.mbCpf.Name = "mbCpf";
            this.mbCpf.Size = new System.Drawing.Size(125, 27);
            this.mbCpf.TabIndex = 2;
            // 
            // txtNumeroCasa
            // 
            this.txtNumeroCasa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.txtNumeroCasa.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroCasa.ForeColor = System.Drawing.Color.White;
            this.txtNumeroCasa.Location = new System.Drawing.Point(504, 278);
            this.txtNumeroCasa.MaxLength = 8;
            this.txtNumeroCasa.Name = "txtNumeroCasa";
            this.txtNumeroCasa.Size = new System.Drawing.Size(163, 27);
            this.txtNumeroCasa.TabIndex = 6;
            // 
            // txtRua
            // 
            this.txtRua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.txtRua.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRua.ForeColor = System.Drawing.Color.White;
            this.txtRua.Location = new System.Drawing.Point(136, 278);
            this.txtRua.MaxLength = 40;
            this.txtRua.Name = "txtRua";
            this.txtRua.Size = new System.Drawing.Size(285, 27);
            this.txtRua.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(132, 221);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 21);
            this.label11.TabIndex = 106;
            this.label11.Text = "ENDEREÇO";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(496, 254);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 21);
            this.label12.TabIndex = 105;
            this.label12.Text = "* Nº Casa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(495, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 21);
            this.label8.TabIndex = 104;
            this.label8.Text = "* Telefone";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 21);
            this.label5.TabIndex = 102;
            this.label5.Text = "* Nascimento:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(131, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 22);
            this.label4.TabIndex = 101;
            this.label4.Text = "* CPF";
            // 
            // cbSexo
            // 
            this.cbSexo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.cbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSexo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSexo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSexo.ForeColor = System.Drawing.Color.White;
            this.cbSexo.FormattingEnabled = true;
            this.cbSexo.Items.AddRange(new object[] {
            "Masculino",
            "Feminino"});
            this.cbSexo.Location = new System.Drawing.Point(503, 87);
            this.cbSexo.Name = "cbSexo";
            this.cbSexo.Size = new System.Drawing.Size(162, 27);
            this.cbSexo.TabIndex = 1;
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.txtNome.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNome.ForeColor = System.Drawing.Color.White;
            this.txtNome.Location = new System.Drawing.Point(135, 89);
            this.txtNome.MaxLength = 40;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(285, 27);
            this.txtNome.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(131, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 22);
            this.label3.TabIndex = 98;
            this.label3.Text = "* Nome";
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btnSalvar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalvar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(0, 506);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(900, 94);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "SALVAR NOVO CADASTRO";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // PaginaCadastro
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Top);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PaginaCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Livraria";
            this.Top.ResumeLayout(false);
            this.Top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnFechar;
        private new System.Windows.Forms.Panel Top;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.ComboBox cbTipoCadastro;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox mbTelefone;
        private System.Windows.Forms.MaskedTextBox mbCpf;
        private System.Windows.Forms.TextBox txtNumeroCasa;
        private System.Windows.Forms.TextBox txtRua;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSexo;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.MaskedTextBox mbEstado;
        private System.Windows.Forms.DateTimePicker dtNascimento;
    }
}

