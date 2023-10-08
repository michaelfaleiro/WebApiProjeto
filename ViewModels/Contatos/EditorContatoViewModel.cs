using System.ComponentModel.DataAnnotations;

namespace WebApiProjeto.ViewModels.Contato;

public class EditorContatoViewModel
{
    [Required(ErrorMessage = "Telefone Obrigatório")]
    public string Telefone { get; set; }

    [Required( ErrorMessage = "Informar o Id do Cliente")]
    public int ClienteId { get; set; }
}
