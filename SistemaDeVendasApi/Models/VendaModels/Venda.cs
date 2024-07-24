using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SistemaDeVendasApi.Models.ClienteModels;

namespace SistemaDeVendasApi.Models.VendaModels
{
    public class Venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendaID { get; set; }

        [Required]
        public int ClienteID { get; set; }

        [ForeignKey("ClienteID")]
        public Cliente Cliente { get; set; }

        [Required]
        public DateTime DataVenda { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorTotal { get; set; }

        public ICollection<ItemVenda> ItensVenda { get; set; } = [];
    }
}
