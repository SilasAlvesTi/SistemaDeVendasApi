using SistemaDeVendasApi.Models.VendaModels;

namespace SistemaDeVendasApi.DTOs
{
    public record VendaListagemDTO(
        int VendaID,
        int ClienteID,
        DateTime DataVenda,
        decimal ValorTotal,
        ICollection<ItemVendaListagemDTO> ItensVenda);
}
