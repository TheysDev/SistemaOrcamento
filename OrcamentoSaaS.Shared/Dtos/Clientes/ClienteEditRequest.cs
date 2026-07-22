using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public sealed record ClienteEditRequest(
    [Required(ErrorMessage = "O nome é obrigatório.")]
    string Nome, 
    [Required(ErrorMessage = "A cidade é obrigatória.")]
    string Cidade,
    [Required(ErrorMessage = "A Uf é obrigatória.")]
    [StringLength(2, MinimumLength = 2)]
    string Uf, 
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    string Email, 
    string? Telefone);