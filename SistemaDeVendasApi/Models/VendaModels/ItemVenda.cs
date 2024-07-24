using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SistemaDeVendasApi.Models.ProdutoModels;

namespace SistemaDeVendasApi.Models.VendaModels
{
    public class ItemVenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemVendaID { get; set; }

        [Required]
        public int VendaID { get; set; }

        [ForeignKey("VendaID")]
        public Venda Venda { get; set; }

        [Required]
        public int ProdutoID { get; set; }

        [ForeignKey("ProdutoID")]
        public Produto Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecoUnitario { get; set; }
    }
}
