using MyOwnGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnGame.Core.Services.DataProvider
{
    public class RoundService : IRoundService
	{
		#region Fields
		private GameContext _db;
		#endregion

		#region Ctors
		public RoundService(GameContext db)
		{
			_db = db;
		}
		#endregion

		#region Methods
		public Task<bool> AddAsync(Round round)
		{
			return Task.Run(() =>
			{
				try
				{
					_db.Rounds.Add(round);
					_db.SaveChanges();

					return true;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					return false;
				}
			});
		}

		public Task<List<Round>> GetAsync()
		{
			return Task.Run(() =>
			{
				try
				{
					return _db.Rounds.Include("Topics.Questions").ToList();
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					return null;
				}
			});
		}

		public Task<Round> GetByIdAsync(int id)
		{
			return Task.Run(() =>
			{
				try
				{
					return _db.Rounds.Include("Topics.Questions").SingleOrDefault(r => r.Id == id);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					return null;
				}
			});
		}

		public Task<bool> RemoveAllAsync()
		{
			return Task.Run(() =>
			{
				try
				{
					_db.Questions.RemoveRange(_db.Questions);
					_db.Topics.RemoveRange(_db.Topics);
					_db.Rounds.RemoveRange(_db.Rounds);
					_db.SaveChanges();

					return true;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					return false;
				}
			});
		}

		public Task<bool> RemoveAsync(int id)
		{
			return Task.Run(() =>
			{
				try
				{
					var round = _db.Rounds.Include("Topics.Questions").SingleOrDefault(r => r.Id == id);
					foreach (var topic in round.Topics)
					{
						_db.Questions.RemoveRange(topic.Questions);
					}

					_db.Topics.RemoveRange(round.Topics);
					_db.Rounds.Remove(round);
					_db.SaveChanges();

					return true;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					return false;
				}
			});
		} 
		#endregion
	}
}
