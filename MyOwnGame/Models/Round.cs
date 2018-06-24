using System.Collections.Generic;

namespace MyOwnGame.Models
{
	/// <summary>
	/// Раунд.
	/// </summary>
	public class Round
	{
		public Round()
		{
			Topics = new List<Topic>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		#region Reference
		public List<Topic> Topics { get; set; }
		#endregion
	}
}
