# DesafioDev

Este repositório contém os projetos Backend e Frontend para o DesafioDev.

## Tecnologias e Padrões Utilizados

Este projeto utiliza uma variedade de tecnologias e padrões de design para criar uma aplicação web robusta e escalável.

- **.NET Core**: A aplicação backend foi desenvolvida utilizando .NET Core, uma plataforma de desenvolvimento de código aberto, mantida pela Microsoft, para criar aplicações modernas e de alto desempenho.
- **Entity Framework Core**: Utilizado para o mapeamento objeto-relacional (ORM), permitindo que os desenvolvedores trabalhem com um banco de dados usando objetos .NET.
- **React**: A aplicação frontend foi desenvolvida utilizando React, uma biblioteca JavaScript para construir interfaces de usuário.
- **Padrão MVC (Model-View-Controller)**: Este padrão é evidente na estrutura do projeto backend, com a presença de diretórios para "Controllers" e "Domain" (que pode ser considerado como o "Model" neste contexto). O padrão MVC é um dos padrões de design mais comumente usados em aplicações web e ajuda a separar a lógica de negócios da interface do usuário, tornando o código mais organizado e fácil de manter.
- **Padrão Repository**: Este padrão pode ser identificado na presença do diretório "Infrastructure" no projeto backend. O padrão Repository abstrai os detalhes de acesso aos dados, fornecendo uma maneira mais objetiva de manipular conjuntos de dados de objetos de domínio.
- **Padrão Unit of Work**: Este padrão pode ser identificado na presença do diretório "Infrastructure" no projeto backend. O padrão Unit of Work mantém uma lista de objetos afetados por uma transação de negócios e coordena a gravação de alterações.
- **SOLID Principles**: Os princípios SOLID são um conjunto de princípios de design de software que promovem a manutenibilidade e a escalabilidade do código. Eles são amplamente utilizados neste projeto.
- **Clean Code**: O código neste projeto é escrito seguindo as diretrizes de Clean Code, o que significa que ele é fácil de ler, entender e manter.

## Estrutura do Projeto

O projeto é dividido em duas partes principais:

1. [Backend](./src/Backend) - Contém a API REST para o projeto, desenvolvida com .NET Core e Entity Framework Core.
2. [FrontEnd](./src/FrontEnd) - Contém a aplicação React que consome a API REST.

Cada diretório tem seu próprio README com instruções de instalação e uso.

## Instalação e Uso

Para instalar e usar cada projeto, siga as instruções no README de cada diretório:

1. [Instruções do Backend](./src/Backend/README.md)
2. [Instruções do FrontEnd](./src/FrontEnd/README.md)

## Docker

Para executar os projetos em containers Docker, siga as instruções nos READMEs de cada subprojeto.

## Estrutura do Repositório

O repositório está organizado da seguinte forma:

- `src/Backend`: Contém o código fonte do backend.
- `src/FrontEnd`: Contém o código fonte do frontend.

## Contribuição

Contribuições são bem-vindas. Para contribuir, por favor, abra um issue ou faça um pull request.

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
