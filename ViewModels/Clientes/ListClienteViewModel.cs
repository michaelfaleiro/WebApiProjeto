﻿
namespace WebApiProjeto.ViewModels.Clientes
{
    public class ListClienteViewModel : EditorClienteViewModel
    {
        public int Id { get; set; }
        public List<Models.Contato> Contatos { get; set; }
    }
}
