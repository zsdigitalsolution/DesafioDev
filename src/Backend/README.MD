# DesafioDev Backend

Este é o backend do projeto DesafioDev. Ele é uma API RESTful construída com C# usando o padrão de arquitetura Domain-Driven Design (DDD).

## Arquitetura

O projeto segue o padrão de arquitetura Domain-Driven Design (DDD). Ele está organizado em camadas, incluindo a camada de domínio, a camada de aplicação, a camada de infraestrutura e a camada de apresentação.

O projeto também segue os princípios SOLID e utiliza vários padrões de projeto, incluindo Factory, Repository, Service e Controller.

## Tecnologias

O projeto utiliza as seguintes tecnologias:

- **.NET Core**: A aplicação backend foi desenvolvida utilizando .NET Core, uma plataforma de desenvolvimento de código aberto, mantida pela Microsoft, para criar aplicações modernas e de alto desempenho.
- **Entity Framework Core**: Utilizado para o mapeamento objeto-relacional (ORM), permitindo que os desenvolvedores trabalhem com um banco de dados usando objetos .NET.
- **Migrations**: As Migrations do Entity Framework Core são usadas para gerenciar as mudanças e versões do banco de dados.
- SQL Server
- **Padrão Repository**: Este padrão pode ser identificado na presença do diretório "Infrastructure". O padrão Repository abstrai os detalhes de acesso aos dados, fornecendo uma maneira mais objetiva de manipular conjuntos de dados de objetos de domínio.
- **Padrão Unit of Work**: Este padrão pode ser identificado na presença do diretório "Infrastructure". O padrão Unit of Work mantém uma lista de

## Testes

O projeto utiliza testes unitários para garantir a qualidade do código. Os testes são escritos usando a framework de testes xUnit e a biblioteca de mocking Moq.

## Docker

Para executar o projeto em um container Docker, siga as instruções abaixo:

1. Instale o Docker em sua máquina.
2. Navegue até a raiz do projeto.
3. Execute o comando `docker build -t desafiodev-backend .` para construir a imagem Docker.
4. Execute o comando `docker run -p 8080:80 desafiodev-backend` para iniciar o container.

Agora, a API estará disponível em `http://localhost:8080`.

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
