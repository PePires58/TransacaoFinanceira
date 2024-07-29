using TransacaoFinanceira.Dominio.Entidades;

namespace TransacaoFinanceira.Dominio.Servicos
{
    public interface IImprimirResultadoTransacoesService
    {
        void Imprimir(Transacao[] transacoes);
        void Imprimir(Transacao transacao);
    }
}
