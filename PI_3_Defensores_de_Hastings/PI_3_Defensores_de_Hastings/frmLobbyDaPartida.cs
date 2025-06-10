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
        private readonly LlamaService _llamaService;
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
            _llamaService = new LlamaService();
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
                timer1.Start();
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

        private async void timer1_Tick(object sender, EventArgs e)
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
                var gameState = GetCurrentGameState();
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
                ExecuteFallbackStrategy("S");
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
                ExecuteFallbackStrategy("P");
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
                ExecuteFallbackStrategy("V");
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
            var sector = DetermineSector(Jogo.TurnoAtual);
            var character = _availableLetters[new Random().Next(_availableLetters.Count)];
            var level = new Random().Next(1, 6);

            Jogo.SelecionarPersonagem(character, sector, level);
            _availableLetters.Remove(character);
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
            var character = _availableLetters[new Random().Next(_availableLetters.Count)];
            Jogo.PromoverPersonagem(character);
        }

        private void ExecuteFallbackVoting()
        {
            var vote = new Random().Next(100) < 60 ? "S" : "N";
            Jogo.Votar(vote);
        }

        private string GetCurrentGameState()
        {
            return $"Fase: {Jogo.FaseAtual}, Turno: {Jogo.TurnoAtual}, " +
                   $"Letras: {string.Join(",", _availableLetters)}, " +
                   $"Cartas: {string.Join(",", _playerCards)}";
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
            string fase = Jogo.FaseAtual;
            MessageBox.Show(fase);
        }

        private async void btnTestarLlama_Click(object sender, EventArgs e)
        {
            try
            {
                btnTestarLlama.Enabled = false;
                btnTestarLlama.Text = "Testando...";
                
                var conectividade = await LlamaTest.TestarConectividade();
                
                if (conectividade)
                {
                    await LlamaTest.MostrarTesteDecisao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao testar Llama: {ex.Message}", 
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTestarLlama.Enabled = true;
                btnTestarLlama.Text = "Testar IA";
            }
        }

        private void btnMostrarEstrategia_Click(object sender, EventArgs e)
        {
            var estrategia = @"
🤖 ESTRATÉGIA DO AGENTE AUTÔNOMO

📊 Estratégia por Fase:

1. Seleção (S):
   - Posicionamento estratégico por turno
   - Escolha de personagens por prioridade
   - Níveis ajustados ao setor

2. Promoção (P):
   - Análise de personagens disponíveis
   - Escolha baseada em prioridades
   - Consideração de cartas

3. Votação (V):
   - Decisão baseada no turno
   - Probabilidade ajustada
   - Estratégia de finalização

🛡️ Fallback: Estratégia básica se necessário";

            MessageBox.Show(estrategia, "Estratégia do Agente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnDebugBot_Click(object sender, EventArgs e)
        {
            try
            {
                btnDebugBot.Enabled = false;
                btnDebugBot.Text = "Testando...";
                
                Console.WriteLine("=== TESTE MANUAL DO BOT ===");
                
                // Verificar estado atual
                VerificarVez();
                string fase = Jogo.FaseAtual;
                
                Console.WriteLine($"Estado atual - Fase: {fase}");
                Console.WriteLine($"ID Jogador: {lblMostraID.Text}");
                Console.WriteLine($"Vez de quem: {lblMostraVez.Text}");
                Console.WriteLine($"É minha vez: {lblMostraID.Text == lblMostraVez.Text}");
                
                if (lblMostraID.Text == lblMostraVez.Text)
                {
                    Console.WriteLine("Executando bot...");
                    await ExecutarAgente();
                }
                else
                {
                    Console.WriteLine("Não é minha vez ainda");
                    MessageBox.Show("Não é sua vez ainda. Aguarde sua vez para o bot jogar automaticamente.", 
                        "Debug Bot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no debug: {ex.Message}");
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDebugBot.Enabled = true;
                btnDebugBot.Text = "Debug Bot";
            }
        }

        private void btnVerificarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                VerificarVez();
                string fase = Jogo.FaseAtual;
                
                var estado = $@"
📊 ESTADO ATUAL DO JOGO:

🆔 ID do Jogador: {lblMostraID.Text}
🔄 Vez de quem: {lblMostraVez.Text}
✅ É minha vez: {lblMostraID.Text == lblMostraVez.Text}
🎯 Fase atual: {fase}
📝 Letras disponíveis: {string.Join(", ", _availableLetters)}
🃏 Minhas cartas: {string.Join(", ", _playerCards)}
🔢 Contador: {Contador}
⏰ Timer ativo: {tmrVez.Enabled}";

                MessageBox.Show(estado, "Estado do Jogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar estado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
