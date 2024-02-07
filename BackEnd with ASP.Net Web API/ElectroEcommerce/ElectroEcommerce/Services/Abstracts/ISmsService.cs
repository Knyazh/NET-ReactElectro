using Infobip.Api.Client.Model;

namespace ElectroEcommerce.Services.Abstracts;

public interface ISmsService
{
	Task<List<SmsTextualMessage>> SendSMSAsync(string recipient, string sms);
	Task<List<SmsTextualMessage>> SendSMSAsync(List<string> recipients, string sms);
}
