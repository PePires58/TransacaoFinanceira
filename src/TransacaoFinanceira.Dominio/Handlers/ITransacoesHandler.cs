using TransacaoFinanceira.Dominio.Entidades;

namespace TransacaoFinanceira.Dominio.Handlers
{
    public interface ITransacoesHandler
    {
        void RealizarTransacoes(Transacao[] transacoes);
    }
}
