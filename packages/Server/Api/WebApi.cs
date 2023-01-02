namespace Schulcast.Api;

public class WebApi : Module
{
	public override void ConfigureServices(IServiceCollection services)
	{
		services.AddCors();
		services.AddMemoryCache();
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
		{
			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = "Schulcast",
				ValidAudience = "Schulcast",
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_SECRET)),
			};
		});
		services.AddAuthorization(options =>
		{
			options.FallbackPolicy = new AuthorizationPolicyBuilder()
				.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
				.RequireAuthenticatedUser()
				.Build();
		});
		services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
		services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
		{
			options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			options.SerializerOptions.Converters.Add(new DateTimeConverter());
		});
	}

	public override void ConfigureApplication(WebApplication application)
	{
		application.UseRouting();
		application.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
		application.UseHttpsRedirection();
		application.UseAuthentication();
		application.UseAuthorization();
		application.UseSchulcastExceptionHandler();
	}
}