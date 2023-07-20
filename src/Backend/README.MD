# DesafioDev Backend

Este é o backend para o DesafioDev. Ele é uma API REST construída com .NET Core e Entity Framework Core.

## Tecnologias e Padrões Utilizados

- **.NET Core**: A aplicação backend foi desenvolvida utilizando .NET Core, uma plataforma de desenvolvimento de código aberto, mantida pela Microsoft, para criar aplicações modernas e de alto desempenho.

- **Entity Framework Core**: Utilizado para o mapeamento objeto-relacional (ORM), permitindo que os desenvolvedores trabalhem com um banco de dados usando objetos .NET.

- **Migrations**: As Migrations do Entity Framework Core são usadas para gerenciar as mudanças e versões do banco de dados.

- **Padrão MVC (Model-View-Controller)**: Este padrão é evidente na estrutura do projeto, com a presença de diretórios para "Controllers" e "Domain" (que pode ser considerado como o "Model" neste contexto). O padrão MVC é um dos padrões de design mais comumente usados em aplicações web e ajuda a separar a lógica de negócios da interface do usuário, tornando o código mais organizado e fácil de manter.

- **Padrão Repository**: Este padrão pode ser identificado na presença do diretório "Infrastructure". O padrão Repository abstrai os detalhes de acesso aos dados, fornecendo uma maneira mais objetiva de manipular conjuntos de dados de objetos de domínio.

- **Padrão Unit of Work**: Este padrão pode ser identificado na presença do diretório "Infrastructure". O padrão Unit of Work mantém uma lista de objetos afetados por uma transação de negócios e coordena a gravação de alterações.

## Estrutura do Projeto

- **Controllers**: Contém os controladores da API, que são responsáveis por coordenar as ações do aplicativo.
- **Domain**: Contém as classes de domínio, que são as principais classes de negócios.
- **Infrastructure**: Contém classes que suportam a camada de domínio.
- **Migrations**: Contém as migrações do Entity Framework Core.
- **Properties**: Contém as propriedades do projeto.
- **appsettings.json**: Contém as configurações do aplicativo, como strings de conexão do banco de dados.

## Instalação

1. Clone o repositório
2. Navegue até o diretório `src/Backend/DesafioDevApi`
3. Instale as dependências com `dotnet restore`

## Uso

Para iniciar o servidor, execute `dotnet run`. O servidor estará disponível em `http://localhost:5000`.

## Testes

Para executar os testes, execute `dotnet test`.

## Licença

MIT
