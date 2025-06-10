# 🤖 Estratégia do Agente Autônomo - Defensores de Hastings

## 🎯 Visão Geral

O agente autônomo foi projetado para tomar decisões estratégicas no jogo King Me! utilizando um sistema de prioridades e análise de estado do jogo, sem depender de IA externa.

## 🧠 Estratégia por Fase

### 📍 **Fase S - Seleção (Posicionamento)**

**Objetivo:** Posicionar personagens estrategicamente no tabuleiro

**Estratégia:**
- **Turnos 0-3:** Setor 4 (criar base sólida)
- **Turnos 4-7:** Setor 3 (avanço médio)
- **Turnos 8-12:** Setor 2 (posição estratégica)
- **Turnos 12-16:** Setor 1 (próximo ao trono)

**Sistema de Prioridades:**
- Personagens são priorizados de A a E
- Setores são priorizados de 1 a 4
- Níveis são ajustados baseados no turno e setor

**Formato da Resposta:**
```
SETOR: [número], PERSONAGEM: [letra], NIVEL: [1-5]
```

### ⬆️ **Fase P - Promoção**

**Objetivo:** Promover personagens para ganhar poder

**Estratégia:**
- Análise de personagens disponíveis
- Escolha baseada em prioridades
- Consideração de cartas disponíveis

**Formato da Resposta:**
```
PROMOVER: [letra]
```

### 🗳️ **Fase V - Votação**

**Objetivo:** Votar para influenciar o resultado do jogo

**Estratégia:**
- Turnos 0-5: Voto "Sim" para estabelecer posição
- Turnos 6-14: Decisão probabilística (60% Sim, 40% Não)
- Turnos 15+: Voto "Fim" para finalizar

**Formato da Resposta:**
```
VOTO: [S/N/F]
```

## 🔄 Mecanismo de Execução

### 1. **Análise do Estado**
```csharp
var decision = _autonomousAgent.MakeDecision(
    fase, turno, availableLetters, playerCards);
```

### 2. **Processamento da Decisão**
```csharp
ExecuteDecision(decision, fase);
```

## 📊 Sistema de Prioridades

### **Personagens:**
- A: Prioridade 5 (Mais alta)
- B: Prioridade 4
- C: Prioridade 3
- D: Prioridade 2
- E: Prioridade 1 (Mais baixa)

### **Setores:**
- 1: Prioridade 4 (Próximo ao trono)
- 2: Prioridade 3 (Posição estratégica)
- 3: Prioridade 2 (Avanço médio)
- 4: Prioridade 1 (Base sólida)

## 🛡️ Validações e Segurança

### **Validações de Entrada:**
- Verificação de setores válidos (1-4)
- Validação de personagens disponíveis
- Controle de níveis (1-5)
- Verificação de votos válidos (S/N/F)

### **Tratamento de Erros:**
- Validação de parâmetros de entrada
- Tratamento de casos excepcionais
- Logs de erro para debugging

## 🚀 Como Usar

1. **Compilar o projeto**
2. **Executar o jogo** - o agente usará automaticamente a estratégia

## 📈 Vantagens da Estratégia

### **Características:**
- ✅ Decisões baseadas em análise de estado
- ✅ Sistema de prioridades dinâmico
- ✅ Adaptação baseada no turno
- ✅ Validações robustas
- ✅ Fácil manutenção e ajuste

## 🔧 Configuração

### **Prioridades:**
```csharp
// Atualizar prioridades de personagens
_autonomousAgent.UpdatePriorities("A", 5);

// Atualizar prioridades de setores
_autonomousAgent.UpdateSectorPriority(1, 4);
```

## 📝 Logs e Debugging

O sistema registra:
- Decisões tomadas
- Estado do jogo
- Erros de validação
- Ajustes de prioridade

## 🎮 Exemplo de Uso

```csharp
// O agente automaticamente:
// 1. Verifica se é sua vez
// 2. Analisa o estado do jogo
// 3. Toma decisão baseada na estratégia
// 4. Executa a ação
// 5. Atualiza o estado do jogo
```

Esta implementação fornece um **adversário estratégico** que pode analisar o contexto do jogo e tomar decisões baseadas em regras predefinidas e análise de estado. 