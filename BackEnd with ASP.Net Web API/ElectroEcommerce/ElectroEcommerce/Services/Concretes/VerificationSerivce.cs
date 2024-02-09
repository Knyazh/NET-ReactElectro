using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Services.Abstracts;
using System.Security.Cryptography;
using System.Text;

namespace ElectroEcommerce.Services.Concretes;

public class VerificationSerivce : IVerificationService
{
	private readonly Random _random;
	private readonly DataContext _dataContext;
	public VerificationSerivce( DataContext dataContext)
	{
		_random = new Random();
		_dataContext = dataContext;
	}

	public string RandomFolderPrefixGenerator( string prefix)
	{
		string randomPrefix;
		do
		{
			decimal randomNumer = _random.Next(10000, 10000000);
			randomPrefix = prefix + randomNumer;
		}
		while (_dataContext.PrefixFolders.Any(p => p.RandomPrefix.Equals(randomPrefix)));
		var prefixFolder = new RandomPrefixFolder
		{
			RandomPrefix = randomPrefix
		};
		_dataContext.PrefixFolders.Add(prefixFolder);
		return randomPrefix;
		
	}

	public string HashPassword(string password)
	{
		using (SHA256 sha256 = SHA256.Create())
		{

			byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < hashedBytes.Length; i++)
			{
				builder.Append(hashedBytes[i].ToString("x2"));
			}

			return builder.ToString();
		}
	}
	public bool VerifyPassword(string password, string hashedPassword)
	{

		string hashedInput = HashPassword(password);

		return hashedInput == hashedPassword;
	}

	public string GenerateRandomSymmetricSecurityKey(int keySizeInBits = 1024)
	{
		int byteSize = keySizeInBits / 8;

		using (var provider = new RNGCryptoServiceProvider())
		{
			byte[] keyData = new byte[byteSize];
			provider.GetBytes(keyData);
			return Convert.ToBase64String(keyData);
		}
	}

	public string GenerateAppPassword(int length = 16)
	{
		const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		using (var rng = new RNGCryptoServiceProvider())
		{
			byte[] tokenData = new byte[length];
			rng.GetBytes(tokenData);

			char[] chars = new char[length];
			int validCharCount = validChars.Length;

			for (int i = 0; i < length; i++)
			{
				chars[i] = validChars[tokenData[i] % validCharCount];
			}

			return new string(chars);
		}
	}
}
