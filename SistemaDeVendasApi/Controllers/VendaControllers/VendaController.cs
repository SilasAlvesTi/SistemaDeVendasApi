using Microsoft.AspNetCore.Mvc;
using SistemaDeVendasApi.DTOs;
using SistemaDeVendasApi.Models.VendaModels;
using SistemaDeVendasApi.Services;

namespace SistemaDeVendasApi.Controllers.VendaControllers
{
    [Route("/vendas")]
    [ApiController]
    public class VendaEndPoints : ControllerBase
    {
        private readonly VendaService _vendaService;

        public VendaEndPoints(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            return Ok(await _vendaService.GetAllVendasAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> GetVendaById(int id)
        {
            var venda = await _vendaService.GetVendaByIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            return Ok(venda);
        }

        [HttpPost]
        public async Task<ActionResult> PostVenda([FromBody] VendaAdicionarDTO vendaDto)
        {
            if (vendaDto == null)
            {
                return BadRequest("VendaDTO is null.");
            }

            var createdVenda = await _vendaService.AdicionarVendaAsync(vendaDto);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenda(int id, [FromBody] VendaAtualizarDTO vendaDto)
        {
            if (id != vendaDto.VendaID)
            {
                return BadRequest();
            }

            var result = await _vendaService.EditarVendaAsync(id, vendaDto);

            if (result is null)
            {
                return BadRequest("Não existe esse id");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            await _vendaService.RemoverVendaByIdAsync(id);
            return NoContent();
        }
    }
}
