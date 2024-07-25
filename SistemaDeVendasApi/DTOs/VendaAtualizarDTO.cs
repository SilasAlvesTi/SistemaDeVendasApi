using SistemaDeVendasApi.Models.VendaModels;

namespace SistemaDeVendasApi.DTOs
{
    public record VendaAtualizarDTO(int VendaID, List<ItemVendaAdicionarDTO> ItensVenda);
}
