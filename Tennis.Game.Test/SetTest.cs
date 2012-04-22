using System;
using NUnit.Framework;
using Tennis.Logic;

namespace Tennis.Logic.Test
{
	[TestFixture]
	public class SetTest
	{
		Set target;
		
		[SetUp]
		public void Setup()
		{
			target = new Set();
		}
		
		[TearDown]
		public void TearDown()
		{
			target = null;
		}
		
		[Test]
		public void SideOne_Wins_Game_Score_Is_1_0 ()
		{
			//Act
			SideOneWinsGames(1);
			
			//Assert
			Assert.AreEqual(1, target.SideOneScore);
			Assert.AreEqual(0, target.SideTwoScore);
			Assert.AreEqual(SetState.Playing, target.State);
			Assert.AreEqual("1 - 0", target.PrintScore());
		}
		
		[Test]
		public void SideTwo_Wins_Game_Score_Is_1_1 ()
		{
			//Act
			SideOneWinsGames(1);
			SideTwoWinsGames(1);
			
			//Assert
			Assert.AreEqual(1, target.SideOneScore);
			Assert.AreEqual(1, target.SideTwoScore);
			Assert.AreEqual(SetState.Playing, target.State);
			Assert.AreEqual("1 - 1", target.PrintScore());
		}
		
		[Test]
		public void SideOne_Wins_Set_Score_Is_6_0 ()
		{
			//Act
			SideOneWinsGames(6);
			
			//Assert
			Assert.AreEqual(6, target.SideOneScore);
			Assert.AreEqual(0, target.SideTwoScore);
			Assert.AreEqual(SetState.SetWonBySideOne, target.State);
			Assert.AreEqual("6 - 0", target.PrintScore());
		}
		
		[Test]
		public void SideTwo_Wins_Set_Score_Is_0_6 ()
		{
			//Act
			SideTwoWinsGames(6);
			
			//Assert
			Assert.AreEqual(0, target.SideOneScore);
			Assert.AreEqual(6, target.SideTwoScore);
			Assert.AreEqual(SetState.SetWonBySideTwo, target.State);
			Assert.AreEqual("0 - 6", target.PrintScore());
		}
		
		[Test]
		public void SideOne_Wins_Game_Score_Is_6_5 ()
		{
			//Act
			MoveTo5All();
			SideOneWinsGames(1);
			
			//Assert
			Assert.AreEqual(6, target.SideOneScore);
			Assert.AreEqual(5, target.SideTwoScore);
			Assert.AreEqual(SetState.Playing, target.State);
			Assert.AreEqual("6 - 5", target.PrintScore());
		}
		
		[Test]
		public void SideOne_Wins_Set_Score_Is_7_5 ()
		{
			//Act
			MoveTo5All();
			SideOneWinsGames(2);
			
			//Assert
			Assert.AreEqual(7, target.SideOneScore);
			Assert.AreEqual(5, target.SideTwoScore);
			Assert.AreEqual(SetState.SetWonBySideOne, target.State);
			Assert.AreEqual("7 - 5", target.PrintScore());
		}
		
		[Test]
		public void SideTwo_Wins_Set_Score_Is_6_8 ()
		{
			//Act
			MoveTo5All();
			SideOneWinsGames(1);
			SideTwoWinsGames(3);
			
			//Assert
			Assert.AreEqual(6, target.SideOneScore);
			Assert.AreEqual(8, target.SideTwoScore);
			Assert.AreEqual(SetState.SetWonBySideTwo, target.State);
			Assert.AreEqual("6 - 8", target.PrintScore());
		}
		
		[Test]
		public void NewSet_Game_Score_0_0 ()
		{			
			//Assert
			Assert.AreEqual(0, target.SideOneScore);
			Assert.AreEqual(0, target.SideTwoScore);
			Assert.AreEqual(SetState.Playing, target.State);
			Assert.AreEqual("0 - 0", target.PrintScore());
		}
		
		private void MoveTo5All()
		{
			SideOneWinsGames(5);
			SideTwoWinsGames(5);
		}
		
		private void SideOneWinsGames(int points)
		{
			for (int i = 0; i < points; i++) {
				target.WinGame(s => Side.One);
			}
		}
		
		private void SideTwoWinsGames(int points)
		{
			for (int i = 0; i < points; i++) {
				target.WinGame(s => Side.Two);
			}
		}
	}
}

