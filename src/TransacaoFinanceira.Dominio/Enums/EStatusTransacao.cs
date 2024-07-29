namespace TransacaoFinanceira.Enums
{
    public enum EStatusTransacao
    {
        AguardandoProcessamento,
        EmProcessamento,
        Processada,
        SaldoInsuficiente,
        SaldoDesatualizadoRetry,
        ContaOrigemNaoExiste,
        ContaDestinoNaoExiste,
        ContasIguais,
        ValorTransacaoInvalido
    }
}
