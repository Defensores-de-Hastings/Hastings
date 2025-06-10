using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI_3_Defensores_de_Hastings
{
    public static class LlamaTest
    {
        public static async Task<bool> TestarConectividade()
        {
            try
            {
                var llamaService = new LlamaService();
                
                // Teste simples
                var decision = await llamaService.GetStrategicDecision(
                    "Teste de conectividade", 
                    "S", 
                    new List<string> { "A", "B", "C" }, 
                    new List<string> { "D", "E" }, 
                    1);

                return !string.IsNullOrEmpty(decision) && decision != "ERRO";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao testar conectividade com Llama: {ex.Message}", 
                    "Erro de Conectividade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public static async Task<string> TestarDecisao(string fase, List<string> availableLetters, List<string> playerCards, int turno)
        {
            try
            {
                var llamaService = new LlamaService();
                var gameState = $"Teste - Fase {fase}, Turno {turno}";
                
                var decision = await llamaService.GetStrategicDecision(
                    gameState, 
                    fase, 
                    availableLetters, 
                    playerCards, 
                    turno);

                return decision;
            }
            catch (Exception ex)
            {
                return $"ERRO: {ex.Message}";
            }
        }

        public static void MostrarTesteConectividade()
        {
            var result = Task.Run(async () => await TestarConectividade()).Result;
            
            if (result)
            {
                MessageBox.Show("✅ Conectividade com Llama OK!\nO bot inteligente está funcionando.", 
                    "Teste de Conectividade", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("❌ Erro na conectividade com Llama.\nO bot usará a estratégia original como fallback.", 
                    "Teste de Conectividade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static async Task MostrarTesteDecisao()
        {
            var testCases = new[]
            {
                new { Fase = "S", Letters = new List<string> { "A", "B", "C" }, Cards = new List<string>(), Turno = 1 },
                new { Fase = "P", Letters = new List<string>(), Cards = new List<string> { "D", "E" }, Turno = 5 },
                new { Fase = "V", Letters = new List<string>(), Cards = new List<string>(), Turno = 10 }
            };

            var results = new List<string>();

            foreach (var testCase in testCases)
            {
                var decision = await TestarDecisao(testCase.Fase, testCase.Letters, testCase.Cards, testCase.Turno);
                results.Add($"Fase {testCase.Fase}: {decision}");
            }

            var message = string.Join("\n", results);
            MessageBox.Show($"Teste de Decisões do Llama:\n\n{message}", 
                "Teste de Decisões", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
} 