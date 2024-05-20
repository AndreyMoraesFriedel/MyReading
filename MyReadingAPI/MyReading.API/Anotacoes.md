# Anota��es do Projeto MyReading.API

## Estrutura do Projeto
- **Models**: Cont�m as classes que representam os dados da aplica��o.
- **Controllers**: Cont�m as classes que lidam com as requisi��es HTTP e fornecem respostas.
- **Data**: Cont�m a configura��o do contexto do banco de dados (`DbContext`).
- **appsettings.json**: Armazena configura��es da aplica��o.

## Passos Realizados

### 1. Cria��o da Classe Book
- Arquivo: `Models/Book.cs`
- Define a estrutura dos dados do livro.

### 2. Cria��o do Contexto de Banco de Dados
- Arquivo: `Data/MyReadingContext.cs`
- Configura a conex�o com o banco de dados e define os conjuntos de dados (`DbSet`).

### 3. Cria��o da F�brica do Contexto de Banco de Dados
- Arquivo: `Data/MyReadingContextFactory.cs`
- Cria inst�ncias do `DbContext` no design-time.

### 4. Configura��o do Startup.cs
- Arquivo: `Startup.cs`
- Configura os servi�os e o pipeline de solicita��o HTTP.

### 5. Cria��o do Controlador BooksController
- Arquivo: `Controllers/BooksController.cs`
- Define os endpoints da API para manipular os dados dos livros.

### 6. Configura��o do appsettings.json
- Arquivo: `appsettings.json`
- Armazena configura��es da aplica��o, como strings de conex�o para bancos de dados.

### Testando a API com Swagger
- Pressione `F5` no Visual Studio.
- Navegue para `https://localhost:<porta>/swagger`.