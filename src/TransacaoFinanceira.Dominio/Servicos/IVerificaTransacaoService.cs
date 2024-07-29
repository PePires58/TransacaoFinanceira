using TransacaoFinanceira.Dominio.Entidades;

namespace TransacaoFinanceira.Dominio.Servicos
{
    public interface IVerificaTransacaoService
    {
        void VerificarTransacao(Transacao transacao);
    }
}
