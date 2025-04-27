using System.ComponentModel.DataAnnotations;

namespace ProcessoLabSystem.Models
{

    public class Cliente
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Celular obrigatório, É necessário que você digite o seu número com o seguinte padrão:  (XX) XXXXX-XXXX ")]
        [RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$")] //Faz com que o Input de número no formato (XX) XXXXXX-XXXX seja obrigatório
        public string Celular { get; set; }
    }
}