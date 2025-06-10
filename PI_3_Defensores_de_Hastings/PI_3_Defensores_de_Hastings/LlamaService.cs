using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PI_3_Defensores_de_Hastings
{
    public class LlamaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://dns.auditto.com.br:11434";

        public LlamaService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> GetStrategicDecision(string gameState, string phase, List<string> availableLetters, List<string> playerCards, int currentTurn)
        {
            var prompt = BuildStrategicPrompt(gameState, phase, availableLetters, playerCards, currentTurn);
            
            Console.WriteLine("🤖 === CONSULTA À IA LLAMA ===");
            Console.WriteLine($"📤 Enviando prompt para Llama...");
            Console.WriteLine($"🎯 Fase: {phase}");
            Console.WriteLine($"🔄 Turno: {currentTurn}");
            Console.WriteLine($"📝 Letras disponíveis: {string.Join(", ", availableLetters)}");
            Console.WriteLine($"🃏 Cartas do jogador: {string.Join(", ", playerCards)}");
            Console.WriteLine($"📊 Estado do jogo: {gameState}");
            
            var request = new
            {
                model = "llama3:latest",
                prompt = prompt,
                stream = false,
                options = new
                {
                    temperature = 0.5,
                    top_p = 0.7,
                    max_tokens = 200
                }
            };

            try
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"🌐 Conectando com: {_baseUrl}/api/generate");
                Console.WriteLine($"📤 Prompt enviado:");
                Console.WriteLine('=' * 50);
                Console.WriteLine(prompt);
                Console.WriteLine('=' * 50);
                
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/generate", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"📥 Resposta recebida (Status: {response.StatusCode}):");
                Console.WriteLine('=' * 50);
                Console.WriteLine(responseContent);
                Console.WriteLine('=' * 50);
                
                if (response.IsSuccessStatusCode)
                {
                    var llamaResponse = JsonConvert.DeserializeObject<LlamaResponse>(responseContent);
                    var parsedDecision = ParseLlamaResponse(llamaResponse.Response, phase);
                    
                    Console.WriteLine($"🧠 Resposta da IA: {llamaResponse.Response}");
                    Console.WriteLine($"🔧 Decisão processada: {parsedDecision}");
                    Console.WriteLine("✅ Consulta à IA concluída com sucesso");
                    
                    return parsedDecision;
                }
                else
                {
                    Console.WriteLine($"❌ Erro na comunicação com Llama: {response.StatusCode}");
                    Console.WriteLine($"📄 Conteúdo do erro: {responseContent}");
                    var fallback = GetFallbackDecision(phase, availableLetters, playerCards);
                    Console.WriteLine($"🔄 Usando fallback: {fallback}");
                    return fallback;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Erro ao comunicar com Llama: {ex.Message}");
                Console.WriteLine($"📋 Stack trace: {ex.StackTrace}");
                var fallback = GetFallbackDecision(phase, availableLetters, playerCards);
                Console.WriteLine($"🔄 Usando fallback: {fallback}");
                return fallback;
            }
        }

        private string BuildStrategicPrompt(string gameState, string phase, List<string> availableLetters, List<string> playerCards, int currentTurn)
        {
            var prompt = $@"
Você é um jogador estratégico do jogo King Me! Analise a situação atual e tome a melhor decisão.

ESTADO ATUAL DO JOGO:
- Fase: {phase}
- Turno atual: {currentTurn}
- Letras disponíveis: {string.Join(", ", availableLetters)}
- Suas cartas: {string.Join(", ", playerCards)}
- Estado do jogo: {gameState}

ESTRATÉGIA BASEADA NA FASE:

FASE S (Seleção):
- Objetivo: Posicionar personagens estrategicamente
- Setores: 1 (mais próximo do trono) a 4 (mais distante)
- Estratégia: 
  * Primeiros turnos (0-3): Foque no setor 4 para criar base sólida
  * Turnos médios (4-7): Avance para setor 3
  * Turnos finais (8-12): Posicione no setor 2
  * Últimos turnos (12-16): Atinja setor 1 (próximo ao trono)
- Escolha personagens que complementem sua estratégia

FASE P (Promoção):
- Objetivo: Promover personagens para ganhar poder
- Estratégia: Promova personagens em posições estratégicas ou que tenham maior potencial

FASE V (Votação):
- Objetivo: Votar para influenciar o resultado
- Opções: S (Sim), N (Não), F (Fim)
- Estratégia: Vote baseado na posição atual e objetivos

RESPONDA APENAS COM:
- Para Fase S: 'SETOR: [número], PERSONAGEM: [letra], NIVEL: [1-5]'
- Para Fase P: 'PROMOVER: [letra]'
- Para Fase V: 'VOTO: [S/N/F]'

Exemplo de resposta para Fase S: 'SETOR: 3, PERSONAGEM: A, NIVEL: 4'
Exemplo de resposta para Fase P: 'PROMOVER: B'
Exemplo de resposta para Fase V: 'VOTO: S'

Tome a decisão mais estratégica baseada no contexto atual:";

            return prompt;
        }

        private string ParseLlamaResponse(string response, string phase)
        {
            Console.WriteLine($"🔍 === PROCESSANDO RESPOSTA DA IA ===");
            Console.WriteLine($"📄 Resposta bruta: {response}");
            Console.WriteLine($"🎯 Fase: {phase}");
            
            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine("❌ Resposta vazia - usando fallback");
                return GetFallbackDecision(phase, new List<string>(), new List<string>());
            }

            response = response.Trim().ToUpper();
            Console.WriteLine($"🧹 Resposta limpa: {response}");

            switch (phase)
            {
                case "S":
                    Console.WriteLine("🎯 Processando Fase S (Seleção)");
                    // Extrair setor, personagem e nível
                    var setorMatch = System.Text.RegularExpressions.Regex.Match(response, @"SETOR:\s*(\d+)");
                    var personagemMatch = System.Text.RegularExpressions.Regex.Match(response, @"PERSONAGEM:\s*([A-Z])");
                    var nivelMatch = System.Text.RegularExpressions.Regex.Match(response, @"NIVEL:\s*(\d+)");

                    Console.WriteLine($"🔍 Setor encontrado: {setorMatch.Success} - {setorMatch.Groups[1].Value}");
                    Console.WriteLine($"🔍 Personagem encontrado: {personagemMatch.Success} - {personagemMatch.Groups[1].Value}");
                    Console.WriteLine($"🔍 Nível encontrado: {nivelMatch.Success} - {nivelMatch.Groups[1].Value}");

                    if (setorMatch.Success && personagemMatch.Success && nivelMatch.Success)
                    {
                        var result = $"SETOR:{setorMatch.Groups[1].Value},PERSONAGEM:{personagemMatch.Groups[1].Value},NIVEL:{nivelMatch.Groups[1].Value}";
                        Console.WriteLine($"✅ Decisão válida para Fase S: {result}");
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("❌ Formato inválido para Fase S - usando fallback");
                    }
                    break;

                case "P":
                    Console.WriteLine("🎯 Processando Fase P (Promoção)");
                    var promoverMatch = System.Text.RegularExpressions.Regex.Match(response, @"PROMOVER:\s*([A-Z])");
                    Console.WriteLine($"🔍 Promoção encontrada: {promoverMatch.Success} - {promoverMatch.Groups[1].Value}");
                    
                    if (promoverMatch.Success)
                    {
                        var result = $"PROMOVER:{promoverMatch.Groups[1].Value}";
                        Console.WriteLine($"✅ Decisão válida para Fase P: {result}");
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("❌ Formato inválido para Fase P - usando fallback");
                    }
                    break;

                case "V":
                    Console.WriteLine("🎯 Processando Fase V (Votação)");
                    var votoMatch = System.Text.RegularExpressions.Regex.Match(response, @"VOTO:\s*([SNF])");
                    Console.WriteLine($"🔍 Voto encontrado: {votoMatch.Success} - {votoMatch.Groups[1].Value}");
                    
                    if (votoMatch.Success)
                    {
                        var result = $"VOTO:{votoMatch.Groups[1].Value}";
                        Console.WriteLine($"✅ Decisão válida para Fase V: {result}");
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("❌ Formato inválido para Fase V - usando fallback");
                    }
                    break;
                    
                default:
                    Console.WriteLine($"❌ Fase desconhecida: {phase} - usando fallback");
                    break;
            }

            var fallback = GetFallbackDecision(phase, new List<string>(), new List<string>());
            Console.WriteLine($"🔄 Usando fallback: {fallback}");
            return fallback;
        }

        private string GetFallbackDecision(string phase, List<string> availableLetters, List<string> playerCards)
        {
            Console.WriteLine($"🔄 === USANDO FALLBACK ===");
            Console.WriteLine($"🎯 Fase: {phase}");
            Console.WriteLine($"📝 Letras disponíveis: {string.Join(", ", availableLetters)}");
            Console.WriteLine($"🃏 Cartas do jogador: {string.Join(", ", playerCards)}");
            
            var random = new Random();

            switch (phase)
            {
                case "S":
                    if (availableLetters.Count > 0)
                    {
                        var setor = random.Next(1, 5);
                        var personagem = availableLetters[random.Next(availableLetters.Count)];
                        var nivel = random.Next(1, 6);
                        var result = $"SETOR:{setor},PERSONAGEM:{personagem},NIVEL:{nivel}";
                        Console.WriteLine($"🎲 Fallback Fase S: {result}");
                        return result;
                    }
                    break;

                case "P":
                    if (playerCards.Count > 0)
                    {
                        var personagem = playerCards[random.Next(playerCards.Count)];
                        var result = $"PROMOVER:{personagem}";
                        Console.WriteLine($"🎲 Fallback Fase P: {result}");
                        return result;
                    }
                    break;

                case "V":
                    var votos = new[] { "S", "N", "F" };
                    var voto = votos[random.Next(votos.Length)];
                    var resultVoto = $"VOTO:{voto}";
                    Console.WriteLine($"🎲 Fallback Fase V: {resultVoto}");
                    return resultVoto;
                    break;
            }

            Console.WriteLine("❌ Fallback falhou - retornando ERRO");
            return "ERRO";
        }
    }

    public class LlamaResponse
    {
        [JsonProperty("response")]
        public string Response { get; set; }
    }
} 