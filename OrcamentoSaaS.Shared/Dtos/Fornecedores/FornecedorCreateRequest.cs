using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorCreateRequest(
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
    string? Telefone,
    [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
    [StringLength(14, MinimumLength = 11)]
    string Documento,
    [Required(ErrorMessage = "A porcentagem é obrigatório.")]
    [Range(typeof(decimal), "0,01", "100000",
        ErrorMessage = "A porcentagem a prazo não pode ser negativa ou zero")]
    decimal PorcentagemAVista,
    [Range(typeof(decimal), "0,01", "100000",
        ErrorMessage = "A porcentagem a prazo não pode ser negativa ou zero")]
    [Required(ErrorMessage = "A porcentagem é obrigatório.")]
    decimal PorcentagemAPrazo);