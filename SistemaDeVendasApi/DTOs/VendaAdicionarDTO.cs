namespace SistemaDeVendasApi.DTOs
{
    public record VendaAdicionarDTO(int ClienteID, List<ItemVendaAdicionarDTO> ItensVenda);
}
