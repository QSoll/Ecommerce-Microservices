Microserviço de Vendas
Tarefa	Estimativa
Criar projeto e estrutura básica	30 min
Modelos: Pedido, ItemPedido	30 min
Endpoints: Criar e consultar pedidos	1h
Integração com Estoque (via HTTP ou RabbitMQ)	1h–2h
Testes automatizados	1h
Subtotal: ~4h–5h

🔗 Comunicação com RabbitMQ
Tarefa	Estimativa
Configurar RabbitMQ localmente	30 min
Criar produtor no Vendas	45 min
Criar consumidor no Estoque	45 min
Testar fluxo completo	1h
Subtotal: ~3h

🔐 Autenticação com JWT
Tarefa	Estimativa
Implementar login e geração de token	1h
Proteger endpoints com [Authorize]	30 min
Criar roles e permissões básicas	30 min
Testes de autenticação	1h
Subtotal: ~3h

🚪 API Gateway
Tarefa	Estimativa
Configurar Ocelot ou YARP	1h
Criar rotas para Estoque e Vendas	30 min
Testar redirecionamento	30 min
Subtotal: ~2h

📈 Extras (opcional)
Tarefa	Estimativa
Logs com Serilog	30 min
Swagger enriquecido	30 min
Dockerização	1h–2h
Subtotal: ~2h–3h