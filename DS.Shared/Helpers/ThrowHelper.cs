using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DS.Shared;

/// <summary>
///     Вспомогательный класс для проверки данных и выбрасывания ошибок.
/// </summary>
public static class ThrowHelper
{
	/// <summary>
	///     Проверяет, что аргумент не равен null, если равен, то выкидывает ошибку <see cref="ArgumentNullException" />.
	/// </summary>
	/// <param name="argument">Аргумент.</param>
	/// <param name="paramName">Наименование аргумента.</param>
	/// <typeparam name="T">Тип аргумента.</typeparam>
	/// <exception cref="ArgumentNullException">Выбрасывается если аргумент равен null.</exception>
	public static void ArgumentNotNull<T>(
		[NotNull] T? argument,
		[CallerArgumentExpression("argument")] string? paramName = null
	)
	{
		if (argument != null)
		{
			return;
		}

		throw new ArgumentNullException(paramName);
	}

	/// <summary>
	///     Проверяет, что аргумент не равен null, пустой, или содержит только пробелы.
	/// </summary>
	/// <param name="argument">Аргумент.</param>
	/// <param name="paramName">Наименование аргумента.</param>
	/// <exception cref="ArgumentNullException">Выбрасывается если аргумент равен null.</exception>
	/// <exception cref="ArgumentException">Выбрасывается если аргумент пустой или содержит только пробелы.</exception>
	public static void ArgumentNotNullOrWhiteSpace(
		[NotNull] string? argument,
		[CallerArgumentExpression("argument")] string? paramName = null
	)
	{
		ArgumentNotNull(argument, paramName);
		if (!string.IsNullOrWhiteSpace(argument))
		{
			return;
		}

		throw new ArgumentException("String cannot be empty or white space.", paramName);
	}

	/// <summary>
	///     Проверяет, что аргумент находится в указанном промежутке.
	/// </summary>
	/// <param name="argument">Аргумент.</param>
	/// <param name="startRange">Начало промежутка.</param>
	/// <param name="endRange">Конец промежутка.</param>
	/// <param name="paramName">Наименование аргумента.</param>
	/// <exception cref="ArgumentNullException">Выбрасывается если аргумент равен null.</exception>
	/// <exception cref="ArgumentException">Выбрасывается если аргумент пустой или содержит только пробелы.</exception>
	public static void ArgumentOutOfRange<T>(
		T argument,
		T startRange,
		T endRange,
		[CallerArgumentExpression("argument")] string? paramName = null
	) where T : IComparable
	{
		ArgumentNotNull(argument, paramName);
		// -1 - текущее значение меньше переданного
		// 0 - значение я равны
		// 1 - текущее значение больше переданного
		if (argument.CompareTo(startRange) == -1 || argument.CompareTo(endRange) == 1)
		{
			throw new ArgumentOutOfRangeException(paramName);
		}
	}
}
