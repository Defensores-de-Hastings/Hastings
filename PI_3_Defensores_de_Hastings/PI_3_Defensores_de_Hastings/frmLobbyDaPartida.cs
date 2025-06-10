using KingMeServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

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

            Console.WriteLine("=== INICIALIZAÇÃO DO FORMULÁRIO ===");
            Console.WriteLine($"ID Sala: {_idSala}");
            Console.WriteLine($"ID Jogador: {idJogador}");
            Console.WriteLine($"Senha: {senhaJogador}");
            Console.WriteLine($"Letras iniciais: {string.Join(", ", _initialLetters)}");
            Console.WriteLine($"Timer antes: {tmrVez.Enabled}");
            
            tmrVez.Enabled = true;
            
            Console.WriteLine($"Timer depois: {tmrVez.Enabled}");
            Console.WriteLine("Formulário inicializado com sucesso");
        }

        private void frmLobbyDaPartida_Load(object sender, EventArgs e)
        {
            try
            {
                Jogo.Iniciar(IdJogador, _senhaDoJogador);
                tmrVez.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao iniciar jogo: {ex.Message}");
            }
        }

        private void ExibirCartas()
        {
            Console.WriteLine("Exibindo cartas...");
            var cartas = Jogo.ListarCartas(IdJogador, _senhaDoJogador);
            Console.WriteLine($"Cartas recebidas: {cartas}");
            txtCartas.Text = cartas;
            var dict = GetPersonagensDict();
            TXBCartas.Items.Clear();
            _playerCards.Clear(); // Limpar antes de adicionar
            
            foreach (var letra in cartas)
            {
                var key = letra.ToString();
                TXBCartas.Items.Add(dict.ContainsKey(key) ? dict[key] : key);
                if (!_playerCards.Contains(key)) // Evitar duplicatas
                {
                    _playerCards.Add(key);
                }
            }
            Console.WriteLine($"Cartas do jogador: {string.Join(", ", _playerCards)}");
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
            try
            {
                if (Jogo.EhMinhaVez())
                {
                    // Atualiza a interface
                    ExibirCartas();
                    ExibirPersonagens();
                    ExibirSetores();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no timer: {ex.Message}");
            }
        }

        private string GetCurrentGameState()
        {
            return $"Fase: {Jogo.FaseAtual}, Turno: {Jogo.TurnoAtual}, " +
                   $"Letras disponíveis: {string.Join(", ", _availableLetters)}, " +
                   $"Cartas do jogador: {string.Join(", ", _playerCards)}";
        }

        private void btnVerMapa_Click(object sender, EventArgs e)
        {
            var frmMapa = new frmMapa();
            frmMapa.Show();
        }

        private void btnVotar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtVoto.Text, out int jogador))
            {
                Jogo.Votar(jogador, _senhaDoJogador, txtVoto.Text);
            }
            else
            {
                MessageBox.Show("Jogador inválido.");
            }
        }

        private void btnPromover_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtPromover.Text, out int jogador))
            {
                Jogo.Promover(jogador, _senhaDoJogador, txtEscolhaPersonagem.Text);
            }
            else
            {
                MessageBox.Show("Jogador inválido.");
            }
        }

        private void lblColocarSetor_Click(object sender, EventArgs e)
        {
            txtEscolheSetor.Focus();
        }

        private void lblEscolhaPersonagem_Click(object sender, EventArgs e)
        {
            txtEscolhaPersonagem.Focus();
        }

        private void lstbSetores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbSetores.SelectedItem != null)
                txtEscolheSetor.Text = lstbSetores.SelectedItem.ToString();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstbVerificarVez_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbVerificarVez.SelectedItem != null)
                txtVoto.Text = lstbVerificarVez.SelectedItem.ToString();
        }
    }
}
