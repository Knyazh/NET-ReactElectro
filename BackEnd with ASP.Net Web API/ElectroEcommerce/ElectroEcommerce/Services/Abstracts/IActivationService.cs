using ElectroEcommerce.DataBase.Models;

namespace ElectroEcommerce.Services.Abstracts;

public interface IActivationService
{
	Task<ActivationToken> GenerateAndSendURL(User user, string token);
}
