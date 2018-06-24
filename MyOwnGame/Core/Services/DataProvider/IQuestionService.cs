using MyOwnGame.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOwnGame.Core.Services.DataProvider
{
    public interface IQuestionService
	{
		/// <summary>
		/// Получение вопроса по id.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <returns>Вопрос.</returns>
		Task<Question> GetByIdAsync(int id);

		/// <summary>
		/// Получение списка вопросов.
		/// </summary>
		/// <returns>Список вопросов.</returns>
		Task<List<Question>> GetAsync();

		/// <summary>
		/// Добавление вопроса.
		/// </summary>
		/// <param name="question">Вопрос.</param>
		/// <returns>Логическое значение.</returns>
		Task<bool> AddAsync(int tId, Question question);

		/// <summary>
		/// Удаление вопроса.
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns>Логическое значение.</returns>
		Task<bool> RemoveAsync(int id);

		/// <summary>
		/// Удаление вопросов.
		/// </summary>
		/// <returns>Логическое значение.</returns>
		Task<bool> RemoveAllAsync();
	}
}
