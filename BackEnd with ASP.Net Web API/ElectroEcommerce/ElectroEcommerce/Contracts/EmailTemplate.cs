namespace ElectroEcommerce.Contracts;

public class EmailTemplate
{
	public static class Body
	{
		public const string Activation_Email = "Dear user {Name} {Surname}, your account has been successfully activated. Your app password: [{app_password}]. ";
		public const string Exist_Account_Email = "Dear user, your account has already been confirmed and exists in our database";
		public const string Expired_Token = "Your activation token has expired";
	}

	public static class Subject
	{
		public const string Activation_Email = "Activation Email";
		public const string Expired_Token = "Expired Token";
		public const string Success_Activation = "Successful activation";
		public const string Order_Invoice = "Electro eCommerce Order Invoice";
	}
}
