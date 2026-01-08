using System.ComponentModel.DataAnnotations;

namespace AspProfile.Models.DTO;

public class UserResponseDTO
{
    [Required(ErrorMessage="O primeiro nome é obrigatório")]
    public string FirstName { get; set; }

    [Required(ErrorMessage="O sobrenome é obrigatório")]
    public string LastName { get; set; }

    [Required(ErrorMessage="O Email é obrigatório")]
    [EmailAddress(ErrorMessage="O Email é inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage="A descrição é obrigatória")]
    public string Description { get; set; }
}