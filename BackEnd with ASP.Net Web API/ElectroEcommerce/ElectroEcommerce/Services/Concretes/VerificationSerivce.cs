using ElectroEcommerce.DataBase.Models;
using ElectroEcommerce.Services.Abstracts;

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
		while (_dataContext.PrefixFolders.Any(p => p.randomPrefix.Equals(randomPrefix)));
		var prefixFolder = new RandomPrefixFolder
		{
			randomPrefix = randomPrefix
		};
		_dataContext.PrefixFolders.Add(prefixFolder);
		return randomPrefix;
		
	}

}
