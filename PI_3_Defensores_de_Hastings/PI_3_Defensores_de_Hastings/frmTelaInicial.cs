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

namespace PI_3_Defensores_de_Hastings // :) Define o namespace da aplicação
{
    public partial class frmTelaInicial : Form // :) Declara a classe frmTelaInicial que herda de Form
    {
        private string versao; // :) Declara uma variável privada para armazenar a versão
        public frmTelaInicial() // :) Construtor da classe frmTelaInicial
        {
            InitializeComponent(); // :) Inicializa os componentes do formulário
            lblControleVersao.Text = Jogo.versao; // :) Atualiza o label lblControleVersao com a versão obtida de Jogo

            string imagePath = @".\imagens\Fundo_Lobby.png";

            if (System.IO.File.Exists(imagePath))
            {
                this.BackgroundImage = Properties.Resources;
                this.BackgroundImageLayout = ImageLayout.Stretch; // :) imagem da tela de fundo
            }
            else
            {
                MessageBox.Show("Imagem de fundo não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void Tela2_Load(object sender, EventArgs e) // :) Evento de carregamento do formulário Tela2
        {
        }

        private void btnVoltar_Click(object sender, EventArgs e) // :) Evento de clique do botão btnVoltar
        {
            frmLobby lobby = new frmLobby(); // :) Cria uma nova instância do formulário frmLobby
            lobby.ShowDialog(); // :) Exibe o formulário frmLobby como uma caixa de diálogo modal
        }

        private void btnCriarPartida_Click(object sender, EventArgs e) // :) Evento de clique do botão btnCriarPartida
        {
            Form1 tela1 = new Form1(); // :) Cria uma nova instância do formulário Form1
            tela1.versao = this.versao; // :) Atribui a versão do formulário atual à propriedade versao de tela1
            tela1.AtualizarTela(); // :) Chama o método AtualizarTela do formulário tela1
            tela1.ShowDialog(); // :) Exibe o formulário tela1 como uma caixa de diálogo modal
        }

        
    }
}
