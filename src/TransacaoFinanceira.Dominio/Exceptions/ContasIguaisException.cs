namespace TransacaoFinanceira.Dominio.Exceptions
{

    [Serializable]
	public class ContasIguaisException : Exception
	{
		public ContasIguaisException() { }
		public ContasIguaisException(string message) : base(message) { }
		public ContasIguaisException(string message, Exception inner) : base(message, inner) { }
	}
}
