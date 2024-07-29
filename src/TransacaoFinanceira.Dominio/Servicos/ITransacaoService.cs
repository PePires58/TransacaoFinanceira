using TransacaoFinanceira.Dominio.Entidades;

namespace TransacaoFinanceira.Dominio.Servicos
{
    public interface ITransacaoService
    {
        public void EfetivarTransacao(Transacao transacao);
    }
}
