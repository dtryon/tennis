using NUnit.Framework;

namespace Tennis.FSharp.Logic.Test
{
	[TestFixture]
	public class GameTest
	{
		Game target;
		
		[SetUp]
		public void Setup()
		{
			target = new Game();
		}
		
		[TearDown]
		public void TearDown()
		{
			target = null;
		}

        [Test]
        public void NewGame_Score_Love_To_Love()
        {
            //Assert
            Assert.AreEqual(PointState.Love, target.SideOneScore);
            Assert.AreEqual(PointState.Love, target.SideTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, target.State);
            Assert.AreEqual("love - love", target.PrintScore());
        }

        [Test]
        public void SideOne_Wins_Point_Score_Is_Fifteen_To_Love()
        {
            //Act
            SideOneWinsPoints(1);

            //Assert
            Assert.AreEqual(PointState.Fifteen, target.SideOneScore);
            Assert.AreEqual(PointState.Love, target.SideTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, target.State);
            Assert.AreEqual("fifteen - love", target.PrintScore());
        }

        [Test]
        public void SideOne_Wins_Game_To_Love()
        {
            //Act
            SideOneWinsPoints(4);

            //Assert
            Assert.AreEqual(GameState.GameWonBySideOne, target.State);
            Assert.AreEqual("game - side one", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Game_To_Love()
        {
            //Act
            SideTwoWinsPoints(4);

            //Assert
            Assert.AreEqual(GameState.GameWonBySideTwo, target.State);
            Assert.AreEqual("game - side two", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Point_Score_Is_Fifteen_To_Fifteen()
        {
            //Act
            SideOneWinsPoints(1);
            SideTwoWinsPoints(1);


            //Assert
            Assert.AreEqual(PointState.Fifteen, target.SideOneScore);
            Assert.AreEqual(PointState.Fifteen, target.SideTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, target.State);
            Assert.AreEqual("fifteen - fifteen", target.PrintScore());
        }

        [Test]
        public void SideOne_Wins_Point_Score_Is_Thirty_To_Fifteen()
        {
            //Act
            SideOneWinsPoints(2);
            SideTwoWinsPoints(1);


            //Assert
            Assert.AreEqual(PointState.Thirty, target.SideOneScore);
            Assert.AreEqual(PointState.Fifteen, target.SideTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, target.State);
            Assert.AreEqual("thirty - fifteen", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Point_Score_Is_Thirty_To_Thirty()
        {
            //Act
            SideOneWinsPoints(2);
            SideTwoWinsPoints(2);


            //Assert
            Assert.AreEqual(PointState.Thirty, target.SideOneScore);
            Assert.AreEqual(PointState.Thirty, target.SideTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, target.State);
            Assert.AreEqual("thirty - thirty", target.PrintScore());
        }

        [Test]
        public void SideOne_Wins_Point_Score_Is_Forty_To_Thirty()
        {
            //Act
            SideOneWinsPoints(3);
            SideTwoWinsPoints(2);


            //Assert
            Assert.AreEqual(PointState.Forty, target.SideOneScore);
            Assert.AreEqual(PointState.Thirty, target.SideTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, target.State);
            Assert.AreEqual("forty - thirty", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Point_Score_Is_Deuce()
        {
            //Act
            SideOneWinsPoints(3);
            SideTwoWinsPoints(3);


            //Assert
            Assert.AreEqual(PointState.Deuce, target.SideOneScore);
            Assert.AreEqual(PointState.Deuce, target.SideTwoScore);
            Assert.AreEqual(GameState.Deuce, target.State);
            Assert.AreEqual("deuce", target.PrintScore());
        }

        [Test]
        public void SideOne_Wins_Point_Score_Is_Advantage_One()
        {
            //Act
            GetToDeuce();
            SideOneWinsPoints(1);


            //Assert
            Assert.AreEqual(GameState.Deuce, target.State);
            Assert.AreEqual("advantage - side one", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Point_Score_Is_2nd_Deuce()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(1);


            //Assert
            Assert.AreEqual(GameState.Deuce, target.State);
            Assert.AreEqual("deuce", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Point_Score_Is_Advantage_Two()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(1);
            SideTwoWinsPoints(1);


            //Assert
            Assert.AreEqual(GameState.Deuce, target.State);
            Assert.AreEqual("advantage - side two", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Game()
        {
            //Act
            GetToDeuce();
            SideTwoWinsPoints(2);


            //Assert
            Assert.AreEqual(GameState.GameWonBySideTwo, target.State);
            Assert.AreEqual("game - side two", target.PrintScore());
        }

        [Test]
        public void SideOne_Wins_Game()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(1);
            SideOneWinsPoints(2);


            //Assert
            Assert.AreEqual(GameState.GameWonBySideOne, target.State);
            Assert.AreEqual("game - side one", target.PrintScore());
        }

        [Test]
        public void SideOne_Long_Game_Has_Advantage()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(20);
            SideOneWinsPoints(1);

            //Assert
            Assert.AreEqual(GameState.Deuce, target.State);
            Assert.AreEqual("advantage - side one", target.PrintScore());
        }

        [Test]
        public void SideTwo_Long_Game_Has_Advantage()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(45);
            SideTwoWinsPoints(1);

            //Assert
            Assert.AreEqual(GameState.Deuce, target.State);
            Assert.AreEqual("advantage - side two", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Long_Game()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(45);
            SideTwoWinsPoints(2);


            //Assert
            Assert.AreEqual(GameState.GameWonBySideTwo, target.State);
            Assert.AreEqual("game - side two", target.PrintScore());
        }

        private void GetToDeuce()
        {
            SideOneWinsPoints(3);
            SideTwoWinsPoints(3);
        }

        private void HoldAtDeuce(int times)
        {
            for (int i = 0; i < times; i++)
            {
                SideOneWinsPoints(1);
                SideTwoWinsPoints(1);
            }
        }

        private void SideOneWinsPoints(int points)
        {
            for (int i = 0; i < points; i++)
            {
                target.WinPoint(s => Side.One);
            }
        }

        private void SideTwoWinsPoints(int points)
        {
            for (int i = 0; i < points; i++)
            {
                target.WinPoint(s => Side.Two);
            }
        }
	}
}

 