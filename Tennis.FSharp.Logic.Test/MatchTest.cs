using NUnit.Framework;

namespace Tennis.FSharp.Logic.Test
{
    [TestFixture()]
    public class MatchTest
    {
        Match target;

        [SetUp]
        public void Setup()
        {
            target = new Match();
        }

        [TearDown]
        public void TearDown()
        {
            target = null;
        }

        [Test]
        public void NewMatch_Score_Is_0_0()
        {
            //Assert
            Assert.AreEqual(0, target.SideOneScore);
            Assert.AreEqual(0, target.SideTwoScore);
            Assert.AreEqual(MatchState.Playing, target.State);
            Assert.AreEqual("0 - 0", target.PrintScore());
        }

        [Test]
        public void SideOne_Wins_Set_Score_Is_1_0()
        {
            //Act
            SideOneWinsGames(1);

            //Assert
            Assert.AreEqual(1, target.SideOneScore);
            Assert.AreEqual(0, target.SideTwoScore);
            Assert.AreEqual(MatchState.Playing, target.State);
            Assert.AreEqual("1 - 0", target.PrintScore());
        }

        [Test]
        public void SideOne_Wins_Set_Score_Is_2_0_Match_Won()
        {
            //Act
            SideOneWinsGames(2);

            //Assert
            Assert.AreEqual(2, target.SideOneScore);
            Assert.AreEqual(0, target.SideTwoScore);
            Assert.AreEqual(MatchState.MatchWonBySideOne, target.State);
            Assert.AreEqual("2 - 0", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Set_Score_Is_0_1()
        {
            //Act
            SideTwoWinsGames(1);

            //Assert
            Assert.AreEqual(0, target.SideOneScore);
            Assert.AreEqual(1, target.SideTwoScore);
            Assert.AreEqual(MatchState.Playing, target.State);
            Assert.AreEqual("0 - 1", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Set_Score_Is_0_2_Match_Won()
        {
            //Act
            SideTwoWinsGames(2);

            //Assert
            Assert.AreEqual(0, target.SideOneScore);
            Assert.AreEqual(2, target.SideTwoScore);
            Assert.AreEqual(MatchState.MatchWonBySideTwo, target.State);
            Assert.AreEqual("0 - 2", target.PrintScore());
        }

        [Test]
        public void SideTwo_Wins_Set_Score_Is_1_2_Match_Won()
        {
            //Act
            SideTwoWinsGames(1);
            SideOneWinsGames(1);
            SideTwoWinsGames(1);

            //Assert
            Assert.AreEqual(1, target.SideOneScore);
            Assert.AreEqual(2, target.SideTwoScore);
            Assert.AreEqual(MatchState.MatchWonBySideTwo, target.State);
            Assert.AreEqual("1 - 2", target.PrintScore());
        }

        private void SideOneWinsGames(int points)
        {
            for (int i = 0; i < points; i++)
            {
                target.WinSet(s => Side.One);
            }
        }

        private void SideTwoWinsGames(int points)
        {
            for (int i = 0; i < points; i++)
            {
                target.WinSet(s => Side.Two);
            }
        }
    }
}

