using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senac.GerenciamentoLocaGames.Domain.Models;

namespace Senac.GerenciamentoLocaGames.Domain.Repositories
{
    public interface IJogoRepository
    {
        Task AtualizarPorId(long id,Jogo jogo);

        Task<long> Cadastrar(Jogo jogo);

        Task DeletarPorId(long id);

        Task<Jogo> ObterdetalhadoPorId(long id);

        Task<IEnumerable<Jogo>> ObterTodos();

        Task AlugarJogo(long id, Jogo jogo);

        Task DevolverJogo(long id);
    }
}
