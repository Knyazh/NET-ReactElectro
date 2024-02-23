namespace ElectroEcommerce.Errors;

public class CustomErrors
{
	public enum Key
	{
		Email = 0,
		PhoneNumber = 1,
		BasketItem = 2,
	}

	public static class Value
	{
		public const string EMAIL_EXIST_ERROR = "Email address Already Registered";
		public const string PHONENUMBER_EXIST_ERROR = "Phone number Already Registered";
		public const string BASKET_ITEM_POST_ERROR = "Adding basket Error please try again later";
	}
}
