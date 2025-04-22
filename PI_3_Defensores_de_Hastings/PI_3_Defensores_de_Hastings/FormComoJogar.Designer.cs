namespace PI_3_Defensores_de_Hastings
{
    partial class FrmComoJogar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmComoJogar));
            this.lblComoJogarTitulo = new System.Windows.Forms.Label();
            this.lblExplicacao = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblComoJogarTitulo
            // 
            this.lblComoJogarTitulo.AutoSize = true;
            this.lblComoJogarTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblComoJogarTitulo.Font = new System.Drawing.Font("Sitka Display", 60F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblComoJogarTitulo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblComoJogarTitulo.Location = new System.Drawing.Point(246, -16);
            this.lblComoJogarTitulo.Name = "lblComoJogarTitulo";
            this.lblComoJogarTitulo.Size = new System.Drawing.Size(431, 116);
            this.lblComoJogarTitulo.TabIndex = 0;
            this.lblComoJogarTitulo.Text = "Como Jogar";
            // 
            // lblExplicacao
            // 
            this.lblExplicacao.AutoSize = true;
            this.lblExplicacao.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblExplicacao.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblExplicacao.Location = new System.Drawing.Point(23, 100);
            this.lblExplicacao.Name = "lblExplicacao";
            this.lblExplicacao.Size = new System.Drawing.Size(811, 464);
            this.lblExplicacao.TabIndex = 1;
            this.lblExplicacao.Text = resources.GetString("lblExplicacao.Text");
            // 
            // FrmComoJogar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(924, 591);
            this.Controls.Add(this.lblExplicacao);
            this.Controls.Add(this.lblComoJogarTitulo);
            this.Name = "FrmComoJogar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Como Jogar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblComoJogarTitulo;
        private System.Windows.Forms.Label lblExplicacao;
    }
}