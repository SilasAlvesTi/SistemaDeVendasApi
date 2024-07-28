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

        public async Task<IEnumerable<VendaListagemDTO>> GetAllVendasAsync()
        {
            var vendas = await _vendaRepository.GetAllAsync();
            return vendas.Select(ToDTO).ToList();
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

        public VendaListagemDTO ToDTO(Venda venda)
        {
            return new VendaListagemDTO(
                venda.VendaID,
                venda.ClienteID,
                venda.DataVenda,
                venda.ValorTotal,
                venda.ItensVenda.Select(iv => new ItemVendaListagemDTO(
                    iv.ItemVendaID,
                    iv.VendaID,
                    iv.ProdutoID,
                    iv.Quantidade,
                    iv.PrecoUnitario
                )).ToList()
            );
        }

        public Venda ToEntity(VendaListagemDTO dto)
        {
            return new Venda
            {
                VendaID = dto.VendaID,
                ClienteID = dto.ClienteID,
                DataVenda = dto.DataVenda,
                ValorTotal = dto.ValorTotal,
                ItensVenda = dto.ItensVenda.Select(iv => new ItemVenda
                {
                    ItemVendaID = iv.ItemVendaID,
                    VendaID = iv.VendaID,
                    ProdutoID = iv.ProdutoID,
                    Quantidade = iv.Quantidade,
                    PrecoUnitario = iv.PrecoUnitario
                }).ToList()
            };
        }

    }
}
