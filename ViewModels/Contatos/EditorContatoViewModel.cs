using System.ComponentModel.DataAnnotations;

namespace WebApiProjeto.ViewModels.Contato;

public class EditorContatoViewModel
{
    [Required(ErrorMessage = "Telefone Obrigatório")]
    [MinLength(11, ErrorMessage = "Deve conter no mínimo 11 digitos")]
    [MaxLength(15, ErrorMessage = "Máximo 15 digitos")]
    public string Telefone { get; set; }

    [Required( ErrorMessage = "Informar o Id do Cliente")]
    public int ClienteId { get; set; }
}
