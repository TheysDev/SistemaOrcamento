using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public record ClienteBaseRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = "";
    
    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string Cidade { get; set; } = "";
    
    [Required(ErrorMessage = "A Uf é obrigatória.")]
    [StringLength(2, MinimumLength = 2)]
    public string Uf { get; set; } = "";
    
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; set; } = "";
        
    public string? Telefone { get; set; } = "";
    
    [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
    [StringLength(18, MinimumLength = 11, ErrorMessage = "Deve ter no maximo 14 caracteres.")]
    public string Documento { get; set; } = "";
}