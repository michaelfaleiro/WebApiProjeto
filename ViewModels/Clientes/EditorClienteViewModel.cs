using System.ComponentModel.DataAnnotations;

namespace WebApiProjeto.ViewModels.Clientes
{
    public class EditorClienteViewModel
    {
        [Required(ErrorMessage = "Nome obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Sobrenome obrigatório")]
        public string SobreNome { get; set; }
        [Required(ErrorMessage = "Telefone obrigatório")]
        public string Telefone { get; set; }
    }
}