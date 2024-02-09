namespace ElectroEcommerce.Errors;

public class CustomErrors
{
	public enum Key
	{
		Email = 0,
		PhoneNumber = 1
	}

	public static class Value
	{
		public const string EMAIL_EXIST_ERROR = "Email address Already Registered";
		public const string PHONENUMBER_EXIST_ERROR = "Phone number Already Registered";
	}
}
