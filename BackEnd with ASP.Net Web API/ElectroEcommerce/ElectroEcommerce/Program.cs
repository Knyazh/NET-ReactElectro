global using ElectroEcommerce.DataBase;
using ElectroEcommerce.Services.Abstracts;
using ElectroEcommerce.Services.Concretes;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ElectroEcommerce;

public class Program
{
	[Obsolete]
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddControllers()
			.AddFluentValidation(x =>
			{
				x.ImplicitlyValidateChildProperties = true;
				x.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
			});

		builder.Services
		.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		.AddCookie(options =>
		{
			options.Cookie.Name = "MyCustomCookie";
			options.LoginPath = "/auth/login";
			options.LogoutPath = "/auth/logout";
			options.ExpireTimeSpan = TimeSpan.FromHours(48);
		});

		builder.Services.AddLogging();
		builder.Services.AddDbContext<DataContext>(options =>
		{
			options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
		})

		.AddScoped<IFileService, FileService>()
		.AddScoped<IActivationService, ActivationService>()
		.AddScoped<IEmailService, EmailService>()
		.AddScoped<ISmsService, SmsService>()
		.AddScoped<IVerificationService, VerificationSerivce>()
		.AddScoped<IUserService, UserService>()
		.AddScoped<INotificationService, NotificationService>()
		.AddScoped<IBasketService, BasketService>()
		.AddScoped<IOrderService , OrderService>()
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


		using (var scope = builder.Services.BuildServiceProvider().CreateScope())
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
			SeedData.SeedDatabase(dbContext);
		}

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors("AllowAll");
		app.UseHttpsRedirection();
		app.UseHttpsRedirection();

		app.UseAuthorization();


		app.MapControllers();
		

		app.Run();
	}
}