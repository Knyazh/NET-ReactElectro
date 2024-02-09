namespace ElectroEcommerce.Services.Abstracts;

public interface IVerificationService
{
	public string RandomFolderPrefixGenerator(string prefix);

	string HashPassword(string password);
	bool VerifyPassword(string password, string hashedPassword);
	string GenerateRandomSymmetricSecurityKey(int keySizeInBits = 1024);
	string GenerateAppPassword(int length = 16);
}
