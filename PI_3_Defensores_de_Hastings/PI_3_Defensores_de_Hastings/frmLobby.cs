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
    public partial class frmLobby : Form 
    {
        public frmLobby()
        {
            InitializeComponent(); 
            ListarPartidas();
            tmrVerificarPartida.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) 
        {
            //Método vazio, sem implementação
        }

        private void btnEntrarPartida_Click(object sender, EventArgs e) 
        {
            string nomeJogador = txtbNomeDoJogador.Text.Trim(); // :) Obtém e remove espaços em branco do nome do jogador
            string senhaSala = txtbSenhaDaSala.Text.Trim(); // :) Obtém e remove espaços em branco da senha da sala

            if (string.IsNullOrEmpty(nomeJogador)) // :) Verifica se o nome do jogador está vazio
            {
                MessageBox.Show("Insira o seu nome: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro
                return; 
            }
            if (string.IsNullOrEmpty(senhaSala)) // :) Verifica se a senha da sala está vazia
            {
                MessageBox.Show("Insira a sua senha: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro
                return; 
            }
            if (dgvPartidas.SelectedRows.Count == 0) // :) Verifica se nenhuma partida foi selecionada
            {
                MessageBox.Show("Selecione uma partida: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro
                return; 
            }

            Partida match = (Partida)dgvPartidas.SelectedRows[0].DataBoundItem; // :) Obtém a partida selecionada
            int idSala = match.id; 

            string idJogadorInfo = Jogo.Entrar(idSala, nomeJogador, senhaSala); // :) Tenta entrar na partida e obtém as informações do jogador
            string[] info = idJogadorInfo.Split(','); // :) Separa as informações do jogador utilizando a vírgula

            if (info.Length < 2) // :) Verifica se as informações obtidas são suficientes
            {
                MessageBox.Show("Erro ao entrar na partida. Verifique as credenciais.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro
                return; 
            }

            string idJogador = info[0]; // :) Obtém o ID do jogador
            string senhaJogador = info[1]; // :) Obtém a senha do jogador

            frmLobbyDaPartida gameLobby = new frmLobbyDaPartida(idSala, idJogador, senhaJogador); // :) Cria o formulário do lobby da partida com as informações necessárias
            gameLobby.ShowDialog(); // :) Exibe o formulário do lobby da partida como uma caixa de diálogo modal
        }

        private void btnVoltar_Click(object sender, EventArgs e) // :) Evento de clique do botão Voltar
        {
            this.Close();
        }

        private void ListarPartidas()
        {
            dgvPartidas.DataSource = Partida.ListarPartidas();

            dgvPartidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPartidas.EditMode = DataGridViewEditMode.EditProgrammatically; 
            dgvPartidas.AllowUserToResizeRows = false; 
            dgvPartidas.AllowUserToResizeColumns = false;
            dgvPartidas.RowHeadersVisible = false; 
            dgvPartidas.MultiSelect = false;

            dgvPartidas.Columns[0].Visible = true; 
            dgvPartidas.Columns[1].HeaderText = "Nome da Partida"; 
            dgvPartidas.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 
        }

        private void tmrVerificarPartida_Tick(object sender, EventArgs e)
        {
            tmrVerificarPartida.Enabled = false;
            lblTimer.Text += DateTime.Now.ToString() + "\n";
            ListarPartidas();
            tmrVerificarPartida.Enabled = true;
        }

        private void bntAtualizar_Click(object sender, EventArgs e)
        {
            ListarPartidas();
        }
    }
}
