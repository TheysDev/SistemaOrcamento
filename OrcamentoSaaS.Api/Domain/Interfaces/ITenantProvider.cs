namespace OrcamentoSaaS.Api.Domain.Interfaces;

public interface ITenantProvider
{
    Guid TenantId { get; }
}