namespace WebApiProjeto.Models
{
    public class ClienteOrcamento
    {
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }
        public int OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; }
    }
}