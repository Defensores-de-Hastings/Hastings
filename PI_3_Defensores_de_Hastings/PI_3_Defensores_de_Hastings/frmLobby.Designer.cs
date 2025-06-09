namespace PI_3_Defensores_de_Hastings
{
    partial class frmLobby
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPartidas = new System.Windows.Forms.DataGridView();
            this.btnEntrarPartida = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.txtbSenhaDaSala = new System.Windows.Forms.TextBox();
            this.txtbNomeDoJogador = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.lblNomeJogador = new System.Windows.Forms.Label();
            this.tmrVerificarPartida = new System.Windows.Forms.Timer(this.components);
            this.lblTimer = new System.Windows.Forms.Label();
            this.bntAtualizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPartidas
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPartidas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPartidas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvPartidas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvPartidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartidas.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPartidas.Location = new System.Drawing.Point(13, 13);
            this.dgvPartidas.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPartidas.Name = "dgvPartidas";
            this.dgvPartidas.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.dgvPartidas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPartidas.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPartidas.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPartidas.Size = new System.Drawing.Size(584, 623);
            this.dgvPartidas.TabIndex = 0;
            this.dgvPartidas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnEntrarPartida
            // 
            this.btnEntrarPartida.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEntrarPartida.Font = new System.Drawing.Font("Sitka Display", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnEntrarPartida.ForeColor = System.Drawing.Color.Black;
            this.btnEntrarPartida.Location = new System.Drawing.Point(638, 364);
            this.btnEntrarPartida.Margin = new System.Windows.Forms.Padding(4);
            this.btnEntrarPartida.Name = "btnEntrarPartida";
            this.btnEntrarPartida.Size = new System.Drawing.Size(266, 86);
            this.btnEntrarPartida.TabIndex = 1;
            this.btnEntrarPartida.Text = "Entrar";
            this.btnEntrarPartida.UseVisualStyleBackColor = true;
            this.btnEntrarPartida.Click += new System.EventHandler(this.btnEntrarPartida_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnVoltar.Font = new System.Drawing.Font("Sitka Display", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnVoltar.ForeColor = System.Drawing.Color.Black;
            this.btnVoltar.Location = new System.Drawing.Point(638, 550);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(266, 86);
            this.btnVoltar.TabIndex = 2;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // txtbSenhaDaSala
            // 
            this.txtbSenhaDaSala.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtbSenhaDaSala.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbSenhaDaSala.Location = new System.Drawing.Point(642, 156);
            this.txtbSenhaDaSala.Margin = new System.Windows.Forms.Padding(4);
            this.txtbSenhaDaSala.Name = "txtbSenhaDaSala";
            this.txtbSenhaDaSala.Size = new System.Drawing.Size(266, 22);
            this.txtbSenhaDaSala.TabIndex = 18;
            // 
            // txtbNomeDoJogador
            // 
            this.txtbNomeDoJogador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtbNomeDoJogador.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbNomeDoJogador.Location = new System.Drawing.Point(642, 70);
            this.txtbNomeDoJogador.Margin = new System.Windows.Forms.Padding(4);
            this.txtbNomeDoJogador.Name = "txtbNomeDoJogador";
            this.txtbNomeDoJogador.Size = new System.Drawing.Size(266, 22);
            this.txtbNomeDoJogador.TabIndex = 17;
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenha.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblSenha.Location = new System.Drawing.Point(638, 133);
            this.lblSenha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(124, 19);
            this.lblSenha.TabIndex = 16;
            this.lblSenha.Text = "Senha da Sala:";
            // 
            // lblNomeJogador
            // 
            this.lblNomeJogador.AutoSize = true;
            this.lblNomeJogador.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeJogador.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblNomeJogador.Location = new System.Drawing.Point(638, 47);
            this.lblNomeJogador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNomeJogador.Name = "lblNomeJogador";
            this.lblNomeJogador.Size = new System.Drawing.Size(152, 19);
            this.lblNomeJogador.TabIndex = 15;
            this.lblNomeJogador.Text = "Nome do Jogador:";
            // 
            // tmrVerificarPartida
            // 
            this.tmrVerificarPartida.Interval = 5000;
            this.tmrVerificarPartida.Tick += new System.EventHandler(this.tmrVerificarPartida_Tick);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTimer.Location = new System.Drawing.Point(639, 193);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(33, 16);
            this.lblTimer.TabIndex = 20;
            this.lblTimer.Text = "label2";
            this.lblTimer.Visible = false;
            // 
            // bntAtualizar
            // 
            this.bntAtualizar.Font = new System.Drawing.Font("Sitka Display", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntAtualizar.ForeColor = System.Drawing.Color.Black;
            this.bntAtualizar.Location = new System.Drawing.Point(638, 457);
            this.bntAtualizar.Name = "bntAtualizar";
            this.bntAtualizar.Size = new System.Drawing.Size(266, 86);
            this.bntAtualizar.TabIndex = 21;
            this.bntAtualizar.Text = "Atulizar";
            this.bntAtualizar.UseVisualStyleBackColor = true;
            this.bntAtualizar.Click += new System.EventHandler(this.bntAtualizar_Click);
            // 
            // frmLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1011, 658);
            this.Controls.Add(this.bntAtualizar);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.txtbSenhaDaSala);
            this.Controls.Add(this.txtbNomeDoJogador);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.lblNomeJogador);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnEntrarPartida);
            this.Controls.Add(this.dgvPartidas);
            this.Font = new System.Drawing.Font("Sitka Display", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmLobby";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLobby";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPartidas;
        private System.Windows.Forms.Button btnEntrarPartida;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.TextBox txtbSenhaDaSala;
        private System.Windows.Forms.TextBox txtbNomeDoJogador;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.Label lblNomeJogador;
        private System.Windows.Forms.Timer tmrVerificarPartida;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button bntAtualizar;
    }
}