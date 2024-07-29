using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Repositorios;
using TransacaoFinanceira.Dominio.Servicos;

namespace TransacaoFinanceira.Aplicacao.Servicos
{
    public class VerificaTransacaoService(IRepositorioContas RepositorioContas) : IVerificaTransacaoService
    {
        public void VerificarTransacao(Transacao transacao)
        {
            var contaOrigem = RepositorioContas.Consultar(transacao.IdContaOrigem);
            if (contaOrigem.Id <= 0)
            {
                transacao.SetStatus(Enums.EStatusTransacao.ContaOrigemNaoExiste);
                return;
            }
            var contaDestino = RepositorioContas.Consultar(transacao.IdContaDestino);
            if (contaDestino.Id <= 0)
            {
                transacao.SetStatus(Enums.EStatusTransacao.ContaDestinoNaoExiste);
                return;
            }

            if (contaOrigem.Id == contaDestino.Id)
            {
                transacao.SetStatus(Enums.EStatusTransacao.ContasIguais);
                return;
            }

            if (transacao.ValorTransacao <= 0)
            {
                transacao.SetStatus(Enums.EStatusTransacao.ValorTransacaoInvalido);
            }
        }
    }
}
