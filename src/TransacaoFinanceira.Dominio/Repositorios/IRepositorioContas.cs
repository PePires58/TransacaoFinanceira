using TransacaoFinanceira.Entidades;

namespace TransacaoFinanceira.Dominio.Repositorios
{
    public interface IRepositorioContas
    {
        Conta Consultar(long idConta);
        void Atualizar(Conta conta);
        void Atualizar(Conta conta, decimal saldoOriginal);

    }
}
