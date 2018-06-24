using MyOwnGame.Models;
using System.Data.Entity;

namespace MyOwnGame.Core.Services.DataProvider
{
	public class GameContext : DbContext
	{
		public GameContext() : base("DefaultConnection")
		{

		}

		public DbSet<Round> Rounds { get; set; }
		
		public DbSet<Topic> Topics { get; set; }

		public DbSet<Question> Questions { get; set; }
	}
}
