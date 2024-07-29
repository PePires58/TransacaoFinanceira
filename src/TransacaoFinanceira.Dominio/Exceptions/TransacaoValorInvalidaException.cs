namespace TransacaoFinanceira.Dominio.Exceptions
{

    [Serializable]
	public class TransacaoValorInvalidaException : Exception
	{
		public TransacaoValorInvalidaException() { }
		public TransacaoValorInvalidaException(string message) : base(message) { }
		public TransacaoValorInvalidaException(string message, Exception inner) : base(message, inner) { }
		protected TransacaoValorInvalidaException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
