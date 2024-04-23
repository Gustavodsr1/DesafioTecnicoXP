<h1 align="center">Desafio Tecnico XP Gestao de Portfolio</h1>
<p align="center">Este projeto apresenta uma abordagem simplificada para gerenciar uma carteira de investimentos, modelada após as práticas de gestão de portfólio utilizadas em mesas de operações financeiras.</p>
<h2> Introdução do Projeto </h2>
<p>Para o desenvolvimento do projeto, procurei novos design patterns para estudos e decidi utilizar o SOLID e pela primeira vez o padrão Facade. Na arquitetura utilizei o DDD (Domain Driven Design).</p>
<p>Dado o prazo restrito para a entrega do projeto, foram realizadas adaptações para garantir que as funcionalidades estejam alinhadas com o escopo estabelecido.</p>
<p>A seguir, apresento um guia passo a passo para a execução do programa e a realização de testes de suas funcionalidades.</p>
<h2> Requisitos do projeto </h2>
<p>Criar um serviço que permita o time de operação realizar manutenção nos produtos de investimentos.</p>
<h3> Funcionalidades: </h3>
<p>Gestão dos produtos financeiros</p>
<p>Disparo de e-mail diário para notificar os administradores a respeito dos produtos com vencimento próximo</p>
<p>Criar um serviço que permita o cliente comprar, vender e consultar seus investimentos.</p>
<p>Negociar produto financeiro (Compra e Venda)</p>
<p>Extrato do produto</p>
<h4>O que esperamos:</h4>
<p>As funcionalidades de consulta de produtos disponíveis e extrato devem suportar um grande volume de requisições e manter baixo tempo de resposta, abaixo de 100ms</p>
<p>Documentação de como executar a aplicação</p>
<p>Documentação de como utilizar a aplicação </p>
<h2>Executando o Projeto</h2>
<p>Para iniciar o projeto, abra a solução GestaoPortfolio.API.sln, comece executando o comando "docker-compose up -d" na pasta principal, onde está localizado o arquivo docker-compose.yml. Após confirmar que tudo está funcionando corretamente.</p>
<p>Quando o projeto está em execução, o banco de dados é criado (sendo recriado a cada execução) e uma página do Swagger é aberta. Os testes das funcionalidades são simples, porém é necessário seguir uma sequência para preencher as informações necessárias nas tabelas ao realizar uma chamada.</p>
<ol>
    <li>
        <p>Primeiro, é necessário criar um usuário na rota "usuario/criar" e, em seguida, utilizar as mesmas informações para fazer o login em "usuario/login". Após verificação bem-sucedida, copie o token e cole-o no campo disponível na modal que abrirá ao clicar no botão "Authorize" no topo da página. Importante: o e-mail precisa seguir o padrão correto, terminando com "@xxxx.xxx".</p>
    </li>
    <li>
        <p>Utilize as rotas de POST para incluir um cliente (cliente/inserir) e um produto (produto/criar). Essas são as tabelas mais básicas e não têm dependências.</p>
    </li>
    <li>
        <p>Após a criação do cliente e do produto, crie uma oferta (oferta/criar) utilizando o código do produto criado anteriormente para associá-lo ao papel.</p>
    </li>
    <li>
        <p>Com os passos anteriores, todos os dados necessários para realizar a primeira operação estão disponíveis. Nesse caso, é obrigatório ser uma compra, pois ainda não há nenhuma posição na carteira do cliente para realizar uma venda. Utilize a rota "operacao/incluir/compra" para enviar o código da oferta criada, o ID do cliente que está realizando a compra e a quantidade da operação.</p>
    </li>
    <li>
        <p>Verifique no GET da carteira se a posição foi criada. Feito isso, todas as outras rotas estão prontas para serem testadas, incluindo a operação de venda. (Obs.: É importante prestar atenção ao código da oferta e ao ID do cliente ao registrar uma operação, pois essas informações são essenciais para que tudo ocorra conforme o esperado).</p>
    </li>
