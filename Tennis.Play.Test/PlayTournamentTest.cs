using System;
using NUnit.Framework;
using System.Collections.Generic;
using Moq;
using Tennis.Logic;

namespace Tennis.Play.Test
{
	[TestFixture()]
	public class PlayTournamentTest
	{
		IPlayTournament target;
		List<IPlayMatch> playMatches;
		Mock<IPlayMatch> playMatch;
		
		[SetUp]
		public void Setup()
		{
			playMatch = new Mock<IPlayMatch>();
			playMatches = new List<IPlayMatch>();
			playMatches.Add(playMatch.Object);
			target = new PlayTournament(playMatches);
		}
		
		[TearDown]
		public void TearDown()
		{
			playMatch.VerifyAll();
			target = null;
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Null_Sides_Contract_Checked ()
		{
			//Act
			target = new PlayTournament(null);
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void Constructor_Empty_Sides_Contract_Checked ()
		{
			//Arrange
			var matches = new List<IPlayMatch>();
			
			//Act
			target = new PlayTournament(matches);
		}
		
		[Test()]
		public void Play_2_Sides_Winner_Determined ()
		{
			//Arrange
			target = new PlayTournament(playMatches);
			Mock<ISide> winner = new Mock<ISide>();
			playMatch.Setup<ISide>(m => m.Play()).Returns(winner.Object);
			
			//Act
			ISide result = target.Play();
			
			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(winner.Object, result);
			playMatch.Verify(m => m.Play(), Times.AtMostOnce());
			Assert.AreEqual(1, target.GetMatchScores().Count);
			
		}
		
		[Test()]
		public void Play_4_Sides_Winner_Determined ()
		{
			//Arrange
			Mock<IPlayMatch> anotherMatch = new Mock<IPlayMatch>();
			playMatches.Add(anotherMatch.Object);
			
			target = new PlayTournament(playMatches);
			
			Mock<ISide> winner = new Mock<ISide>();
			winner.SetupProperty<int>(w => w.Strength, 33);
			
			playMatch.Setup<ISide>(m => m.Play()).Returns(winner.Object);
			anotherMatch.Setup<ISide>(m => m.Play()).Returns(winner.Object);
			
			//Act
			ISide result = target.Play();
			
			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(winner.Object, result);
			playMatch.Verify(m => m.Play(), Times.Exactly(1));
			Assert.AreEqual(3, target.GetMatchScores().Count);
		}
		
		
		private List<ISide> GetSides(int numberOfSides)
		{
			List<ISide> result = new List<ISide>();
			
			Random random = new Random();
			var strength = random.Next(100);
			
			for (int i = 0; i < numberOfSides; i++) {
				Mock<ISide> side = new Mock<ISide>();
				side.Setup(s => s.Strength).Returns(strength);
				
				result.Add(side.Object);
			}
			
			return result;
		}
	}
}

