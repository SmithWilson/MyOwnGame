using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyOwnGame.Core.Services.DataProvider.DbServices.Interfaces;
using MyOwnGame.Core.Services.DataProvider;
using MyOwnGame.Core.Services.DataProvider.DbServices;
using System.Threading.Tasks;
using MyOwnGame.Models;
using System.Collections.Generic;
using System.Linq;

namespace Test.Database.Tests
{
	[TestClass]
	public class TopicServiceTest
	{
		#region Fields
		private ITopicService _topicService;
		private GameContext _db = new GameContext();
		#endregion

		#region Ctors
		public TopicServiceTest()
		{
			_topicService = new TopicService(_db);
		}
		#endregion

		#region TestMethods
		[TestMethod]
		public async Task GetByIdAsyncTest()
		{
			var topic = new Topic
			{
				Name = "C#",
				Questions = new List<Question>
				{
					new Question
					{
						Text = "get",
						Price = 100
					},
					new Question
					{
						Text = "get",
						Price = 200
					}
				}
			};


			_db.Topics.Add(topic);
			_db.SaveChanges();

			var t = await _topicService.GetByIdAsync(topic.Id);

			await _topicService.RemoveAsync(topic.Id);

			Assert.AreEqual(topic, t);
		}

		[TestMethod]
		public async Task GetAsyncTest()
		{
			var t1 = new Topic
			{
				Questions = new List<Question>
				{
					new Question
					{
						Text = "t",
						Price = 200
					}
				}
			};
			var t2 = new Topic
			{
				Questions = new List<Question>
				{
					new Question
					{
						Text = "r",
						Price = 100
					}
				}
			};

			_db.Topics.Add(t1);
			_db.Topics.Add(t2);

			_db.SaveChanges();

			var list = new List<Topic> { t1, t2 };

			var topics = await _topicService.GetAsync();

			_db.Topics.Remove(t1);
			_db.Topics.Remove(t2);

			_db.SaveChanges();

			Assert.IsTrue(Enumerable.SequenceEqual(list, topics));
		}

		[TestMethod]
		public async Task AddAsyncTest()
		{
			var topic = new Topic
			{
				Name = "C#",
				Questions = new List<Question>
				{
					new Question
					{
						Text = "add",
						Price = 100
					},
					new Question
					{
						Text = "add",
						Price = 200
					}
				}
			};
			var round = new Round
			{
				Name = "Add"
			};

			_db.Rounds.Add(round);
			_db.SaveChanges();
			
			var flag = await _topicService.AddAsync(round.Id, topic);

			await _topicService.RemoveAsync(topic.Id);
			_db.Rounds.Remove(round);

			Assert.IsTrue(flag);
		}

		[TestMethod]
		public async Task RemoveAsyncTest()
		{
			var t = new Topic
			{
				Name = "C#",
				Questions = new List<Question>
				{
					new Question
					{
						Text = "r",
						Price = 100
					},
					new Question
					{
						Text = "r",
						Price = 200
					}
				}
			};

			_db.Topics.Add(t);
			_db.SaveChanges();

			Assert.IsTrue(await _topicService.RemoveAsync(t.Id));
		}

		[TestMethod]
		public async Task RemoveAllAsyncTest()
		{
			var t = new Topic
			{
				Name = "C#",
				Questions = new List<Question>
				{
					new Question
					{
						Text = "ra",
						Price = 100
					},
					new Question
					{
						Text = "ra",
						Price = 200
					}
				}
			};

			_db.Topics.Add(t);
			_db.SaveChanges();

			Assert.IsTrue(await _topicService.RemoveAllAsync());
		} 
		#endregion
	}
}
