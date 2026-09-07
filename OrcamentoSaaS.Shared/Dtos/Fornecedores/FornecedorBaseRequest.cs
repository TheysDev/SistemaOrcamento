using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorBaseRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string Cidade { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A Uf é obrigatória.")]
    [StringLength(2, MinimumLength = 2)]
    public string Uf { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; set; } = string.Empty;
        
    public string? Telefone { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
    [StringLength(18, MinimumLength = 11, ErrorMessage = "Deve ter no maximo 14 caracteres.")]
    public string Documento { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A porcentagem é obrigatório.")]
    [Range(typeof(decimal), "0,01", "100000",
        ErrorMessage = "A porcentagem a prazo não pode ser negativa ou zero")]
    public decimal PorcentagemAVista { get; set; }
    
    [Range(typeof(decimal), "0,01", "100000",
        ErrorMessage = "A porcentagem a prazo não pode ser negativa ou zero")]
    [Required(ErrorMessage = "A porcentagem é obrigatório.")]
    public decimal PorcentagemAPrazo { get; set; }
}