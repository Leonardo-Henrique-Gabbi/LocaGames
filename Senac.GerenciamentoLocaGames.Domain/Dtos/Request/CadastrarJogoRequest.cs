namespace Senac.GerenciamentoLocaGames.Domain.Dtos.Request;

public class CadastrarJogoRequest
{
    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public bool Disponivel { get; set; }

    public string Categoria { get; set; }

    public string Responsavel { get; set; }

    DateTime DataRetirada { get; set; }
}
