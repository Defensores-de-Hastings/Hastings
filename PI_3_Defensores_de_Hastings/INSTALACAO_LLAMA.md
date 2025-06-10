# 🚀 Instalação e Configuração do Bot Inteligente

## 📋 Pré-requisitos

### 1. **Visual Studio 2019 ou superior**
- .NET Framework 4.8
- Suporte a C#

### 2. **Pacotes NuGet Necessários**

Execute os seguintes comandos no **Package Manager Console** do Visual Studio:

```powershell
Install-Package Newtonsoft.Json -Version 13.0.3
```

Ou adicione via **NuGet Package Manager**:
1. Clique com botão direito no projeto
2. "Manage NuGet Packages"
3. Procure por "Newtonsoft.Json"
4. Instale a versão 13.0.3

## 🔧 Configuração

### 1. **Verificar Conectividade**

O bot está configurado para usar o servidor Llama em:
```
http://dns.auditto.com.br:11434
```

### 2. **Testar a Conexão**

Após compilar o projeto:
1. Execute o jogo
2. Clique em "Testar IA" para verificar conectividade
3. Se houver erro, o bot usará estratégia original como fallback

## 🎮 Como Usar

### **Execução Automática:**
1. Compile e execute o projeto
2. O bot automaticamente usará IA para decisões
3. Se Llama falhar, usa estratégia original

### **Teste Manual:**
- Use o botão "Testar IA" para verificar conectividade
- Use "Mostrar Estratégia" para ver detalhes da IA

## 🛠️ Solução de Problemas

### **Erro de Conectividade:**
```
❌ Erro na comunicação com Llama
```
**Solução:** 
- Verifique se o servidor está online
- O bot usará estratégia original automaticamente

### **Erro de Compilação:**
```
❌ Newtonsoft.Json não encontrado
```
**Solução:**
- Instale o pacote via NuGet
- Verifique se o packages.config está correto

### **Timeout de Conexão:**
```
❌ Timeout ao comunicar com Llama
```
**Solução:**
- O timeout é de 30 segundos
- O bot usará fallback automaticamente

## 📊 Estrutura dos Arquivos

```
PI_3_Defensores_de_Hastings/
├── LlamaService.cs          # Serviço de comunicação com IA
├── LlamaTest.cs             # Testes de conectividade
├── frmLobbyDaPartida.cs     # Formulário principal (modificado)
├── packages.config          # Dependências NuGet
└── BOT_STRATEGY.md          # Documentação da estratégia
```

## 🔄 Fallback Automático

Se o Llama não estiver disponível, o bot automaticamente:

1. **Detecta erro de conectividade**
2. **Usa estratégia original** (aleatória)
3. **Continua funcionando** normalmente
4. **Registra erro** no console

## 📝 Logs e Debugging

Para ver logs detalhados:
1. Abra o **Output Window** no Visual Studio
2. Execute o projeto
3. Monitore as mensagens de erro/conectividade

## ✅ Verificação de Instalação

Após a instalação, verifique se:

- [ ] Projeto compila sem erros
- [ ] Newtonsoft.Json está instalado
- [ ] Botão "Testar IA" funciona
- [ ] Bot joga automaticamente
- [ ] Fallback funciona se Llama falhar

## 🎯 Estratégia Implementada

### **Antes (Aleatória):**
- Decisões baseadas em sorte
- Sem análise contextual
- Estratégia fixa

### **Agora (Inteligente):**
- Análise contextual com IA
- Decisões estratégicas
- Adaptação dinâmica
- Fallback robusto

## 🚀 Próximos Passos

1. **Teste a conectividade** com o botão "Testar IA"
2. **Execute uma partida** para ver a IA em ação
3. **Monitore os logs** para ver decisões tomadas
4. **Ajuste parâmetros** se necessário

---

**🎮 O bot agora é um adversário inteligente que analisa o contexto do jogo e toma decisões estratégicas baseadas em IA!** 