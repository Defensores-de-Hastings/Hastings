using System;
using System.Collections.Generic;
using System.Linq;

namespace PI_3_Defensores_de_Hastings
{
    public class AutonomousAgent
    {
        private readonly Random _random;
        private readonly Dictionary<string, int> _characterPriorities;
        private readonly Dictionary<int, int> _sectorPriorities;

        public AutonomousAgent()
        {
            _random = new Random();
            _characterPriorities = new Dictionary<string, int>();
            _sectorPriorities = new Dictionary<int, int>();
            InitializePriorities();
        }

        private void InitializePriorities()
        {
            // Inicializa prioridades dos personagens
            _characterPriorities["A"] = 5;
            _characterPriorities["B"] = 4;
            _characterPriorities["C"] = 3;
            _characterPriorities["D"] = 2;
            _characterPriorities["E"] = 1;

            // Inicializa prioridades dos setores
            _sectorPriorities[1] = 4; // Próximo ao trono
            _sectorPriorities[2] = 3; // Posição estratégica
            _sectorPriorities[3] = 2; // Avanço médio
            _sectorPriorities[4] = 1; // Base sólida
        }

        public string MakeDecision(string fase, int turno, List<string> availableLetters, List<string> playerCards)
        {
            switch (fase.ToUpper())
            {
                case "S":
                    return MakeSelectionDecision(turno, availableLetters);
                case "P":
                    return MakePromotionDecision(availableLetters, playerCards);
                case "V":
                    return MakeVotingDecision(turno, playerCards);
                default:
                    throw new ArgumentException("Fase inválida");
            }
        }

        private string MakeSelectionDecision(int turno, List<string> availableLetters)
        {
            // Determina o setor baseado no turno
            int sector = DetermineSector(turno);
            
            // Escolhe o personagem com maior prioridade disponível
            string character = availableLetters
                .OrderByDescending(c => _characterPriorities[c])
                .First();

            // Determina o nível baseado na estratégia
            int level = DetermineLevel(turno, sector);

            return $"SETOR: {sector}, PERSONAGEM: {character}, NIVEL: {level}";
        }

        private int DetermineSector(int turno)
        {
            if (turno <= 3) return 4;      // Base sólida
            if (turno <= 7) return 3;      // Avanço médio
            if (turno <= 12) return 2;     // Posição estratégica
            return 1;                      // Próximo ao trono
        }

        private int DetermineLevel(int turno, int sector)
        {
            // Nível mais alto para setores mais estratégicos
            int baseLevel = 5 - sector;
            
            // Ajusta o nível baseado no turno
            if (turno <= 5) return Math.Max(1, baseLevel - 2);
            if (turno <= 10) return Math.Max(1, baseLevel - 1);
            return baseLevel;
        }

        private string MakePromotionDecision(List<string> availableLetters, List<string> playerCards)
        {
            // Escolhe o personagem com maior prioridade que pode ser promovido
            string characterToPromote = availableLetters
                .Where(c => playerCards.Contains(c))
                .OrderByDescending(c => _characterPriorities[c])
                .FirstOrDefault();

            return $"PROMOVER: {characterToPromote}";
        }

        private string MakeVotingDecision(int turno, List<string> playerCards)
        {
            // Lógica de votação baseada no turno e estado do jogo
            if (turno <= 5)
            {
                return "VOTO: S"; // Vota sim no início para estabelecer posição
            }
            else if (turno >= 15)
            {
                return "VOTO: F"; // Vota fim quando o jogo está avançado
            }
            else
            {
                // Decisão baseada em probabilidade
                return _random.Next(100) < 60 ? "VOTO: S" : "VOTO: N";
            }
        }

        public void UpdatePriorities(string character, int newPriority)
        {
            if (_characterPriorities.ContainsKey(character))
            {
                _characterPriorities[character] = newPriority;
            }
        }

        public void UpdateSectorPriority(int sector, int newPriority)
        {
            if (_sectorPriorities.ContainsKey(sector))
            {
                _sectorPriorities[sector] = newPriority;
            }
        }
    }
} 