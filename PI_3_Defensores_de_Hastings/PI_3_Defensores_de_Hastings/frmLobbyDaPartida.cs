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
        private readonly AutonomousAgent _autonomousAgent;
        
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
            _autonomousAgent = new AutonomousAgent();

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
                Task.Run(async () => await TestarAgenteInicial());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao iniciar jogo: {ex.Message}");
            }
        }

        private async Task TestarAgenteInicial()
        {
            try
            {
                await ExecutarAgente();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no teste inicial: {ex.Message}");
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

        private async void tmrVez_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Jogo.EhMinhaVez())
                {
                    await ExecutarAgente();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no timer: {ex.Message}");
            }
        }

        private async Task ExecutarAgente()
        {
            try
            {
                var fase = Jogo.FaseAtual;
                var turno = Jogo.TurnoAtual;

                var decision = _autonomousAgent.MakeDecision(fase, turno, _availableLetters, _playerCards);
                ExecuteDecision(decision, fase);
            }
            catch (Exception ex)
            {
                ExecuteFallbackStrategy(Jogo.FaseAtual);
            }
        }

        private void ExecuteDecision(string decision, string fase)
        {
            if (string.IsNullOrEmpty(decision))
            {
                ExecuteFallbackStrategy(fase);
                return;
            }

            switch (fase.ToUpper())
            {
                case "S":
                    ExecuteSelectionDecision(decision);
                    break;
                case "P":
                    ExecutePromotionDecision(decision);
                    break;
                case "V":
                    ExecuteVotingDecision(decision);
                    break;
                default:
                    ExecuteFallbackStrategy(fase);
                    break;
            }
        }

        private void ExecuteSelectionDecision(string decision)
        {
            try
            {
                var parts = decision.Split(',');
                var sector = int.Parse(parts[0].Split(':')[1].Trim());
                var character = parts[1].Split(':')[1].Trim();
                var level = int.Parse(parts[2].Split(':')[1].Trim());

                Jogo.SelecionarPersonagem(character, sector, level);
                _availableLetters.Remove(character);
            }
            catch
            {
                ExecuteFallbackSelection();
            }
        }

        private void ExecutePromotionDecision(string decision)
        {
            try
            {
                var character = decision.Split(':')[1].Trim();
                Jogo.PromoverPersonagem(character);
            }
            catch
            {
                ExecuteFallbackPromotion();
            }
        }

        private void ExecuteVotingDecision(string decision)
        {
            try
            {
                var vote = decision.Split(':')[1].Trim();
                Jogo.Votar(vote);
            }
            catch
            {
                ExecuteFallbackVoting();
            }
        }

        private void ExecuteFallbackStrategy(string fase)
        {
            switch (fase.ToUpper())
            {
                case "S":
                    ExecuteFallbackSelection();
                    break;
                case "P":
                    ExecuteFallbackPromotion();
                    break;
                case "V":
                    ExecuteFallbackVoting();
                    break;
            }
        }

        private void ExecuteFallbackSelection()
        {
            try
            {
                var character = _availableLetters.FirstOrDefault();
                if (character != null)
                {
                    var sector = DetermineSector(Jogo.TurnoAtual);
                    Jogo.SelecionarPersonagem(character, sector, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na seleção de fallback: {ex.Message}");
            }
        }

        private int DetermineSector(int turno)
        {
            if (turno <= 3) return 4;
            if (turno <= 7) return 3;
            if (turno <= 12) return 2;
            return 1;
        }

        private void ExecuteFallbackPromotion()
        {
            try
            {
                var character = _availableLetters.FirstOrDefault();
                if (character != null)
                {
                    Jogo.PromoverPersonagem(character);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na promoção de fallback: {ex.Message}");
            }
        }

        private void ExecuteFallbackVoting()
        {
            try
            {
                Jogo.Votar("S");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na votação de fallback: {ex.Message}");
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

        private void btnMostrarEstrategia_Click(object sender, EventArgs e)
        {
            var estado = GetCurrentGameState();
            var fase = Jogo.FaseAtual;
            var turno = Jogo.TurnoAtual;

            var decision = _autonomousAgent.MakeDecision(fase, turno, _availableLetters, _playerCards);
            var mensagem = $"📊 ESTADO ATUAL DO JOGO:\n\n" +
                          $"Estado: {estado}\n" +
                          $"Decisão do agente: {decision}";

            MessageBox.Show(mensagem, "Estratégia do Agente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnVerificarEstado_Click(object sender, EventArgs e)
        {
            var estado = GetCurrentGameState();
            MessageBox.Show(estado, "Estado do Jogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
