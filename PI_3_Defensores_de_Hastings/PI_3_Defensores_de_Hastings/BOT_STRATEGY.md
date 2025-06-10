# 🤖 Estratégia do Bot Inteligente - Defensores de Hastings

## 🎯 Visão Geral

O bot foi aprimorado para usar **IA (Llama)** para tomar decisões estratégicas no jogo King Me!, substituindo a estratégia aleatória anterior por uma abordagem mais inteligente e contextual.

## 🔗 Integração com Llama

**URL do Serviço:** `http://dns.auditto.com.br:11434`

O bot utiliza o modelo **llama3** para analisar o estado do jogo e tomar decisões estratégicas baseadas em:

- Estado atual do jogo
- Fase atual (S/P/V)
- Letras disponíveis
- Cartas do jogador
- Turno atual

## 🧠 Estratégia por Fase

### 📍 **Fase S - Seleção (Posicionamento)**

**Objetivo:** Posicionar personagens estrategicamente no tabuleiro

**Estratégia Inteligente:**
- **Turnos 0-3:** Setor 4 (criar base sólida)
- **Turnos 4-7:** Setor 3 (avanço médio)
- **Turnos 8-12:** Setor 2 (posição estratégica)
- **Turnos 12-16:** Setor 1 (próximo ao trono)

**Decisões do Llama:**
- Escolha do setor baseada no progresso do jogo
- Seleção de personagem considerando estratégia
- Definição do nível (1-5) do personagem

**Formato da Resposta:**
```
SETOR: [número], PERSONAGEM: [letra], NIVEL: [1-5]
```

### ⬆️ **Fase P - Promoção**

**Objetivo:** Promover personagens para ganhar poder

**Estratégia Inteligente:**
- Analisa personagens disponíveis
- Escolhe personagem com maior potencial estratégico
- Considera posição atual no tabuleiro

**Formato da Resposta:**
```
PROMOVER: [letra]
```

### 🗳️ **Fase V - Votação**

**Objetivo:** Votar para influenciar o resultado do jogo

**Estratégia Inteligente:**
- Analisa contexto atual do jogo
- Considera posição estratégica
- Toma decisão baseada em objetivos

**Opções de Voto:**
- **S:** Sim
- **N:** Não  
- **F:** Fim

**Formato da Resposta:**
```
VOTO: [S/N/F]
```

## 🔄 Mecanismo de Execução

### 1. **Análise do Estado**
```csharp
var gameState = GetCurrentGameState();
var decision = await _llamaService.GetStrategicDecision(
    gameState, fase, _availableLetters, _playerCards, Contador);
```

### 2. **Processamento da Decisão**
```csharp
ExecuteLlamaDecision(decision, fase);
```

### 3. **Fallback Inteligente**
Se o Llama não estiver disponível ou retornar erro, o sistema usa a estratégia original como fallback.

## 📊 Prompt Estratégico

O Llama recebe um prompt detalhado que inclui:

```
Você é um jogador estratégico do jogo King Me! Analise a situação atual e tome a melhor decisão.

ESTADO ATUAL DO JOGO:
- Fase: [S/P/V]
- Turno atual: [número]
- Letras disponíveis: [lista]
- Suas cartas: [lista]
- Estado do jogo: [descrição]

ESTRATÉGIA BASEADA NA FASE:
[instruções específicas para cada fase]

RESPONDA APENAS COM:
[formato específico para cada fase]
```

## 🛡️ Validações e Segurança

### **Validações de Entrada:**
- Verificação de setores válidos (1-4)
- Validação de personagens disponíveis
- Controle de níveis (1-5)
- Verificação de votos válidos (S/N/F)

### **Tratamento de Erros:**
- Timeout de 30 segundos para comunicação
- Fallback automático para estratégia original
- Logs de erro para debugging

## 🚀 Como Usar

1. **Compilar o projeto** com as dependências do Newtonsoft.Json
2. **Verificar conectividade** com o servidor Llama
3. **Executar o jogo** - o bot usará automaticamente a IA

## 📈 Vantagens da Nova Estratégia

### **Antes (Aleatória):**
- ❌ Decisões baseadas em sorte
- ❌ Sem consideração do contexto
- ❌ Estratégia fixa e previsível

### **Agora (Inteligente):**
- ✅ Análise contextual do jogo
- ✅ Decisões baseadas em estratégia
- ✅ Adaptação dinâmica
- ✅ Fallback robusto

## 🔧 Configuração

### **URL do Llama:**
```csharp
private readonly string _baseUrl = "http://dns.auditto.com.br:11434";
```

### **Parâmetros do Modelo:**
- **Modelo:** llama2
- **Temperature:** 0.7 (criatividade balanceada)
- **Top_p:** 0.9 (diversidade controlada)
- **Max_tokens:** 200 (resposta concisa)

## 📝 Logs e Debugging

O sistema registra:
- Comunicação com Llama
- Decisões tomadas
- Erros de comunicação
- Fallbacks utilizados

## 🎮 Exemplo de Uso

```csharp
// O bot automaticamente:
// 1. Verifica se é sua vez
// 2. Analisa o estado do jogo
// 3. Consulta o Llama para decisão
// 4. Executa a ação estratégica
// 5. Atualiza o estado do jogo
```

Esta implementação transforma o bot de um jogador aleatório em um **adversário estratégico inteligente** que pode analisar o contexto do jogo e tomar decisões baseadas em estratégia real. 