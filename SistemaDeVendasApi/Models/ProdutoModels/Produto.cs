using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeVendasApi.Models.ProdutoModels
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Preco { get; set; }

        [Required]
        public int Estoque { get; set; }
    }
}
