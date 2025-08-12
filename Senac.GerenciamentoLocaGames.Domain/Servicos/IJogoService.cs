using Senac.GerenciamentoLocaGames.Domain.Dtos.Request;
using Senac.GerenciamentoLocaGames.Domain.Dtos.Response.Jogo;

namespace Senac.GerenciamentoLocaGames.Domain.Servicos
{
    public interface IJogoService
    {
        Task<IEnumerable<ObterTodosResponse>> ObterTodos();

        Task<ObterDetalhadoPorIdResponse> ObterDetalhadoPorId(long id);

        Task<CadastrarResponse> Cadastrar(CadastrarJogoRequest cadastrarRequest);

        Task AtualizarPorId(long id, AtualizarJogoRequest atualizarRequest);

        Task DeletarPorId(long id);

        Task AlugarJogo(long id, AlugarJogoRequest request);

        Task DevolverJogo(long id);
    }
}
