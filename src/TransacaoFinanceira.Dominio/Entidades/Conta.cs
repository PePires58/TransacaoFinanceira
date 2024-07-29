namespace TransacaoFinanceira.Entidades
{
    public record Conta
    {
        public Conta(long id, decimal saldo)
        {
            Id = id;
            Saldo = saldo;
        }

        public long Id { get; set; }
        public decimal Saldo { get; set; }
    }
}
