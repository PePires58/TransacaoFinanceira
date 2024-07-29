namespace TransacaoFinanceira.Dominio.Exceptions
{

    [Serializable]
    public class ContaDestinoNaoExisteException : Exception
    {
        public ContaDestinoNaoExisteException() { }
        public ContaDestinoNaoExisteException(string message) : base(message) { }
        public ContaDestinoNaoExisteException(string message, Exception inner) : base(message, inner) { }
    }
    
}
