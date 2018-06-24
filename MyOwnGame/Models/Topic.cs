using System.Collections.Generic;

namespace MyOwnGame.Models
{
	/// <summary>
	/// Тема.
	/// </summary>
	public class Topic
	{
		public Topic()
		{
			Questions = new List<Question>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		#region Reference
		public List<Question> Questions { get; set; }

		public int? RoundId { get; set; }

		public Round Round { get; set; } 
		#endregion
	}
}