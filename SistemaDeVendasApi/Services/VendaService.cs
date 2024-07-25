using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.DTOs;
using SistemaDeVendasApi.Models.VendaModels;
using SistemaDeVendasApi.Repositories;

namespace SistemaDeVendasApi.Services
{
    public class VendaService
    {
        private readonly VendaRepository _vendaRepository;
        private readonly ItemVendaRepository _itemVendaRepository;

        public VendaService(VendaRepository vendaRepository, ItemVendaRepository itemVendaRepository)
        {
            _vendaRepository = vendaRepository;
            _itemVendaRepository = itemVendaRepository;
        }

        public async Task<IEnumerable<Venda>> GetAllVendasAsync()
        {
            return await _vendaRepository.GetAllAsync();
        }

        public async Task<Venda> AdicionarVendaAsync(VendaAdicionarDTO vendaAdicionarDTO)
        {
            var venda = new Venda
            {
                ClienteID = vendaAdicionarDTO.ClienteID,
                ItensVenda = vendaAdicionarDTO.ItensVenda.Select(itemDto => new ItemVenda
                {
                    ProdutoID = itemDto.ProdutoID,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = itemDto.PrecoUnitario
                }).ToList()
            };
            CalcularTotalVenda(venda);

            await _vendaRepository.AdicionarAsync(venda);

            return venda;
        }

        public async Task<Venda> EditarVendaAsync(int id, VendaAtualizarDTO vendaAtualizarDTO)
        {
            var vendaDoBanco = await _vendaRepository.GetByIdAsync(vendaAtualizarDTO.VendaID);
            if (vendaDoBanco == null)
            {
                throw new Exception("Venda não encontrada.");
            }

            vendaDoBanco.DataVenda = DateTime.Now;

            foreach (var itemDto in vendaAtualizarDTO.ItensVenda)
            {
                var itemDoBanco = vendaDoBanco.ItensVenda.FirstOrDefault(i => i.ProdutoID == itemDto.ProdutoID);
                if (itemDoBanco != null)
                {
                    itemDoBanco.Quantidade = itemDto.Quantidade;
                    itemDoBanco.PrecoUnitario = itemDto.PrecoUnitario;
                }
                else
                {
                    vendaDoBanco.ItensVenda.Add(new ItemVenda
                    {
                        ProdutoID = itemDto.ProdutoID,
                        Quantidade = itemDto.Quantidade,
                        PrecoUnitario = itemDto.PrecoUnitario,
                        VendaID = vendaDoBanco.VendaID
                    });
                }
            }

            var itensParaRemover = vendaDoBanco.ItensVenda
                .Where(i => !vendaAtualizarDTO.ItensVenda.Any(dto => dto.ProdutoID == i.ProdutoID))
                .ToList();

            foreach (var item in itensParaRemover)
            {
                vendaDoBanco.ItensVenda.Remove(item);
                await _itemVendaRepository.ApagarAsync(item.ItemVendaID);
            }

            var venda = await _vendaRepository.EditarAsync(id, vendaDoBanco);

            return venda;
        }

        public async Task<Venda?> GetVendaByIdAsync(int id)
        {
            return await _vendaRepository.GetByIdAsync(id);
        }

        public async Task RemoverVendaByIdAsync(int id)
        {
            await _vendaRepository.ApagarAsync(id);
        }

        private void CalcularTotalVenda(Venda venda)
        {
            decimal total = 0;

            foreach (var item in venda.ItensVenda)
            {
                int quantidade = item.Quantidade;
                decimal precoUnitario = item.PrecoUnitario;
                decimal desconto = 0.1m;

                if (quantidade > 3)
                {
                    total += (quantidade * precoUnitario) - (quantidade * precoUnitario * desconto);
                }

            }
            venda.ValorTotal = total;
        }

    }
}
