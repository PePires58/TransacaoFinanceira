namespace TransacaoFinanceira.Dominio.Exceptions
{

    [Serializable]
	public class ContaOrigemNaoExisteException : Exception
	{
		public ContaOrigemNaoExisteException() { }
		public ContaOrigemNaoExisteException(string message) : base(message) { }
		public ContaOrigemNaoExisteException(string message, Exception inner) : base(message, inner) { }
	}
}
