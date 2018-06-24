using MyOwnGame.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOwnGame.Core.Services.DataProvider
{
    public interface ITopicService
	{
		/// <summary>
		/// Получение темы по id.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <returns>Тема.</returns>
		Task<Topic> GetByIdAsync(int id);

		/// <summary>
		/// Получение списка тем.
		/// </summary>
		/// <returns>Список тем.</returns>
		Task<List<Topic>> GetAsync();

		/// <summary>
		/// Добавление темы.
		/// </summary>
		/// <param name="topic">Тема.</param>
		/// <returns>Логическое значение.</returns>
		Task<bool> AddAsync(int rId, Topic topic);

		/// <summary>
		/// Удаление темы.
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns>Логическое значение.</returns>
		Task<bool> RemoveAsync(int id);

		/// <summary>
		/// Удаление тем.
		/// </summary>
		/// <returns>Логическое значение.</returns>
		Task<bool> RemoveAllAsync();
	}
}
