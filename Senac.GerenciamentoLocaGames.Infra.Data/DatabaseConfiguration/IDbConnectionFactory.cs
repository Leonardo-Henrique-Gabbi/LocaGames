
using System.Data;
namespace Senac.GerenciamentoLocaGames.Infra.Data.DatabaseConfiguration
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
