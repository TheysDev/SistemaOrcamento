namespace OrcamentoSaaS.Shared.Dtos.Auth;

public record LoginResponse(string TokenType, string AccessToken, int ExpiresIn, string RefreshToken);