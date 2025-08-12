using Dapper;
using Senac.GerenciamentoLocaGames.Domain.Models;
using Senac.GerenciamentoLocaGames.Domain.Repositories;
using Senac.GerenciamentoLocaGames.Infra.Data.DatabaseConfiguration;

namespace LocaGames_API.Infra
{


    public class JogoRepository : IJogoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public JogoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AtualizarPorId(long id, Jogo jogo)
        {
            await _connectionFactory.CreateConnection()
             .QueryFirstOrDefaultAsync(
             @"
            UPDATE jogo
            SET
                titulo = @Titulo,
                descricao = @Descricao,
                categoria = @Categoria
            WHERE
                id = @Id
            ",jogo);
        }

        public async Task<long> Cadastrar(Jogo jogo)
        {
           
            var sql = @"
            INSERT INTO jogo
                (titulo, descricao, disponivel, categoria, responsavel, dataEntrega, isEmAtraso)
            OUTPUT INSERTED.id
            VALUES
                (@Titulo, @Descricao, @Disponivel, @Categoria, @Responsavel, @DataEntrega, @IsEmAtraso)
        ";

             var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleAsync<long>(sql, jogo);
        }
        

        public async Task DeletarPorId(long id)
        {
            {
                var sql = @"
            DELETE FROM Jogo
            WHERE id = @Id
        ";

                using var connection = _connectionFactory.CreateConnection();
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<Jogo> ObterdetalhadoPorId(long id)
        {
            return await _connectionFactory.CreateConnection()
            .QueryFirstOrDefaultAsync<Jogo>(
            @"
            SELECT 
                j.id,
                j.titulo,
                j.descricao,
                j.disponivel,
                c.Id AS Categoria,
                j.responsavel,
                j.dataEntrega,
                j.isEmAtraso
            FROM Jogo j
            INNER JOIN Categoria c ON c.Id = j.Categoria
            WHERE j.id = @Id
            ",
            new { Id = id }
            );
        }

        public async Task<IEnumerable<Jogo>> ObterTodos()
        {
            return await _connectionFactory.CreateConnection()
            .QueryAsync<Jogo>(
             @"
            SELECT 
                j.id,
                j.titulo,
                j.descricao,
                j.disponivel,
                c.Id AS Categoria,
                j.responsavel,
                j.dataEntrega,
                j.isEmAtraso
            FROM Jogo j
            INNER JOIN Categoria c ON c.Id = j.Categoria
             "
              );
        }

        public async Task AlugarJogo(long id, Jogo jogo)
        {
             await _connectionFactory.CreateConnection()
             .QueryFirstOrDefaultAsync(
                @"
            UPDATE Jogo
            SET
                responsavel = @Responsavel,
                disponivel = @Disponivel,
                dataEntrega = @DataEntrega
            WHERE
                Id = @Id
        ", jogo);
        }

        public async Task DevolverJogo(long id)
        {
            await _connectionFactory.CreateConnection()
            .QueryFirstOrDefaultAsync(
                @"
            UPDATE Jogo
            SET
                disponivel = 1,
                responsavel = NULL,
                dataEntrega = NULL
            WHERE
                Id = @Id
            ",
            new { Id = id }
            );
        }
    }
}

