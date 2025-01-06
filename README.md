# TicketWave-Event

## Micro Serviço de Gestão de Eventos para Vendas de Ingressos
---

## **Descrição**

Este projeto é um micro serviço desenvolvido para gerenciar eventos e facilitar a venda de ingressos. Ele permite a criação, atualização, consulta e exclusão de eventos, além de possibilitar a integração com outros sistemas de vendas. O serviço foi projetado utilizando boas práticas de programação, padrões modernos e uma arquitetura limpa, garantindo escalabilidade, testabilidade e manutenção eficiente.

---

## **Tecnologias Utilizadas**

- **.NET 9**: Plataforma principal utilizada para o desenvolvimento do micro serviço.
- **Minimal APIs**: Para uma abordagem enxuta e performática na criação de endpoints HTTP.
- **Repository Pattern**: Padrão de repositório implementado para abstração e gerenciamento do acesso aos dados.
- **XUnit**: Framework de teste para garantir a confiabilidade e a qualidade do sistema.
- **Pattern UserCases**: Organização das regras de negócio em casos de uso específicos.
- **Clean Architecture**: Estruturação do projeto para separar as responsabilidades e facilitar manutenção e escalabilidade.
- **FluentValidation**: Ferramenta para validação de dados e regras de negócio de forma limpa e reutilizável.
- **MongoDB**: Banco de dados NoSQL utilizado para persistência dos dados.
- **Docker**: Para containerização e fácil deploy do micro serviço.

---

## **Funcionalidades**

- **Gestão de Eventos**: 
  - Criação, edição, consulta e exclusão de eventos.
  - Consulta por filtros como data, localização, e categorias.
- **Validação de Dados**: Uso do FluentValidation para garantir a consistência das informações.
- **Escalabilidade**: Pronto para suportar grandes volumes de requisições.
- **Testes Automatizados**: Testes de unidade para cobrir casos de uso e garantir qualidade.
- **Desempenho Otimizado**: Uso de Minimal APIs para respostas rápidas e eficientes.

---
