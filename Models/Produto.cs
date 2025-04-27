using System.ComponentModel.DataAnnotations;

namespace ProcessoLabSystem.Models
{
    public class Produto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preço obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Quantidade obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade não pode ser negativa.")]
        public int Quantidade { get; set; }
    }
}