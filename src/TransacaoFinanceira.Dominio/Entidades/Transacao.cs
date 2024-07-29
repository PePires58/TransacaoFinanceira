using System;
using TransacaoFinanceira.Entidades;
using TransacaoFinanceira.Enums;

namespace TransacaoFinanceira.Dominio.Entidades
{
    public record Transacao(long Id, DateTime DataHora, long IdContaOrigem, long IdContaDestino, decimal ValorTransacao)
    {
        public EStatusTransacao Status { get; private set; } = EStatusTransacao.AguardandoProcessamento;

        public Conta ContaOrigem { get; private set; } = new(0, 0);
        public Conta ContaDestino { get; private set; } = new(0, 0);

        public void SetStatus(EStatusTransacao status)
        {
            Status = status;
        }

        public void SetContasTransacao(Conta contaOrigem, Conta contaDestino)
        {
            ContaOrigem = contaOrigem;
            ContaDestino = contaDestino;
        }
    }
}
