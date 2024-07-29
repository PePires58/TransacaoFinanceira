using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Servicos;

namespace TransacaoFinanceira.Aplicacao.Servicos
{
    public class ImprimirResultadoTransacoesService : IImprimirResultadoTransacoesService
    {
        public void Imprimir(Transacao[] transacoes)
        {
            foreach (var item in transacoes.OrderBy(c => c.DataHora))
            {
                Imprimir(item);
            } 
        }

        public void Imprimir(Transacao transacao)
        {
            switch (transacao.Status)
            {
                case Enums.EStatusTransacao.AguardandoProcessamento:
                    Console.WriteLine("Transacao numero {0} aguardando processamento", transacao.Id);
                    break;
                case Enums.EStatusTransacao.EmProcessamento:
                    Console.WriteLine("Transacao numero {0} em processamento", transacao.Id);
                    break;
                case Enums.EStatusTransacao.Processada:
                    Console.WriteLine("Transacao numero {0} foi efetivada com sucesso! Novos saldos: Conta Origem:{1} | Conta Destino: {2}", transacao.Id, transacao.ContaOrigem.Saldo, transacao.ContaDestino.Saldo);
                    break;
                case Enums.EStatusTransacao.SaldoInsuficiente:
                    Console.WriteLine("Transacao numero {0} foi cancelada por falta de saldo", transacao.Id);
                    break;
                case Enums.EStatusTransacao.SaldoDesatualizadoRetry:
                    Console.WriteLine("Transacao numero {0} nao foi efetivada pois o saldo está desatualizando, iremos tentar novamente", transacao.Id);
                    break;
                case Enums.EStatusTransacao.ContaOrigemNaoExiste:
                    Console.WriteLine("Transacao numero {0} nao foi efetivada pois conta de origem {1} nao existe", transacao.Id, transacao.IdContaOrigem);
                    break;
                case Enums.EStatusTransacao.ContaDestinoNaoExiste:
                    Console.WriteLine("Transacao numero {0} nao foi efetivada pois conta de destino {1} nao existe", transacao.Id, transacao.IdContaDestino);
                    break;
                case Enums.EStatusTransacao.ContasIguais:
                    Console.WriteLine("Transacao numero {0} nao foi efetivada pois conta de origem e destino sao iguais", transacao.Id);
                    break;
                case Enums.EStatusTransacao.ValorTransacaoInvalido:
                    Console.WriteLine("Transacao numero {0} nao foi efetivada pois o valor de transacao e invalido", transacao.Id);
                    break;
                default:
                    break;
            }
        }
    }
}
