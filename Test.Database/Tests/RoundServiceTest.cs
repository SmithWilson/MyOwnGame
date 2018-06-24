using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyOwnGame.Core.Services.DataProvider.DbServices.Interfaces;
using MyOwnGame.Core.Services.DataProvider;
using MyOwnGame.Core.Services.DataProvider.DbServices;
using MyOwnGame.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Database.Tests
{
	[TestClass]
	public class RoundServiceTest
	{
		#region Fields
		private IRoundService _roundService;
		private GameContext _db = new GameContext();
		#endregion

		#region Ctors
		public RoundServiceTest()
		{
			_roundService = new RoundService(_db);
		}
		#endregion

		#region TestMethods
		[TestMethod]
		public async Task GetByIdAsyncTest()
		{
			var round = new Round
			{
				Name = "First",
				Topics = new List<Topic>
				{
					new Topic
					{
						Name = "C#",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 100
							}
						}
					},
					new Topic
					{
						Name = "Java",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 200
							}
						}
					}
				}
			};

			_db.Rounds.Add(round);
			_db.SaveChanges();

			var actual = await _roundService.GetByIdAsync(round.Id);
			await _roundService.RemoveAsync(round.Id);

			Assert.AreEqual(round, actual);
		}

		[TestMethod]
		public async Task GetAsyncTest()
		{
			var r1 = new Round
			{
				Name = "First",
				Topics = new List<Topic>
				{
					new Topic
					{
						Name = "C#",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 100
							}
						}
					},
					new Topic
					{
						Name = "Java",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 200
							}
						}
					}
				}
			};
			var r2 = new Round
			{
				Name = "Second",
				Topics = new List<Topic>
				{
					new Topic
					{
						Name = "C#",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 100
							}
						}
					},
					new Topic
					{
						Name = "Java",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 200
							}
						}
					}
				}
			};

			_db.Rounds.Add(r1);
			_db.Rounds.Add(r2);

			_db.SaveChanges();

			var list = new List<Round> { r1, r2 };

			var rounds = await _roundService.GetAsync();

			_db.Rounds.Remove(r1);
			_db.Rounds.Remove(r2);

			_db.SaveChanges();

			Assert.IsTrue(Enumerable.SequenceEqual(list, rounds));
		}

		[TestMethod]
		public async Task AddAsyncTest()
		{
			var round = new Round
			{
				Name = "First",
				Topics = new List<Topic>
				{
					new Topic
					{
						Name = "C#",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 100
							}
						}
					},
					new Topic
					{
						Name = "Java",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 200
							}
						}
					}
				}
			};

			var flag = await _roundService.AddAsync(round);
			await _roundService.RemoveAsync(round.Id);
			Assert.IsTrue(flag);
		}

		[TestMethod]
		public async Task RemoveAsyncTest()
		{
			var round = new Round
			{
				Name = "First",
				Topics = new List<Topic>
				{
					new Topic
					{
						Name = "C#",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 100
							}
						}
					},
					new Topic
					{
						Name = "Java",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 200
							}
						}
					}
				}
			};

			_db.Rounds.Add(round);
			_db.SaveChanges();

			Assert.IsTrue(await _roundService.RemoveAsync(round.Id));
		}

		[TestMethod]
		public async Task RemoveAllAsyncTest()
		{
			var round = new Round
			{
				Name = "First",
				Topics = new List<Topic>
				{
					new Topic
					{
						Name = "C#",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 100
							}
						}
					},
					new Topic
					{
						Name = "Java",
						Questions = new List<Question>
						{
							new Question
							{
								Text = "g",
								Price = 200
							}
						}
					}
				}
			};

			_db.Rounds.Add(round);
			_db.SaveChanges();

			Assert.IsTrue(await _roundService.RemoveAllAsync());
		} 
		#endregion
	}
}
