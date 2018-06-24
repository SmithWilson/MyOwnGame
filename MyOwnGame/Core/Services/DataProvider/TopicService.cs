using MyOwnGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnGame.Core.Services.DataProvider
{
    public class TopicService : ITopicService
	{
		#region Fields
		private GameContext _db;
		#endregion

		#region Ctors
		public TopicService(GameContext db)
		{
			_db = db;
		}
		#endregion

		#region Methods
		public Task<bool> AddAsync(int rId, Topic topic)
		{
			return Task.Run(() =>
			{
				try
				{
					_db.Rounds.SingleOrDefault(r => r.Id == rId).Topics.Add(topic);
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

		public Task<List<Topic>> GetAsync()
		{
			return Task.Run(() =>
			{
				try
				{
					return _db.Topics.Include("Questions").ToList();
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					return null;
				}
			});
		}

		public Task<Topic> GetByIdAsync(int id)
		{
			return Task.Run(() =>
			{
				try
				{
					return _db.Topics.Include("Questions").SingleOrDefault(t => t.Id == id);
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
					var topic = _db.Topics.Include("Questions").SingleOrDefault(t => t.Id == id);
					_db.Questions.RemoveRange(topic.Questions);
					_db.Topics.Remove(topic);
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
