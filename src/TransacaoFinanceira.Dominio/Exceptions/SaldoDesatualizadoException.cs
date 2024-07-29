using System;

namespace TransacaoFinanceira.Dominio.Exceptions
{

    [Serializable]
	public class SaldoDesatualizadoException : Exception
	{
		public SaldoDesatualizadoException() { }
		public SaldoDesatualizadoException(string message) : base(message) { }
		public SaldoDesatualizadoException(string message, Exception inner) : base(message, inner) { }
		protected SaldoDesatualizadoException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
