using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows.Forms;
using KingMeServer; 

namespace PI_3_Defensores_de_Hastings 
{
    public partial class frmTelaInicial : Form 
    {
        private string versao; 
        public frmTelaInicial() 
        {
            InitializeComponent(); 
            lblControleVersao.Text = Jogo.versao; 

            string imagePath = @"C:\Users\willi\Downloads\Hastings\PI_3_Defensores_de_Hastings\PI_3_Defensores_de_Hastings\Resources\Fundo_Lobby.png";

            if (System.IO.File.Exists(imagePath))
            {
                this.BackgroundImage = Image.FromFile(imagePath);
                this.BackgroundImageLayout = ImageLayout.Stretch; 
            }
            else
            {
                MessageBox.Show("Imagem de fundo não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void Tela2_Load(object sender, EventArgs e)
        {
        }

        private void btnVoltar_Click(object sender, EventArgs e) 
        {
            frmLobby lobby = new frmLobby(); 
            lobby.ShowDialog();
        }

        private void btnCriarPartida_Click(object sender, EventArgs e) 
        {
            Form1 tela1 = new Form1(); // :) Cria uma nova instância do formulário Form1
            tela1.versao = this.versao; // :) Atribui a versão do formulário atual à propriedade versao de tela1
            tela1.AtualizarTela(); // :) Chama o método AtualizarTela do formulário tela1
            tela1.ShowDialog(); // :) Exibe o formulário tela1 como uma caixa de diálogo modal
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnComoJogar_Click(object sender, EventArgs e)
        {
            FrmComoJogar comoJogar = new FrmComoJogar();
            comoJogar.ShowDialog();
            
        }
    }
}
