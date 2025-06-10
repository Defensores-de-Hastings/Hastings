# 🐛 Debug do Bot - Guia de Solução de Problemas

## 🔍 Problema Identificado
O bot não está jogando automaticamente após clicar em "Começar".

## 📋 Passos para Debug

### 1. **Verificar Logs no Console**
1. Abra o **Output Window** no Visual Studio (View → Output)
2. Execute o projeto
3. Clique em "Começar"
4. Monitore as mensagens de log

### 2. **Usar Botões de Debug**

#### **Botão "Verificar Estado"**
- Mostra o estado atual do jogo
- Verifica se é sua vez
- Mostra fase atual
- Verifica se timer está ativo

#### **Botão "Debug Bot"**
- Executa o bot manualmente
- Mostra logs detalhados
- Identifica onde está o problema

#### **Botão "Testar IA"**
- Testa conectividade com Llama
- Verifica se IA está funcionando

### 3. **Possíveis Causas e Soluções**

#### **❌ Timer não está ativo**
**Sintoma:** Bot não executa automaticamente
**Solução:** 
- Verificar se `tmrVez.Enabled = true`
- Adicionar logs no construtor

#### **❌ Não é minha vez**
**Sintoma:** Bot sai sem fazer nada
**Solução:**
- Aguardar sua vez
- Verificar se ID do jogador está correto

#### **❌ Fase incorreta**
**Sintoma:** Bot não reconhece a fase
**Solução:**
- Verificar função `ReturnFaseJogo`
- Testar com diferentes fases

#### **❌ Listas vazias**
**Sintoma:** Sem letras disponíveis ou cartas
**Solução:**
- Verificar inicialização das listas
- Verificar função `ExibirCartas`

#### **❌ Erro de conectividade com Llama**
**Sintoma:** Timeout ou erro de rede
**Solução:**
- Verificar URL do servidor
- Usar fallback automático

### 4. **Logs Esperados**

#### **Bot Funcionando Corretamente:**
```
Timer iniciado após começar jogo
Exibindo cartas...
Cartas recebidas: ABC
Cartas do jogador: A, B, C
Testando IA inicial...
=== BOT INTELIGENTE ATIVADO ===
Fase atual: S
ID do jogador: 1
Vez de quem: 1
É minha vez? True
Letras disponíveis: A, B, C, D, E, G, H, K, L, M, Q, R, T
Minhas cartas: A, B, C
Contador: 0
Consultando Llama...
Estado do jogo: [estado atual]
Decisão do Llama: SETOR:4,PERSONAGEM:A,NIVEL:3
Decisão executada com sucesso
```

#### **Bot com Problemas:**
```
=== BOT INTELIGENTE ATIVADO ===
Fase atual: F
ID do jogador: 1
Vez de quem: 2
É minha vez? False
Não é minha vez - saindo
```

### 5. **Testes Manuais**

#### **Teste 1: Verificar Timer**
```csharp
// No construtor, adicionar:
Console.WriteLine($"Timer inicial: {tmrVez.Enabled}");
```

#### **Teste 2: Verificar Inicialização**
```csharp
// Após ExibirCartas:
Console.WriteLine($"Letras disponíveis após inicialização: {_availableLetters.Count}");
Console.WriteLine($"Cartas do jogador após inicialização: {_playerCards.Count}");
```

#### **Teste 3: Forçar Execução**
```csharp
// Chamar manualmente:
await roboInteligente();
```

### 6. **Soluções Rápidas**

#### **Solução 1: Reiniciar Timer**
```csharp
tmrVez.Enabled = false;
tmrVez.Enabled = true;
```

#### **Solução 2: Forçar Execução**
```csharp
Task.Run(async () => await roboInteligente());
```

#### **Solução 3: Usar Fallback**
```csharp
roboOriginal(); // Força uso da estratégia original
```

### 7. **Verificações Finais**

- [ ] Timer está ativo
- [ ] É minha vez
- [ ] Fase está correta
- [ ] Listas não estão vazias
- [ ] Llama está respondendo
- [ ] Logs aparecem no console

### 8. **Comandos de Debug**

#### **No Visual Studio:**
1. **Debug → Windows → Output**
2. **Debug → Windows → Immediate**
3. **Debug → Windows → Call Stack**

#### **Logs Importantes:**
- `=== BOT INTELIGENTE ATIVADO ===`
- `=== BOT ORIGINAL ATIVADO ===`
- `Timer iniciado após começar jogo`
- `Consultando Llama...`
- `Decisão do Llama:`

### 9. **Contato para Suporte**

Se o problema persistir:
1. Copie todos os logs do console
2. Use os botões de debug
3. Verifique o estado do jogo
4. Teste conectividade com Llama

---

**🎯 Objetivo:** Identificar exatamente onde o bot está parando e corrigir o problema específico. 