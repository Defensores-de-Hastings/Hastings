using System; // :) Importa o namespace System
using System.Collections.Generic; // :) Importa o namespace para coleções genéricas
using System.ComponentModel; // :) Importa o namespace para componentes
using System.Data; // :) Importa o namespace para acesso a dados
using System.Drawing; // :) Importa o namespace para manipulação gráfica
using System.Linq; // :) Importa o namespace LINQ para consultas
using System.Text; // :) Importa o namespace para manipulação de texto
using System.Threading.Tasks; // :) Importa o namespace para tarefas assíncronas
using System.Windows.Forms; // :) Importa o namespace para Windows Forms
using KingMeServer; // :) Importa o namespace KingMeServer

namespace PI_3_Defensores_de_Hastings // :) Define o namespace da aplicação
{
    public partial class frmLobby : Form // :) Declara a classe frmLobby que herda de Form
    {
        public frmLobby() // :) Construtor da classe frmLobby
        {
            InitializeComponent(); // :) Inicializa os componentes do formulário
            tmrVerificarPartida.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) // :) Evento de clique em uma célula do DataGridView
        {
            // :) Método vazio, sem implementação
        }

        private void btnEntrarPartida_Click(object sender, EventArgs e) // :) Evento de clique do botão Entrar Partida
        {
            string nomeJogador = txtbNomeDoJogador.Text.Trim(); // :) Obtém e remove espaços em branco do nome do jogador
            string senhaSala = txtbSenhaDaSala.Text.Trim(); // :) Obtém e remove espaços em branco da senha da sala

            if (string.IsNullOrEmpty(nomeJogador)) // :) Verifica se o nome do jogador está vazio
            {
                MessageBox.Show("Insira o seu nome: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro
                return; // :) Encerra a execução do método
            }
            if (string.IsNullOrEmpty(senhaSala)) // :) Verifica se a senha da sala está vazia
            {
                MessageBox.Show("Insira a sua senha: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro
                return; // :) Encerra a execução do método
            }
            if (dgvPartidas.SelectedRows.Count == 0) // :) Verifica se nenhuma partida foi selecionada
            {
                MessageBox.Show("Selecione uma partida: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro
                return; // :) Encerra a execução do método
            }

            Partida match = (Partida)dgvPartidas.SelectedRows[0].DataBoundItem; // :) Obtém a partida selecionada
            int idSala = match.id; // :) Obtém o ID da sala da partida selecionada

            string idJogadorInfo = Jogo.Entrar(idSala, nomeJogador, senhaSala); // :) Tenta entrar na partida e obtém as informações do jogador
            string[] info = idJogadorInfo.Split(','); // :) Separa as informações do jogador utilizando a vírgula

            if (info.Length < 2) // :) Verifica se as informações obtidas são suficientes
            {
                MessageBox.Show("Erro ao entrar na partida. Verifique as credenciais.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro
                return; // :) Encerra a execução do método
            }

            string idJogador = info[0]; // :) Obtém o ID do jogador
            string senhaJogador = info[1]; // :) Obtém a senha do jogador

            frmLobbyDaPartida gameLobby = new frmLobbyDaPartida(idSala, idJogador, senhaJogador); // :) Cria o formulário do lobby da partida com as informações necessárias
            gameLobby.ShowDialog(); // :) Exibe o formulário do lobby da partida como uma caixa de diálogo modal
        }

        private void btnVoltar_Click(object sender, EventArgs e) // :) Evento de clique do botão Voltar
        {
            this.Close(); // :) Fecha o formulário atual
        }

        private void ListarPartidas()
        {
            dgvPartidas.DataSource = Partida.ListarPartidas(); // :) Define a fonte de dados do DataGridView com a lista de partidas

            dgvPartidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // :) Define a seleção para linhas inteiras
            dgvPartidas.EditMode = DataGridViewEditMode.EditProgrammatically; // :) Define o modo de edição para ser feito programaticamente
            dgvPartidas.AllowUserToResizeRows = false; // :) Impede que o usuário redimensione as linhas
            dgvPartidas.AllowUserToResizeColumns = false; // :) Impede que o usuário redimensione as colunas
            dgvPartidas.RowHeadersVisible = false; // :) Oculta os cabeçalhos das linhas
            dgvPartidas.MultiSelect = false; // :) Permite a seleção de apenas uma linha

            dgvPartidas.Columns[0].Visible = true; // :) Torna visível a primeira coluna
            dgvPartidas.Columns[1].HeaderText = "Nome da Partida"; // :) Define o texto do cabeçalho da segunda coluna
            dgvPartidas.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // :) Configura a segunda coluna para preencher o espaço disponível
        }

        private void tmrVerificarPartida_Tick(object sender, EventArgs e)
        {
            tmrVerificarPartida.Enabled = false;
            lblTimer.Text += DateTime.Now.ToString() + "\n";
            ListarPartidas();
            tmrVerificarPartida.Enabled = true;
        }

       

    

    }
}
