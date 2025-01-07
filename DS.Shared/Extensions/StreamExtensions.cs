namespace DS.Shared;

/// <summary>
///     Расширения потоков.
/// </summary>
public static class StreamExtensions
{
	/// <summary>
	///     Считывает все байты из потока.
	/// </summary>
	/// <param name="stream">Поток.</param>
	/// <returns>Прочитанные байты.</returns>
	public static byte[] ReadAllBytes(this Stream stream)
	{
		const int bufferSize = 512;

		using (var ms = new MemoryStream())
		{
			var buffer = new byte[bufferSize];

			using (var reader = new BinaryReader(stream))
			{
				int count;
				while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
				{
					ms.Write(buffer, 0, count);
				}
			}

			return ms.ToArray();
		}
	}
}
