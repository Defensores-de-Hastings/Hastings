using KingMeServer;
using System;
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Data; 
using System.Drawing; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI_3_Defensores_de_Hastings 
{
    public partial class Form1 : Form 
    {
        public string versao { get; set; } 

        public Form1()
        {
            InitializeComponent(); 
        }

        public void AtualizarTela() 
        {
            
        }

        private void btnPartida_Click(object sender, EventArgs e)
        {
            string nome = nomeDaPartida.Text; 
            string senha = senhaDaPartida.Text; 
            string grupo = nomeDoGrupo.Text; 

            string id = KingMeServer.Jogo.CriarPartida(nome, senha, grupo); 
            idDaPartida.Text = id; 

            if (nome == string.Empty) 
            {
                MessageBox.Show("Insira o nome da partida: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro se o nome estiver vazio
                return;
            }

            if (senha == string.Empty) 
            {
                MessageBox.Show("Insira a sua senha: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro se a senha estiver vazia
                return; 
            }

            if (grupo == string.Empty) 
            {
                MessageBox.Show("Insira o nome do grupo: ", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); // :) Exibe mensagem de erro se o grupo estiver vazio
                return; 
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e) 
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e) 
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) 
        {
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e) 
        {
        }

        private void label2_Click(object sender, EventArgs e) 
        {
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e) 
        {
        }

        private void label3_Click(object sender, EventArgs e) 
        {
        }

        private void idDaPartida_TextChanged(object sender, EventArgs e) 
        {
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) 
        {
        }

        private void label1_Click(object sender, EventArgs e) 
        {
        }

        private void btnChamaForms2_Click(object sender, EventArgs e) 
        {
            this.Close(); 
        }

        private void lblControleDeVersao_Click(object sender, EventArgs e) 
        {
        }

        private void lblData_Click(object sender, EventArgs e) 
        {
        }

        private void nomeDoGrupo_TextChanged(object sender, EventArgs e) 
        {
        }

        private void nomeDaPartida_TextChanged(object sender, EventArgs e)
        {
        }
    }
}