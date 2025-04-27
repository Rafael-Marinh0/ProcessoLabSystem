using System.ComponentModel.DataAnnotations;

namespace ProcessoLabSystem.Models
{
    public class Fornecedor
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "CNPJ obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ precisa ter 14 dígitos.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Celular obrigatório, É necessário que você digite o seu número com o seguinte padrão:  (XX) XXXXX-XXXX")]
        [RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$")] //Código que faz que o Input de número no formato (XX) XXXXX-XXXX seja obrigatório
        public string Celular { get; set; }

    }
}
