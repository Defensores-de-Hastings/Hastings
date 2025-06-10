using KingMeServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PI_3_Defensores_de_Hastings
{
    public partial class frmLobbyDaPartida : Form
    {
        private readonly int _idSala;
        private readonly string _senhaDoJogador;
        private readonly List<string> _initialLetters = new List<string> { "A", "B", "C", "D", "E", "G", "H", "K", "L", "M", "Q", "R", "T" };
        private List<string> _availableLetters;
        private List<string> _playerCards;
        private string _resultadoFinal;
        private int Contador = 0;
        // Propriedade para obter o ID do jogador como int
        private int IdJogador => int.TryParse(lblMostraID.Text, out var id) ? id : -1;

        public frmLobbyDaPartida(int idSala, string idJogador, string senhaJogador)
        {
            InitializeComponent();
            _idSala = idSala;
            _senhaDoJogador = senhaJogador;
            lblMostraID.Text = idJogador;
            lblMostraSenha.Text = senhaJogador;

            _availableLetters = new List<string>(_initialLetters);
            _playerCards = new List<string>();

            lblEstadoJogo.Visible = false;
            txtID.Text = idJogador;
            txtSenha.Text = senhaJogador;

            
            tmrVez.Enabled = true;
        }

        private void bntComecar_Click(object sender, EventArgs e)
        {
            try
            {
                if (IdJogador < 0)
                {
                    MessageBox.Show("ID do jogador inválido. Insira um número válido.");
                    return;
                }

                Jogo.Iniciar(IdJogador, _senhaDoJogador);
                ExibirCartas();
                ExibirPersonagens();
                ExibirSetores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }

        private void ExibirCartas()
        {
            var cartas = Jogo.ListarCartas(IdJogador, _senhaDoJogador);
            txtCartas.Text = cartas;
            var dict = GetPersonagensDict();
            TXBCartas.Items.Clear();
            foreach (var letra in cartas)
            {
                var key = letra.ToString();
                TXBCartas.Items.Add(dict.ContainsKey(key) ? dict[key] : key);
                _playerCards.Add(key);
            }
        }

        private void ExibirPersonagens()
        {
            var retorno = Jogo.ListarPersonagens().Replace("\r", "").Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var dict = GetPersonagensDict();
            lstbPersonagens.Items.Clear();
            foreach (var codigo in retorno)
            {
                var key = codigo.Trim();
                lstbPersonagens.Items.Add(dict.ContainsKey(key) ? dict[key] : key);
            }
        }

        private void ExibirSetores()
        {
            var setores = Jogo.ListarSetores().Split(new[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            lstbSetores.Items.Clear();
            foreach (var setor in setores)
                lstbSetores.Items.Add(setor);
        }

        private Dictionary<string, string> GetPersonagensDict() => new Dictionary<string, string>
        {
            { "A", "Adilson Konrad" }, { "B", "Beatriz Paiva" }, { "C", "Claro" }, { "D", "Douglas Baquiao" },
            { "E", "Eduardo Takeo" }, { "G", "Guilherme Rey" }, { "H", "Heredia" }, { "K", "Karin" },
            { "L", "Leonardo Takuno" }, { "M", "Mario Toledo" }, { "Q", "Quintas" }, { "R", "Ranulffo" },
            { "T", "Toshio" }
        };

        private void ColocarPersonagem(int setor, string personagem)
        {
            var estado = Jogo.ColocarPersonagem(IdJogador, _senhaDoJogador, setor, personagem);
            lstEstadoDoJogo.Items.Clear();
            foreach (var linha in estado.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                lstEstadoDoJogo.Items.Add(linha);
        }

        private void btnColocarPersonagem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtEscolheSetor.Text, out int setor))
                ColocarPersonagem(setor, txtEscolhaPersonagem.Text);
            else
                MessageBox.Show("Setor inválido.");
        }

        private void btnListarJogadores_Click(object sender, EventArgs e)
        {
            var jogadores = Jogo.ListarJogadores(_idSala).Replace("\r", "").Split('\n');
            lstbJogadores.Items.Clear();
            lstbJogadores.Items.AddRange(jogadores);
        }

        private void btnVerificarVez_Click(object sender, EventArgs e)
        {
            VerificarVez();
        }

        private void VerificarVez()
        {
            var verificacao = Jogo.VerificarVez(_idSala).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            lstbVerificarVez.Items.Clear();
            var linhasFormatadas = new List<string>();

            foreach (var linha in verificacao)
            {
                lstbVerificarVez.Items.Add(linha);
                var partes = linha.Split(',');
                if (partes.Length == 2)
                {
                    _availableLetters.Remove(partes[1]);
                    linhasFormatadas.Add(linha);
                }
            }

            _resultadoFinal = string.Join("$", linhasFormatadas);
            lblMostraVez.Text = verificacao.FirstOrDefault()?.Split(',')[0] ?? string.Empty;

            
            


        }

        private void tmrVez_Tick(object sender, EventArgs e)
        {

            tmrVez.Enabled = false;
            try
            {
                VerificarVez();
                robo();
                VerificarVez();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no timer: " + ex.Message);
            }
            finally
            {
                tmrVez.Enabled = true;
            }
        }

        private void robo()
        {
            string fase = ReturnFaseJogo(_idSala);
            //MessageBox.Show(fase);

            if (fase == "S"&& lblMostraID.Text == lblMostraVez.Text && _availableLetters.Any())
            {
                

                var person = _availableLetters[new Random().Next(_availableLetters.Count)];
                var nivel = new Random().Next(2, 4);
                ColocarPersonagem(nivel, person);
                _playerCards.Add(person);

              

            }
            else if (fase == "P" && lblMostraID.Text == lblMostraVez.Text && _playerCards.Any())
            {
                
                var person = _playerCards[new Random().Next(_playerCards.Count)];
                Jogo.Promover(IdJogador, _senhaDoJogador, person);
            }
            else if (fase == "V" && lblMostraID.Text == lblMostraVez.Text)
            {
                
                string voto = votar();

                Jogo.Votar(IdJogador, _senhaDoJogador, voto);
            }
        }

        static string votar()
        {
            int voto = new Random().Next(1, 5);
            if (voto % 2 == 0) {
                return "N";
            }else if (voto % 2 != 0) { 
                return "S"; 
            }
            else
            
             return "F";




            }

            static string ReturnFaseJogo(int Id)
        {
            var firstLine = Jogo.VerificarVez(Id).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            var partes = firstLine?.Split(',') ?? new string[0];
            return (partes.Length >= 4 ? partes[3] : "F").ToUpper().Trim();
        }

        private void btnVerMapa_Click(object sender, EventArgs e)
        {
            new Mapa(_resultadoFinal).ShowDialog();
        }

        private void btnVotar_Click(object sender, EventArgs e)
        {
            int jogador = IdJogador;
            Jogo.Votar(jogador, _senhaDoJogador, txtVoto.Text);
        }

        private void btnPromover_Click(object sender, EventArgs e)
        {
            promover();
        }

        private void promover()
        {
            int jogador = IdJogador;
            Jogo.Promover(jogador, _senhaDoJogador, txtEscolhaPersonagem.Text);
        }

        private void lblColocarSetor_Click(object sender, EventArgs e)
        {
        }

        private void lblEscolhaPersonagem_Click(object sender, EventArgs e)
        {
        }

        private void lstbSetores_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstbVerificarVez_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fase = ReturnFaseJogo(_idSala);
            MessageBox.Show(fase);
        }
    }
}
