using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senac.GerenciamentoLocaGames.Domain.Models
{
    public class Jogo
    {
        public  long Id { get; set; }

        public string Titulo { get; set; }

        public  string Descricao { get; set; }

        public  bool Disponivel { get; set; }

        public  Categoria Categoria { get; set; }

        public string Responsavel { get; set; }

         public DateTime? DataEntrega { get; set; }

        public bool IsEmAtraso { get; set; }
    }
}
