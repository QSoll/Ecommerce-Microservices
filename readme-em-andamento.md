📘 README — Projeto Ecommerce Microservices
🛒 Visão Geral
Este projeto implementa uma arquitetura de e-commerce baseada em microserviços utilizando ASP.NET Core. Os serviços são independentes, comunicam-se via HTTP e RabbitMQ, e seguem boas práticas de separação de responsabilidades, testes automatizados e autenticação.

📦 Microserviços
🔹 VendasService
Responsável pela criação e gerenciamento de pedidos.

Modelos:

Pedido: contém Id, Cliente, DataPedido, Itens, Status

ItemPedido: contém ProdutoId, Quantidade, PedidoId

StatusPedido: enum com Pendente, Confirmado, Cancelado

Validações:

[Required] em campos essenciais

[Key] explícito no Id

[Required] na lista de Itens para evitar pedidos vazios

Banco de Dados:

VendasDbContext com DbSet<Pedido> e DbSet<ItemPedido>

SQLite para produção

Endpoints:

POST /api/pedidos: cria pedido

GET /api/pedidos/{id}: consulta pedido

Extras:

Swagger configurado

Arquivo .http para testes via VS Code

Enum para status de pedido

🔹 EstoqueService
Responsável pelo cadastro e controle de produtos e estoque.

Modelos:

Produto: contém Id, Nome, Quantidade, Preço

Banco de Dados:

EstoqueDbContext com DbSet<Produto>

SQLite para produção, InMemory para testes

Endpoints:

GET /api/produtos: lista produtos

GET /api/produtos/{id}: consulta produto

POST /api/produtos: cria produto

PATCH /api/produtos/{id}/reduzir-estoque: reduz estoque

Extras:

Migrations configuradas

Testes automatizados em EstoqueTests

Banco InMemory para testes

Swagger ativo

🔹 ApiGateway
Pasta criada para futura configuração de roteamento centralizado com Ocelot ou YARP.

🧪 Testes
Projeto EstoqueTests com testes unitários e de borda

Validação de concorrência e produto inexistente

Testes com banco InMemory

🔐 Autenticação (em andamento)
Planejado para uso de JWT

Proteção de endpoints com [Authorize]

Geração de token via login

🐇 RabbitMQ (em andamento)
Planejado para comunicação assíncrona entre Vendas e Estoque

Publicação de eventos de venda

Redução de estoque via consumidor

📂 Estrutura do Projeto
Código
Ecommerce-Microservices/
├── ApiGateway/
├── EstoqueService/
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Migrations/
│   ├── Properties/
│   ├── obj/
├── EstoqueTests/
├── VendasService/
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Properties/
│   ├── obj/
│   ├── VendasService.http
├── Ecommerce-Microservices.sln
├── plataforma.md
├── README.md
🚀 Como rodar
Instale o .NET SDK

Restaure os pacotes:

bash
dotnet restore
Compile os projetos:

bash
dotnet build
Rode os microserviços:

bash
dotnet run --project VendasService
dotnet run --project EstoqueService
Acesse o Swagger em:

https://localhost:xxxx/swagger

🧠 O que foi feito além do escopo
Testes de concorrência e borda

Enum para status de pedido

Validação de pedidos vazios

Banco InMemory para testes

Arquivo .http para facilitar testes

Estrutura limpa e modular