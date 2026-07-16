namespace OrcamentoSaaS.Api.Querys.ProdutoQuery;

public class GetByCodigo(AppDbContext db)
{
    public Produto ProdutoGetByCodigo(int codigo)
    {
        return db.Produtos.FirstOrDefault(p => p.Codigo == codigo);
    }
}