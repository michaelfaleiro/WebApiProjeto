
using WebApiProjeto.Models;

namespace WebApiProjeto.ViewModels.Clientes
{
    public class ListClienteViewModel : EditorClienteViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Orcamento> Orcamentos { get; internal set; }
    }
}
