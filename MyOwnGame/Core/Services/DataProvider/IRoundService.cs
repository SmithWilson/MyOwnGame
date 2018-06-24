using MyOwnGame.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOwnGame.Core.Services.DataProvider
{
    public interface IRoundService
	{
		/// <summary>
		/// Получение раунда по id.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <returns>Раунд.</returns>
		Task<Round> GetByIdAsync(int id);

		/// <summary>
		/// Получение списка раундов.
		/// </summary>
		/// <returns>Список раундов.</returns>
		Task<List<Round>> GetAsync();

		/// <summary>
		/// Добавление раунда.
		/// </summary>
		/// <param name="question">Раунд.</param>
		/// <returns>Логическое значение.</returns>
		Task<bool> AddAsync(Round question);

		/// <summary>
		/// Удаление раунда.
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns>Логическое значение.</returns>
		Task<bool> RemoveAsync(int id);

		/// <summary>
		/// Удаление раундов.
		/// </summary>
		/// <returns>Логическое значение.</returns>
		Task<bool> RemoveAllAsync();
	}
}
