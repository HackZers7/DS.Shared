using Microsoft.Extensions.Configuration;

namespace DS.Shared.Tests;

public static class IoC
{
	public static readonly string FilePath;

	private static readonly object Lock = new();

	static IoC()
	{
		lock (Lock)
		{
			var configuration = new ConfigurationBuilder()
				.AddUserSecrets<Settings>()
				.Build();

			FilePath = configuration["FilePath"]!;
		}
	}
}