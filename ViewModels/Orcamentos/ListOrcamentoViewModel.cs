using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProjeto.Models;

namespace WebApiProjeto.ViewModels.Orcamentos
{
    public class ListOrcamentoViewModel
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}