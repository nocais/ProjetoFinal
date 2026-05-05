CONTROLLER
É a porta de entrada da API. Recebe as requisições, verifica quem pode chamar através do CORS, e encaminha o Service. Depois, formata a resposta com a classe RespostaApi,
que inclui os dados, o timestamp e o tempo de resposta.

SERVICE
É onde ficam as regras de negócio. Verifica se o email já existe, aplica hash na senha, ativa e desativa usuários, formata e envia os dados para o Repository guardar.

REPOSITORY
Guarda e busca os dados. Aqui usei um dicionário para simular um banco de dados. O Repository busca um usuário por id, por email, lista todos os ativos, adiciona um novo,
atualiza um existente e verifica se um email já está cadastrado.

MODELS
Como os dados são estruturados no sistema. Usuario guarda os dados no repositório. Os DTOs são formulários de entrada e saída. UsuarioCadastroDto é o que o frontend envia para cadastrar,
UsuarioAtualizacaoDto é para atualizar dados, e UsuarioRespostaDto é a resposta sem a senha. RespostaApi garante que as respostas tenham o mesmo formato.

MIDDLEWARE
Registra o horário de início e, depois que o Controller processa, calcula o tempo de resposta e escreve um log com o método HTTP, o caminho, o status code e o tempo de resposta.

FILTER
Executa antes do Controller. O ValidacaoFilter valida os dados, de acordo com os DTOs. Se os dados forem inválidos, retorna lista de erros e o Controller não é executado.

INTERFACES
São contratos que as classes precisam seguir. IUsuarioRepository declara os métodos do repositório, e IUsuarioService declara os métodos de Service.

FLUXO
Requisição chega, Middleware registra o horário de início. Em seguida, o Filtro valida os dados. O Controller recebe e chama o Service. Ele aplica as regras de negócio e chama o Repositório
para guardar ou buscar dados. Ele executa a operação no dicionário e retorna. O Service converte o resultado para DTO, o Controller envelopa com RespostaApi, e o Middleware registra o
tempo total antes de enviar a resposta.

