

# cash-flow-crf

 Aplicação para lançamentos de entradas e saídas de fluxo de caixa financeiro.
 Utilizando Clean architecture e Mediator.

  
## Arquitetura da Aplicação
![Diagrama da solução](https://github.com/brunofejesus/cash-flow-crf/blob/main/crf-desenho-solucao.png)
## Instalação da aplicação

**Utilizando Docker:**

**Requisitos mínimos:** docker instalado na maquina.
Acesse a pasta **cash-flow-crf/**Crf**/**  onde está localizado o arquivo **docker-compose.yml**
Execute os seguintes comandos utilizando o terminal:

    docker-compose build
Após terminar, execute em seguida:

    docker-compose up
    
Isso ira subir o banco de dados postgres e a aplicação web **Crf.ServerApp**.
Para acessar a aplicação utilize a seguinte URL no navegador: http://localhost:8000, se preferir alterar a porta, acesse o arquivo docker-compose.yml e altere a linha 16.

**Utilizando Visual Studio:**

**Requisitos mínimos**: postgres instalado na maquina.

 1. Defina o projeto **Crf.ServerApp** como projeto de inicialização,
    como mostrado na imagem abaixo:
    ![Definir projeto de inicialização](https://github.com/brunofejesus/cash-flow-crf/blob/main/ProjetoDeInicializacao.png)
    
 2. Altere o arquivo **appsettings.json** localizado no projeto
    **Crf.ServerApp** para informar o endereço do banco de dados postgres instalado na sua maquina, como mostrado na imagem abaixo:
    ![String de conexao](https://github.com/brunofejesus/cash-flow-crf/blob/main/AppSettingsJson.png)

 3. Execute o projeto.

 

  

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

### Crf.Application.UnitTests 
Testes unitários

### Banco de dados
Postgres com entity framework code first


