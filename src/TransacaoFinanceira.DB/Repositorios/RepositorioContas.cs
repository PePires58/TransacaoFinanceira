using TransacaoFinanceira.DB.Contexto;
using TransacaoFinanceira.Dominio.Exceptions;
using TransacaoFinanceira.Dominio.Repositorios;
using TransacaoFinanceira.Entidades;

namespace TransacaoFinanceira.DB.Repositorios
{
    public class RepositorioContas : IRepositorioContas
    {
        public RepositorioContas(DbContextMock dbContext)
        {
            DbContext = dbContext;
        }

        private DbContextMock DbContext { get; }

        public void Atualizar(Conta item)
        {
            DbContext.ContasDb[item.Id] = item;
        }

        public void Atualizar(Conta item, decimal saldoAtual)
        {
            if (DbContext.ContasDb.TryGetValue(item.Id, out Conta? conta))
            {
                if (conta.Saldo == saldoAtual)
                    DbContext.ContasDb[item.Id] = conta;
            }
            else
                throw new SaldoDesatualizadoException();
        }

        public Conta Consultar(long idConta) => DbContext.ContasDb.GetValueOrDefault(idConta, new(0, 0));
    }
}
