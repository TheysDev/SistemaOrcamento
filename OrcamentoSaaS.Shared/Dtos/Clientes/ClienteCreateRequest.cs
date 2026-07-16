using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public sealed record ClienteCreateRequest(
    [Required]
    string Nome, 
    [Required]
    string Cidade,
    [Required]
    [StringLength(2, MinimumLength = 2)]
    string Uf, 
    [Required]
    [EmailAddress]
    string Email, 
    string? Telefone, 
    [Required]
    string Documento);