using MyOwnGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnGame.Core.Services.DataProvider
{
    public class QuestionService : IQuestionService
	{
		#region Fields
		private GameContext _db;
		#endregion

		#region Ctors
		public QuestionService(GameContext db)
		{
			_db = db;
		}
		#endregion

		#region Methods
		public Task<bool> AddAsync(int tId, Question question)
		{
			return Task.Run(() =>
			{
				try
				{
					_db.Topics.SingleOrDefault(t => t.Id == tId).Questions.Add(question);
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

		public Task<List<Question>> GetAsync()
		{
			return Task.Run(() =>
			{
				try
				{
					return _db.Questions.ToList();
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					return null;
				}
			});
		}

		public Task<Question> GetByIdAsync(int id)
		{
			return Task.Run(() =>
			{
				try
				{
					return _db.Questions.SingleOrDefault(q => q.Id == id);
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
					var question = _db.Questions.SingleOrDefault(q => q.Id == id);
					_db.Questions.Remove(question);
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
