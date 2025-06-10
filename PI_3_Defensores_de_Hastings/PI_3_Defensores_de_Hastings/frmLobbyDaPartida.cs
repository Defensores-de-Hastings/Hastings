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
                
                // Garantir que o timer está ativo
                if (!tmrVez.Enabled)
                {
                    tmrVez.Enabled = true;
                    Console.WriteLine("Timer iniciado após começar jogo");
                }
                
                // Teste inicial da IA
                Task.Run(async () => await TestarIAInicial());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }

        private async Task TestarIAInicial()
        {
            try
            {
                await Task.Delay(2000); // Aguarda 2 segundos
                Console.WriteLine("Testando IA inicial...");
                await roboInteligente();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no teste inicial: {ex.Message}");
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
            tmrVez.Enabled = false;
            try
            {
                VerificarVez();
                await roboInteligente();
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

        private async Task roboInteligente()
        {
            string fase = ReturnFaseJogo(_idSala);
            Console.WriteLine($"=== BOT INTELIGENTE ATIVADO ===");
            Console.WriteLine($"Fase atual: {fase}");
            Console.WriteLine($"ID do jogador: {lblMostraID.Text}");
            Console.WriteLine($"Vez de quem: {lblMostraVez.Text}");
            Console.WriteLine($"É minha vez? {lblMostraID.Text == lblMostraVez.Text}");
            Console.WriteLine($"Letras disponíveis: {string.Join(", ", _availableLetters)}");
            Console.WriteLine($"Minhas cartas: {string.Join(", ", _playerCards)}");
            Console.WriteLine($"Contador: {Contador}");
            
            if (lblMostraID.Text != lblMostraVez.Text)
            {
                Console.WriteLine("Não é minha vez - saindo");
                return;
            }

            try
            {
                Console.WriteLine("Consultando Llama...");
                var gameState = GetCurrentGameState();
                Console.WriteLine($"Estado do jogo: {gameState}");
                
                var decision = await _llamaService.GetStrategicDecision(gameState, fase, _availableLetters, _playerCards, Contador);
                Console.WriteLine($"Decisão do Llama: {decision}");
                
                ExecuteLlamaDecision(decision, fase);
                Console.WriteLine("Decisão executada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no bot inteligente: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                // Fallback para estratégia original
                Console.WriteLine("Usando estratégia original como fallback");
                roboOriginal();
            }
        }

        private void ExecuteLlamaDecision(string decision, string fase)
        {
            Console.WriteLine($"🎯 === EXECUTANDO DECISÃO DA IA ===");
            Console.WriteLine($"📋 Decisão recebida: {decision}");
            Console.WriteLine($"🎮 Fase: {fase}");
            
            if (string.IsNullOrEmpty(decision) || decision == "ERRO")
            {
                Console.WriteLine("❌ Decisão inválida - usando estratégia original");
                roboOriginal();
                return;
            }

            switch (fase)
            {
                case "S":
                    Console.WriteLine("🎯 Executando decisão de Seleção");
                    ExecuteSelectionDecision(decision);
                    break;
                case "P":
                    Console.WriteLine("🎯 Executando decisão de Promoção");
                    ExecutePromotionDecision(decision);
                    break;
                case "V":
                    Console.WriteLine("🎯 Executando decisão de Votação");
                    ExecuteVotingDecision(decision);
                    break;
                default:
                    Console.WriteLine($"❌ Fase desconhecida: {fase} - usando estratégia original");
                    roboOriginal();
                    break;
            }
        }

        private void ExecuteSelectionDecision(string decision)
        {
            Console.WriteLine($"🎯 === EXECUTANDO SELEÇÃO ===");
            Console.WriteLine($"📋 Decisão: {decision}");
            
            if (!_availableLetters.Any()) 
            {
                Console.WriteLine("❌ Nenhuma letra disponível");
                return;
            }

            var parts = decision.Split(',');
            int setor = 1;
            string personagem = "";
            int nivel = 1;

            Console.WriteLine($"🔍 Analisando partes da decisão: {string.Join(" | ", parts)}");

            foreach (var part in parts)
            {
                var keyValue = part.Split(':');
                if (keyValue.Length == 2)
                {
                    var key = keyValue[0].Trim();
                    var value = keyValue[1].Trim();

                    Console.WriteLine($"🔍 Processando: {key} = {value}");

                    switch (key)
                    {
                        case "SETOR":
                            if (int.TryParse(value, out int s) && s >= 1 && s <= 4)
                            {
                                setor = s;
                                Console.WriteLine($"✅ Setor válido: {setor}");
                            }
                            else
                            {
                                Console.WriteLine($"❌ Setor inválido: {value}");
                            }
                            break;
                        case "PERSONAGEM":
                            if (_availableLetters.Contains(value))
                            {
                                personagem = value;
                                Console.WriteLine($"✅ Personagem válido: {personagem}");
                            }
                            else
                            {
                                Console.WriteLine($"❌ Personagem não disponível: {value}");
                            }
                            break;
                        case "NIVEL":
                            if (int.TryParse(value, out int n) && n >= 1 && n <= 5)
                            {
                                nivel = n;
                                Console.WriteLine($"✅ Nível válido: {nivel}");
                            }
                            else
                            {
                                Console.WriteLine($"❌ Nível inválido: {value}");
                            }
                            break;
                    }
                }
            }

            // Validações
            if (string.IsNullOrEmpty(personagem) || !_availableLetters.Contains(personagem))
            {
                personagem = _availableLetters[new Random().Next(_availableLetters.Count)];
                Console.WriteLine($"🔄 Personagem corrigido para: {personagem}");
            }

            // Aplicar estratégia de setor baseada no contador
            var setorOriginal = setor;
            setor = GetStrategicSetor(Contador);
            Console.WriteLine($"🎯 Setor ajustado de {setorOriginal} para {setor} (baseado no contador {Contador})");

            Console.WriteLine($"🚀 Executando ação: ColocarPersonagem({setor}, {personagem})");
            ColocarPersonagem(setor, personagem);
            _playerCards.Add(personagem);
            _availableLetters.Remove(personagem);
            Contador++;
            
            Console.WriteLine($"✅ Seleção concluída - Contador: {Contador}");
        }

        private void ExecutePromotionDecision(string decision)
        {
            Console.WriteLine($"🎯 === EXECUTANDO PROMOÇÃO ===");
            Console.WriteLine($"📋 Decisão: {decision}");
            
            if (!_playerCards.Any()) 
            {
                Console.WriteLine("❌ Nenhuma carta disponível");
                return;
            }

            var parts = decision.Split(',');
            string personagem = "";

            Console.WriteLine($"🔍 Analisando partes da decisão: {string.Join(" | ", parts)}");

            foreach (var part in parts)
            {
                var keyValue = part.Split(':');
                if (keyValue.Length == 2 && keyValue[0].Trim() == "PROMOVER")
                {
                    var value = keyValue[1].Trim();
                    Console.WriteLine($"🔍 Processando PROMOVER: {value}");
                    
                    if (_playerCards.Contains(value))
                    {
                        personagem = value;
                        Console.WriteLine($"✅ Personagem válido para promoção: {personagem}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"❌ Personagem não disponível: {value}");
                    }
                }
            }

            if (string.IsNullOrEmpty(personagem))
            {
                personagem = _playerCards[new Random().Next(_playerCards.Count)];
                Console.WriteLine($"🔄 Personagem escolhido aleatoriamente: {personagem}");
            }

            Console.WriteLine($"🚀 Executando ação: Jogo.Promover({IdJogador}, {_senhaDoJogador}, {personagem})");
            Jogo.Promover(IdJogador, _senhaDoJogador, personagem);
            Console.WriteLine($"✅ Promoção concluída: {personagem}");
        }

        private void ExecuteVotingDecision(string decision)
        {
            Console.WriteLine($"🎯 === EXECUTANDO VOTAÇÃO ===");
            Console.WriteLine($"📋 Decisão: {decision}");
            
            var parts = decision.Split(',');
            string voto = "";

            Console.WriteLine($"🔍 Analisando partes da decisão: {string.Join(" | ", parts)}");

            foreach (var part in parts)
            {
                var keyValue = part.Split(':');
                if (keyValue.Length == 2 && keyValue[0].Trim() == "VOTO")
                {
                    var value = keyValue[1].Trim();
                    Console.WriteLine($"🔍 Processando VOTO: {value}");
                    
                    if (new[] { "S", "N", "F" }.Contains(value))
                    {
                        voto = value;
                        Console.WriteLine($"✅ Voto válido: {voto}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"❌ Voto inválido: {value}");
                    }
                }
            }

            if (string.IsNullOrEmpty(voto))
            {
                voto = votar();
                Console.WriteLine($"🔄 Voto escolhido aleatoriamente: {voto}");
            }

            Console.WriteLine($"🚀 Executando ação: Jogo.Votar({IdJogador}, {_senhaDoJogador}, {voto})");
            Jogo.Votar(IdJogador, _senhaDoJogador, voto);
            Console.WriteLine($"✅ Votação concluída: {voto}");
        }

        private int GetStrategicSetor(int contador)
        {
            // Estratégia baseada no progresso do jogo
            if (contador >= 0 && contador <= 3)
                return 4; // Base sólida
            else if (contador >= 4 && contador <= 7)
                return 3; // Avanço médio
            else if (contador >= 8 && contador <= 12)
                return 2; // Posição estratégica
            else if (contador >= 12 && contador <= 16)
                return 1; // Próximo ao trono
            else
                return 1; // Default
        }

        private string GetCurrentGameState()
        {
            try
            {
                var estado = Jogo.VerificarVez(_idSala);
                return estado.Length > 100 ? estado.Substring(0, 100) + "..." : estado;
            }
            catch
            {
                return "Estado do jogo não disponível";
            }
        }

        // Estratégia original como fallback
        private void roboOriginal()
        {
            Console.WriteLine("=== BOT ORIGINAL ATIVADO ===");
            string fase = ReturnFaseJogo(_idSala);
            Console.WriteLine($"Fase: {fase}");

            if (fase == "S" && lblMostraID.Text == lblMostraVez.Text && _availableLetters.Any())
            {
                Console.WriteLine("Executando estratégia de seleção");
                int setor = 0;

                if (Contador >= 0 && Contador <= 3)
                {
                    setor = 4;
                    Contador++;
                } else if (Contador >= 4 && Contador <= 7)
                {
                    setor = 3;
                    Contador++;
                }
                else if (Contador >= 8 && Contador <= 12)
                {
                    setor = 2;
                    Contador++;
                }
                else if (Contador >= 12 && Contador <= 16)
                {
                    setor = 1;
                    Contador++;
                }

                var person = _availableLetters[new Random().Next(_availableLetters.Count)];
                var nivel = new Random().Next(1, 5);
                Console.WriteLine($"Colocando personagem: {person} no setor {setor} nível {nivel}");
                ColocarPersonagem(setor, person);
                _playerCards.Add(person);
                Console.WriteLine("Personagem colocado com sucesso");
            }
            else if (fase == "P" && lblMostraID.Text == lblMostraVez.Text && _playerCards.Any())
            {
                Console.WriteLine("Executando estratégia de promoção");
                var person = _playerCards[new Random().Next(_playerCards.Count)];
                Console.WriteLine($"Promovendo personagem: {person}");
                Jogo.Promover(IdJogador, _senhaDoJogador, person);
                Console.WriteLine("Personagem promovido com sucesso");
            }
            else if (fase == "V" && lblMostraID.Text == lblMostraVez.Text)
            {
                Console.WriteLine("Executando estratégia de votação");
                string voto = votar();
                Console.WriteLine($"Votando: {voto}");
                Jogo.Votar(IdJogador, _senhaDoJogador, voto);
                Console.WriteLine("Voto registrado com sucesso");
            }
            else
            {
                Console.WriteLine($"Condições não atendidas - Fase: {fase}, É minha vez: {lblMostraID.Text == lblMostraVez.Text}, Letras disponíveis: {_availableLetters.Any()}, Cartas: {_playerCards.Any()}");
            }
        }

        static string votar()
        {
            int voto = new Random().Next(1, 3);

            if (voto == 1)
            {
                return "S";

            }
            else if (voto == 2)
            {
                return "N";
            }
            else
            {
                return "F";
            }
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
🤖 ESTRATÉGIA DO BOT INTELIGENTE

🔗 Conectado ao Llama: http://dns.auditto.com.br:11434

📊 Estratégia por Fase:

📍 FASE S (Seleção):
- Turnos 0-3: Setor 4 (base sólida)
- Turnos 4-7: Setor 3 (avanço médio)  
- Turnos 8-12: Setor 2 (posição estratégica)
- Turnos 12-16: Setor 1 (próximo ao trono)

⬆️ FASE P (Promoção):
- Analisa personagens disponíveis
- Escolhe com maior potencial estratégico

🗳️ FASE V (Votação):
- Analisa contexto do jogo
- Vota baseado em objetivos estratégicos

🛡️ Fallback: Se Llama falhar, usa estratégia original

✅ Bot está ativo e funcionando!";

            MessageBox.Show(estrategia, "Estratégia do Bot", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string fase = ReturnFaseJogo(_idSala);
                
                Console.WriteLine($"Estado atual - Fase: {fase}");
                Console.WriteLine($"ID Jogador: {lblMostraID.Text}");
                Console.WriteLine($"Vez de quem: {lblMostraVez.Text}");
                Console.WriteLine($"É minha vez: {lblMostraID.Text == lblMostraVez.Text}");
                
                if (lblMostraID.Text == lblMostraVez.Text)
                {
                    Console.WriteLine("Executando bot...");
                    await roboInteligente();
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
                string fase = ReturnFaseJogo(_idSala);
                
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
