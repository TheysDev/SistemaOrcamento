namespace OrcamentoSaaS.Api.Domain.Entities;

public class TenantProvider(IHttpContextAccessor httpContextAccessor) : ITenantProvider
{
    public Guid TenantId
    {
        get
        {
            var user = httpContextAccessor.HttpContext?.User;
            var tenantClaim = user?.FindFirst("TenantId");

            if (tenantClaim == null || !Guid.TryParse(tenantClaim.Value, out var tenantId))
            {
                return Guid.Parse("00000000-0000-0000-0000-000000111111");
                //throw new UnauthorizedAccessException("Usuário não pertence a nenhum tenant");
            }
            return tenantId;
        }
    }
};