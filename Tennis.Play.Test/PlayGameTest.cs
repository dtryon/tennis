using System;
using Tennis.Play;
using Tennis.Logic;
using Moq;
using NUnit.Framework;

namespace Tennis.Play.Test
{
	[TestFixture]
	public class GamePlayTest
	{
		IPlayGame target;
		Mock<IDetermineWinner> determineWinner;
		
		[SetUp]
		public void Setup()
		{
			determineWinner = new Mock<IDetermineWinner>();
			target = new PlayGame(determineWinner.Object);
		}
		
		[TearDown]
		public void TearDown()
		{
			determineWinner.Verify();
			target = null;
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GamePlay_Constructor_Null_IDetermineWinner_Contract_Checked ()
		{	
			//Act
			target = new PlayGame(null);
		}
		
		[Test]
		public void GamePlay_Side_One_Wins ()
		{	
			//Arrange
			determineWinner.Setup(w => w.ForPoint())
    			.Returns(Side.One);
			
			//Act
			var result = target.Play();
			
			//Assert
			Assert.AreEqual(Side.One, result);
			Assert.AreEqual(5, target.GetPointScores().Count);
		}
		
		[Test]
		public void GamePlay_Side_Two_Wins ()
		{	
			//Arrange
			determineWinner.Setup(w => w.ForPoint())
    			.Returns(Side.Two);
			
			//Act
			var result = target.Play();
			
			//Assert
			Assert.AreEqual(result, Side.Two);
			Assert.AreEqual(5, target.GetPointScores().Count);
		}
	}
}