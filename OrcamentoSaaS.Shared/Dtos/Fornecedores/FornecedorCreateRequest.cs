using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorCreateRequest(
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
    [StringLength(14, MinimumLength = 11)]
    string Documento,
    [Required]
    [Range(typeof(decimal), "0.01", "100000",
        ErrorMessage = "A porcentagem a prazo não pode ser negativa ou zero")]
    decimal PorcentagemAVista,
    [Range(typeof(decimal), "0.01", "100000",
        ErrorMessage = "A porcentagem a prazo não pode ser negativa ou zero")]
    [Required]
    decimal PorcentagemAPrazo);