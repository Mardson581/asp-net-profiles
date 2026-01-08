using System.ComponentModel.DataAnnotations;

namespace AspProfile.Models.DTO;

public class UserRequestDTO
{
    public string Id { get; set; }
    
    [MinLength(3, ErrorMessage="O nome deve ter no mínimo 3 caracteres")]
    [MaxLength(50, ErrorMessage="O nome deve ter no máximo 50 caracteres")]
    public string FirstName { get; set; }

    [MinLength(5, ErrorMessage="O sobrenome deve ter no mínimo 5 caracteres")]
    [MaxLength(100, ErrorMessage="O sobrenome deve ter no máximo 100 caracteres")]
    public string LastName { get; set; }

    [Required(ErrorMessage="O Email é obrigatório")]
    [EmailAddress(ErrorMessage="O Email é inválido")]
    public string Email { get; set; }

    [MinLength(10, ErrorMessage="A descrição deve ter no mínimo 10 caracteres")]
    [MaxLength(200, ErrorMessage="A descrição deve ter no máximo 200 caracteres")]
    public string Description { get; set; }
}