</ol>
<h2>Documentação da API para Teste no Swagger</h2>
<h3>Clientes</h3>
<b>GET /cliente/listar</b>
<p>Retorna uma lista de todos os clientes cadastrados na aplicação, podendo ser filtrado por IdCliente.</p>
<b>POST /cliente/incluir</b>
<p>Chamada para criação de um novo cliente, o campo de idCliente pode ser desconsiderado.</p>
<b>PUT /cliente/alterar</b>
<p>Chamada para realizar alterações em um cliente já existente.</p>
<br>
<h3>Usuario</h3>
<b>POST /usuario/criar</b>
<p>É possível fazer o cadastro do usuário que irá utilizar a aplicação.</p>
<b>POST /usuario/login</b>
<p>Recebemos o token de autenticação caso os dados enviados estejam corretos.</p>
<br>
<h3>Oferta</h3>
<b>GET /oferta/listar</b>
<p>Retorna todas as ofertas criadas, podendo ser filtrada por id.</p>
<b>POST /oferta/criar</b>
<p>Essa chamada é utilizada para criar uma nova oferta, podendo desconsiderar o "codigoOferta" no envio.</p>
<b>PUT /oferta/alterar</b>
<p>Essa chamada é utilizada para fazer alguma alteração na oferta, caso seja necessário.</p>
<br>
<h3>Operacao</h3>
<b>GET /operacao/listar</b>
<p>Retorna todas as operações armazenadas, podendo ser filtrada por IdOperacao.</p>
<b>POST /operacao/incluir/venda</b>
<p>Esse método cria uma operação de venda (visão cliente) na base, criando também alterações no estoque da oferta e na carteira (posição cliente). Os campos IdOperacao, valorPrecoUnitario, valorTotalOperacao, stauts e dataOperacao são desconsiderados pois são tratados pelo sistema.</p>
<b>POST /operacao/incluir/compra</b>
<p>Esse método cria uma operação de compra (visão cliente) na base, criando também alterações no estoque da oferta e na carteira (posição cliente). Os campos IdOperacao, valorPrecoUnitario, valorTotalOperacao, stauts e dataOperacao são desconsiderados pois são tratados pelo sistema.</p>
<b>PUT /operacao/alterar</b>
<p>Como a operação é um registro de conferência e histórico, o ideal seria que não fossem alterados os campos de dados sobre a compra ou venda, apenas seu status (GRAVADA, CANCELADA, ERRO, etc)</p>
<br>
<h3>Carteira</h3>
<b>GET /carteira/listar</b>
<p>Retorna todas as carteiras de clientes, podendo ser filtrada pelo campo de IdCliente para nos trazer a carteira de um único cliente.</p>
<b>POST /carteira/incluir</b>
<p>Insere uma nova posição quando a operação de compra é criada. Apesar de já inserir uma nova posição automaticamente quando a operação de compra de uma nova oferta é criada, disponibilizei para testes.</p>
<b>PUT /carteira/alterar</b>
<p>O ideal, assim como na operação, é que a posição do cliente não seja alterada diretamente por chamada, mas disponibilizei para testes. As alterações feitas quando há uma venda são tratadas diretamente pelo backend da aplicação.</p>
<b>DELETE /carteira/excluir/{idPosicao}</b>
<p>Novamente, método disponibilizado apenas para testes, visto que quando o cliente vende todas suas quantidades de um ativo, a posição já é removida pelo tratamento feito no backend.</p>
<br>
<h3>Produto</h3>
<b>GET /produto/listar</b>
<p>Retorna uma lista com todos os produtos cadastrados, sendo possível filtrar por id.</p>
<b>POST /produto/incluir</b>
<p>Cria um novo produto na base, podendo ser utilizado para criar uma nova oferta. Temos um produto cadastrado como testes, que é o CDB.</p>
<b>PUT /produto/alterar</b>
<p>Utilizado para alterar desde o status do produto até sua descrição.</p>
