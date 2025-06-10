# 📊 Logs da IA - Guia de Visualização

## 🎯 Como Visualizar os Logs

### **1. No Visual Studio:**
1. **View → Output** (ou Ctrl+Alt+O)
2. **Selecione "Debug"** na lista suspensa
3. **Execute o projeto** e monitore os logs

### **2. No Console do Windows:**
1. **Execute o projeto** como aplicação de console
2. **Os logs aparecerão** diretamente no terminal

## 📋 Estrutura dos Logs

### **🤖 Consulta à IA**
```
🤖 === CONSULTA À IA LLAMA ===
📤 Enviando prompt para Llama...
🎯 Fase: S
🔄 Turno: 1
📝 Letras disponíveis: A, B, C, D, E, G, H, K, L, M, Q, R, T
🃏 Cartas do jogador: A, B, C
📊 Estado do jogo: [estado atual]
🌐 Conectando com: http://dns.auditto.com.br:11434/api/generate
📤 Prompt enviado:
==================================================
[prompt completo enviado à IA]
==================================================
```

### **📥 Resposta da IA**
```
📥 Resposta recebida (Status: 200):
==================================================
{
  "response": "SETOR: 4, PERSONAGEM: A, NIVEL: 3",
  "done": true
}
==================================================
🧠 Resposta da IA: SETOR: 4, PERSONAGEM: A, NIVEL: 3
🔧 Decisão processada: SETOR:4,PERSONAGEM:A,NIVEL:3
✅ Consulta à IA concluída com sucesso
```

### **🔍 Processamento da Resposta**
```
🔍 === PROCESSANDO RESPOSTA DA IA ===
📄 Resposta bruta: SETOR: 4, PERSONAGEM: A, NIVEL: 3
🎯 Fase: S
🧹 Resposta limpa: SETOR: 4, PERSONAGEM: A, NIVEL: 3
🎯 Processando Fase S (Seleção)
🔍 Setor encontrado: True - 4
🔍 Personagem encontrado: True - A
🔍 Nível encontrado: True - 3
✅ Decisão válida para Fase S: SETOR:4,PERSONAGEM:A,NIVEL:3
```

### **🎯 Execução da Decisão**
```
🎯 === EXECUTANDO DECISÃO DA IA ===
📋 Decisão recebida: SETOR:4,PERSONAGEM:A,NIVEL:3
🎮 Fase: S
🎯 Executando decisão de Seleção
🎯 === EXECUTANDO SELEÇÃO ===
📋 Decisão: SETOR:4,PERSONAGEM:A,NIVEL:3
🔍 Analisando partes da decisão: SETOR:4 | PERSONAGEM:A | NIVEL:3
🔍 Processando: SETOR = 4
✅ Setor válido: 4
🔍 Processando: PERSONAGEM = A
✅ Personagem válido: A
🔍 Processando: NIVEL = 3
✅ Nível válido: 3
🎯 Setor ajustado de 4 para 4 (baseado no contador 0)
🚀 Executando ação: ColocarPersonagem(4, A)
✅ Seleção concluída - Contador: 1
```

## 🔄 Fallback (Quando IA Falha)

### **❌ Erro de Conectividade**
```
❌ Erro na comunicação com Llama: 500
📄 Conteúdo do erro: Internal Server Error
🔄 Usando fallback: SETOR:3,PERSONAGEM:B,NIVEL:2
```

### **🔄 Fallback Automático**
```
🔄 === USANDO FALLBACK ===
🎯 Fase: S
📝 Letras disponíveis: A, B, C, D, E, G, H, K, L, M, Q, R, T
🃏 Cartas do jogador: A, B, C
🎲 Fallback Fase S: SETOR:3,PERSONAGEM:B,NIVEL:2
```

## 📊 Logs por Fase

### **📍 Fase S (Seleção)**
- Prompt: Instruções para posicionamento
- Resposta esperada: `SETOR: [1-4], PERSONAGEM: [A-Z], NIVEL: [1-5]`
- Ação: `ColocarPersonagem(setor, personagem)`

### **⬆️ Fase P (Promoção)**
- Prompt: Instruções para promoção
- Resposta esperada: `PROMOVER: [A-Z]`
- Ação: `Jogo.Promover(id, senha, personagem)`

### **🗳️ Fase V (Votação)**
- Prompt: Instruções para votação
- Resposta esperada: `VOTO: [S/N/F]`
- Ação: `Jogo.Votar(id, senha, voto)`

## 🚨 Logs de Erro

### **❌ Resposta Inválida**
```
❌ Formato inválido para Fase S - usando fallback
🔄 Usando fallback: SETOR:2,PERSONAGEM:C,NIVEL:4
```

### **💥 Erro de Rede**
```
💥 Erro ao comunicar com Llama: The operation has timed out
📋 Stack trace: [stack trace completo]
🔄 Usando fallback: SETOR:1,PERSONAGEM:A,NIVEL:3
```

### **❌ Fase Desconhecida**
```
❌ Fase desconhecida: X - usando fallback
🔄 Usando fallback: SETOR:4,PERSONAGEM:D,NIVEL:1
```

## 🎮 Logs de Jogo

### **🎯 Estado do Jogo**
```
📊 ESTADO ATUAL DO JOGO:

🆔 ID do Jogador: 1
🔄 Vez de quem: 1
✅ É minha vez: True
🎯 Fase atual: S
📝 Letras disponíveis: A, B, C, D, E, G, H, K, L, M, Q, R, T
🃏 Minhas cartas: A, B, C
🔢 Contador: 0
⏰ Timer ativo: True
```

### **🚀 Ações Executadas**
```
🚀 Executando ação: ColocarPersonagem(4, A)
✅ Seleção concluída - Contador: 1

🚀 Executando ação: Jogo.Promover(1, senha123, B)
✅ Promoção concluída: B

🚀 Executando ação: Jogo.Votar(1, senha123, S)
✅ Votação concluída: S
```

## 📈 Análise dos Logs

### **✅ Bot Funcionando Corretamente:**
1. **Consulta à IA** com sucesso
2. **Resposta válida** da IA
3. **Processamento correto** da resposta
4. **Execução da ação** no jogo
5. **Confirmação** da ação

### **⚠️ Bot com Problemas:**
1. **Erro de conectividade** com Llama
2. **Resposta inválida** da IA
3. **Fase desconhecida**
4. **Listas vazias**
5. **Timer inativo**

### **🔧 Debugging:**
- **Copie os logs** completos
- **Identifique** onde está parando
- **Verifique** conectividade com Llama
- **Teste** com botões de debug
- **Compare** com logs esperados

---

**🎯 Objetivo:** Monitorar em tempo real como a IA está tomando decisões e executando ações no jogo! 