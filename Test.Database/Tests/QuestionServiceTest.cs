using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyOwnGame.Models;
using System.Threading.Tasks;
using MyOwnGame.Core.Services.DataProvider;
using System.Linq;
using MyOwnGame.Core.Services.DataProvider.DbServices.Interfaces;
using MyOwnGame.Core.Services.DataProvider.DbServices;
using System.Diagnostics;
using System.Collections.Generic;

namespace Test.Database
{
	[TestClass]
	public class QuestionServiceTest
	{
		#region Fields
		private IQuestionService _questionService;
		private GameContext _db = new GameContext();
		#endregion

		#region Ctors
		public QuestionServiceTest()
		{
			_questionService = new QuestionService(_db);
		}
		#endregion

		#region TestMethods
		[TestMethod]
		public async Task GetByIdAsyncTest()
		{
			var question = new Question
			{
				Text = "title",
				Price = 100
			};


			_db.Questions.Add(question);
			_db.SaveChanges();

			var q = await _questionService.GetByIdAsync(question.Id);

			_db.Questions.Remove(question);
			_db.SaveChanges();

			Assert.AreEqual(question, q);
		}

		[TestMethod]
		public async Task GetAsyncTest()
		{
			var q1 = new Question
			{
				Text = "First",
				Price = 100
			};
			var q2 = new Question
			{
				Text = "Second",
				Price = 200
			};

			_db.Questions.Add(q1);
			_db.Questions.Add(q2);

			_db.SaveChanges();

			var list = new List<Question> { q1, q2 };

			var questions = await _questionService.GetAsync();

			_db.Questions.Remove(q1);
			_db.Questions.Remove(q2);

			_db.SaveChanges();

			Assert.IsTrue(Enumerable.SequenceEqual(list,questions));
		}

		[TestMethod]
		public async Task AddAsyncTest()
		{
			var question = new Question
			{
				Text = "AddMethod",
				Price = 300
			};
			var topic = new Topic
			{
				Name = "Add"
			};

			_db.Topics.Add(topic);
			_db.SaveChanges();

			var flag = await _questionService.AddAsync(topic.Id, question);

			_db.Questions.Remove(question);
			_db.Topics.Remove(topic);
			_db.SaveChanges();

			Assert.IsTrue(flag);
		}

		[TestMethod]
		public async Task RemoveAsyncTest()
		{
			var q = new Question
			{
				Text = "Text",
				Price = 100
			};

			_db.Questions.Add(q);
			_db.SaveChanges();

			Assert.IsTrue(await _questionService.RemoveAsync(q.Id));
		}

		[TestMethod]
		public async Task RemoveAllAsyncTest()
		{
			var q = new Question
			{
				Text = "Text",
				Price = 100
			};

			_db.Questions.Add(q);
			_db.SaveChanges();

			Assert.IsTrue(await _questionService.RemoveAllAsync());
		} 
		#endregion
	}
}
