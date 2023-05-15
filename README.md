# cash-flow-crf

Aplicação para lançamentos de entradas e saídas fluxo de caixa financeiro.
Utilizando Clean architecture e Mediator.

## Instalação da aplicação

Run the application stack

```bash
docker compose up -d
```

## Itens da solução

### Crf.ServerApp

Aplicação front-end em blazor webserver.

### Crf.Application

Logica da aplicação

### Crf.Domain

Entidades, enums, exceções, interfaces, tipos.

### Crf.Infrastructure

Contem classes para acessar recursos externos,acessar banco de dados, inicializar banco de dados,
serviço lançamento financeiro e relatorio.

### Banco de dados

Postgres com entity framework code first
