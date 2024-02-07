using Infobip.Api.Client;
using Infobip.Api.Client.Api;
using Infobip.Api.Client.Model;
using ElectroEcommerce.Services.Abstracts;

namespace ElectroEcommerce.Services.Concretes;

public class SmsService : ISmsService
{
	private readonly IConfiguration _configuration;
	private readonly string _base_url;
	private readonly string _api_key_prefix;
	private readonly string _api_key;
	private readonly string _sender;
	public SmsService(IConfiguration configuration)
	{
		_configuration = configuration;
		_base_url = _configuration.GetValue<string>("SMSServiceSettings:BASE_URL");
		_api_key_prefix = _configuration.GetValue<string>("SMSServiceSettings:API_KEY_PREFIX");
		_api_key = _configuration.GetValue<string>("SMSServiceSettings:API_KEY");
		_sender = _configuration.GetValue<string>("SMSServiceSettings:SENDER");
	}
	public async Task<List<SmsTextualMessage>> SendSMSAsync(List<string> recipients, string sms)
	{
		var sendSmsApi = SetSMSParameters();
		List<SmsTextualMessage> smsMessages = new List<SmsTextualMessage>();

		foreach (var recipient in recipients)
		{
			var smsRequest = GenerateSMSRequest(recipient, sms);
			smsMessages.Add(GenerateSMSRequest(recipient, sms).Messages.FirstOrDefault()!);

			var sms_id = string.Empty;

			try
			{
				var smsResponse = await sendSmsApi.SendSmsMessageAsync(smsRequest);

				Console.WriteLine("Response: " + smsResponse.Messages.FirstOrDefault());
				if (smsResponse.Messages.FirstOrDefault() is not null)
				{
					sms_id = smsResponse.Messages.FirstOrDefault()!.MessageId.ToString();
				}
			}
			catch (ApiException apiException)
			{
				Console.WriteLine($"Error occurred! Message: {sms_id} Error content", apiException.ErrorContent);
			}

		}

		return smsMessages;
	}

	public async Task<List<SmsTextualMessage>> SendSMSAsync(string recipient, string sms)
	{
		var smsMessages = await SendSMSAsync(new List<string> { recipient }, sms);

		return smsMessages;
	}

	private SmsAdvancedTextualRequest GenerateSMSRequest(string recipient, string sms)
	{

		var smsMessage = new SmsTextualMessage()
		{
			From = _sender,
			Destinations = new List<SmsDestination>()
			{
				new SmsDestination(to: recipient)
			},
			Text = sms,
			SendAt = DateTime.UtcNow.ToUniversalTime(),
		};

		var smsRequest = new SmsAdvancedTextualRequest()
		{
			Messages = new List<SmsTextualMessage>() { smsMessage }
		};

		return smsRequest;
	}

	private SendSmsApi SetSMSParameters()
	{

		var configuration = new Configuration
		{
			BasePath = _base_url,
			ApiKeyPrefix = _api_key_prefix,
			ApiKey = _api_key,
		};

		var sendSmsApi = new SendSmsApi(configuration);

		return sendSmsApi;
	}
}
