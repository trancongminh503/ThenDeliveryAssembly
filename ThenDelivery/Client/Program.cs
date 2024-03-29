using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ThenDelivery.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddHttpClient("ThenDelivery.AnonymousAPI", client => {
				client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
			});

			// auto inject this HttpClient
			builder.Services.AddHttpClient("ThenDelivery.ServerAPI",
				client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				 .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services
				.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
				.CreateClient("ThenDelivery.ServerAPI"));

			builder.Services.AddDevExpressBlazor();

			builder.Services
				.AddApiAuthorization()
				.AddAccountClaimsPrincipalFactory<CustomUserFactory>();

			await builder.Build().RunAsync();
		}
	}
}
