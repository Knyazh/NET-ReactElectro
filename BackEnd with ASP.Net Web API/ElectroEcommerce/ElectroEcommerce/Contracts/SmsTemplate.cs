namespace ElectroEcommerce.Contracts
{
	public class SmsTemplate
	{
		public static class Value
		{
			public const string activation_url = "Dear User,\r\n\r\nWe've just sent the confirmation link to your email address, {email}. Please check your inbox to complete the verification process.\r\n\r\nThank you for choosing {surname} {name}!";
		}
	}
}
