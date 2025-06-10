using System;
using System.Threading.Tasks;

namespace PI_3_Defensores_de_Hastings
{
    public class LlamaService
    {
        public LlamaService()
        {
        }

        public async Task<string> GetDecision(string gameState)
        {
            // Implementação básica - retorna uma decisão padrão
            return "CONTINUE";
        }

        public async Task<bool> IsAvailable()
        {
            // Implementação básica - retorna true para indicar que o serviço está disponível
            return true;
        }
    }
} 