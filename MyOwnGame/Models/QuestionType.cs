using System.ComponentModel;

namespace MyOwnGame.Models
{
	/// <summary>
	/// Виды вопросов.
	/// </summary>
	public enum QuestionType
	{
		[Description("Вопрос")]
		Question,

		[Description("Аукцион")]
		Auction
	}
}