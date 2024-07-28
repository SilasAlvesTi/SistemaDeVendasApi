namespace SistemaDeVendasApi.DTOs
{
    public record ItemVendaListagemDTO(
        int ItemVendaID,
        int VendaID,
        int ProdutoID,
        int Quantidade,
        decimal PrecoUnitario);
}
