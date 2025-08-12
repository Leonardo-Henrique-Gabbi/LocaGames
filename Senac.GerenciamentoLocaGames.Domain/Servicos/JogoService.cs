using Senac.GerenciamentoLocaGames.Domain.Dtos.Request;
using Senac.GerenciamentoLocaGames.Domain.Dtos.Response.Jogo;
using Senac.GerenciamentoLocaGames.Domain.Models;
using Senac.GerenciamentoLocaGames.Domain.Repositories;

namespace Senac.GerenciamentoLocaGames.Domain.Servicos
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<IEnumerable<ObterTodosResponse>> ObterTodos()
        {
            var jogos = await _jogoRepository.ObterTodos();


            return jogos.Select(j => new ObterTodosResponse
            {
                Id = j.Id,
                Titulo = j.Titulo
            });
        }

        public async Task<ObterDetalhadoPorIdResponse> ObterDetalhadoPorId(long id)
        {
            var jogo = await _jogoRepository.ObterdetalhadoPorId(id);
            ValidarSeExiste(jogo, id);

            var response = new ObterDetalhadoPorIdResponse
            {
                Id = jogo.Id,
                Titulo = jogo.Titulo,
                Descricao = jogo.Descricao,
                Disponivel = jogo.Disponivel,
                Categoria = jogo.Categoria.ToString(),
                Responsavel = jogo.Responsavel,
                DataRetirada = jogo.DataEntrega,
                IsEmAtraso = jogo.DataEntrega.HasValue && jogo.DataEntrega.Value < DateTime.Now
            };

            return response;
        }

        public async Task<CadastrarResponse> Cadastrar(CadastrarJogoRequest request)
        {
            if (!Enum.TryParse(request.Categoria, true, out Categoria categoria))
                throw new Exception($"Categoria '{request.Categoria}' inválida.");

            var jogo = new Jogo
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Categoria = categoria,
                Disponivel = request.Disponivel,
                Responsavel = request.Responsavel,
                DataEntrega = null,
                IsEmAtraso = false
            };

            long id = await _jogoRepository.Cadastrar(jogo);

            return new CadastrarResponse { Id = id };
        }

        public async Task AtualizarPorId(long id, AtualizarJogoRequest request)
        {
            if (!Enum.TryParse(request.Categoria, true, out Categoria categoria))
                throw new Exception($"Categoria '{request.Categoria}' inválida.");

            var jogo = await _jogoRepository.ObterdetalhadoPorId(id);
            ValidarSeExiste(jogo, id);

            jogo.Titulo = request.Titulo;
            jogo.Descricao = request.Descricao;
            jogo.Categoria = categoria;

            await _jogoRepository.AtualizarPorId(id, jogo);
        }

        public async Task DeletarPorId(long id)
        {
            var jogo = await _jogoRepository.ObterdetalhadoPorId(id);
            ValidarSeExiste(jogo, id);

            await _jogoRepository.DeletarPorId(id);
        }

        public async Task AlugarJogo(long id, AlugarJogoRequest request)
        {
            var jogo = await _jogoRepository.ObterdetalhadoPorId(id);
            ValidarSeExiste(jogo, id);

            if (!jogo.Disponivel)
                throw new Exception("O jogo já está alugado.");

            jogo.Disponivel = false;    
            jogo.Responsavel = request.Responsavel;
            jogo.DataEntrega = CalcularDataEntrega(jogo.Categoria);
            jogo.IsEmAtraso = false;

            await _jogoRepository.AlugarJogo(id, jogo);
        }

        public async Task DevolverJogo(long id)
        {
            var jogo = await _jogoRepository.ObterdetalhadoPorId(id);
            ValidarSeExiste(jogo, id);

            if (jogo.Disponivel)
                throw new Exception("O jogo já está disponível.");

            jogo.Disponivel = true;
            jogo.Responsavel = null;
            jogo.DataEntrega = null;
            jogo.IsEmAtraso = false;

            await _jogoRepository.DevolverJogo(id);
        }


        private DateTime CalcularDataEntrega(Categoria categoria)
        {
            return categoria switch
            {
                Categoria.BRONZE => DateTime.Now.AddDays(9),
                Categoria.PRATA => DateTime.Now.AddDays(6),
                Categoria.OURO => DateTime.Now.AddDays(3),
                _ => throw new Exception("Categoria inválida.")
            };
        }

        private void ValidarSeExiste(Jogo jogo, long id)
        {
            if (jogo == null)
                throw new Exception($"Jogo com ID {id} não encontrado.");
        }
    }
}
