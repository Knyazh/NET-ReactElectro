using System.Text.Json.Serialization;

namespace ElectroEcommerce.DataBase.DTOs.Email;

public class EmailDto
{
	[JsonConstructor]
	public EmailDto(string recipient, string subject, string body)
	{
		Recipients = new List<string> { recipient };
		Subject = subject;
		Body = body;
	}
	[JsonConstructor]
	public EmailDto(List<string> recipients, string subject, string body)
	{
		Recipients = recipients;
		Subject = subject;
		Body = body;
	}

	public EmailDto() { }

	public List<string> Recipients { get; set; } = new List<string>();
	public string Subject { get; set; } = string.Empty;
	public string Body { get; set; } = string.Empty;
}
