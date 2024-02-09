global using  ElectroEcommerce.DataBase;
using ElectroEcommerce.Services.Abstracts;
using ElectroEcommerce.Services.Concretes;
using Microsoft.EntityFrameworkCore;

namespace  ElectroEcommerce;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);


		builder.Services.AddControllers();
		builder.Services.AddDbContext<DataContext>(options =>
		{
			options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
		})

		.AddScoped<IEmailService, EmailService>()
		.AddScoped<ISmsService, SmsService>()
		.AddScoped<IActivationService, ActivationService>()
		.AddEndpointsApiExplorer()
		.AddSwaggerGen()
		.AddHttpContextAccessor()
		.AddCors(options =>
		{
			options.AddPolicy("AllowAll",
				builder =>
				{
					builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
		});

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors("AllowAll");
		app.UseHttpsRedirection();

		app.UseAuthorization();
		app.UseAuthentication();

		app.MapControllers();

		app.Run();
	}
}