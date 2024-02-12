namespace ElectroEcommerce.CustomEx
{
	public class ActivationException : Exception
	{
		public ActivationException() :base("Activate account failed") { }

		public ActivationException(string message) : base(message) { }

		public ActivationException(string message, Exception innerException) : base(message, innerException) { }
	}
}
