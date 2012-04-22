using System;
using NUnit.Framework;
using Moq;
using Tennis.Logic;
using Tennis.Play;

namespace Tennis.Play.Test
{
	[TestFixture()]
	public class PlayMatchTest
	{
		IPlayMatch target;
		Mock<IPlaySet> playSet;
		Mock<ISide> sideOne;
		Mock<ISide> sideTwo;
		
		[SetUp]
		public void Setup()
		{
			playSet = new Mock<IPlaySet>();
			sideOne = new Mock<ISide>();
			sideTwo = new Mock<ISide>();
			target = new PlayMatch(playSet.Object, sideOne.Object, sideTwo.Object);
		}
		
		[TearDown]
		public void TearDown()
		{
			playSet.Verify();
			target = null;
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Null_IPlaySet_Contract_Checked ()
		{
			//Act
			target = new PlayMatch(null, sideOne.Object, sideTwo.Object);
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Null_SideOne_Contract_Checked ()
		{
			//Act
			target = new PlayMatch(playSet.Object, null, sideTwo.Object);
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_Null_SideTwo_Contract_Checked ()
		{
			//Act
			target = new PlayMatch(playSet.Object, sideOne.Object, null);
		}
		
		[Test()]
		public void Side_One_Wins ()
		{
			//Arrage
			playSet.Setup(s => s.Play()).Returns(Side.One);		
			
			//Act
			var result = target.Play();
			
			//Assert
			Assert.AreEqual(sideOne.Object, result);
			Assert.AreEqual(3, target.GetSetScores().Count);
		}
		
		[Test()]
		public void Side_Two_Wins ()
		{
			//Arrage
			playSet.Setup(s => s.Play()).Returns(Side.Two);		
			
			//Act
			var result = target.Play();
			
			//Assert
			Assert.AreEqual(sideTwo.Object, result);
			Assert.AreEqual(3, target.GetSetScores().Count);
		}
		
		[Test()]
		public void Match_Integration ()
		{
			//Arrange
			Mock<ISide> sideOne = new Mock<ISide>();
			sideOne.Setup(s => s.Strength).Returns(40);
			Mock<ISide> sideTwo = new Mock<ISide>();
			sideTwo.Setup(s => s.Strength).Returns(50);
			
			target = new PlayMatch(sideOne.Object, sideTwo.Object);
			
			//Act
			var result = target.Play();
			
			//Assert
			Assert.AreNotEqual(sideOne.Object, result);
		}
	}
}

