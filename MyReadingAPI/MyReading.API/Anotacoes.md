# Anotações do Projeto MyReading.API

## Estrutura do Projeto
- **Models**: Contém as classes que representam os dados da aplicação.
- **Controllers**: Contém as classes que lidam com as requisições HTTP e fornecem respostas.
- **Data**: Contém a configuração do contexto do banco de dados (`DbContext`).
- **appsettings.json**: Armazena configurações da aplicação.

## Passos Realizados

### 1. Criação da Classe Book
- Arquivo: `Models/Book.cs`
- Define a estrutura dos dados do livro.

### 2. Criação do Contexto de Banco de Dados
- Arquivo: `Data/MyReadingContext.cs`
- Configura a conexão com o banco de dados e define os conjuntos de dados (`DbSet`).

### 3. Criação da Fábrica do Contexto de Banco de Dados
- Arquivo: `Data/MyReadingContextFactory.cs`
- Cria instâncias do `DbContext` no design-time.

### 4. Configuração do Startup.cs
- Arquivo: `Startup.cs`
- Configura os serviços e o pipeline de solicitação HTTP.

### 5. Criação do Controlador BooksController
- Arquivo: `Controllers/BooksController.cs`
- Define os endpoints da API para manipular os dados dos livros.

### 6. Configuração do appsettings.json
- Arquivo: `appsettings.json`
- Armazena configurações da aplicação, como strings de conexão para bancos de dados.

### Testando a API com Swagger
- Pressione `F5` no Visual Studio.
- Navegue para `https://localhost:<porta>/swagger`.