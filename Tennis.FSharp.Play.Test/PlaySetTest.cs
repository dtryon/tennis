using System;
using System.Linq;
using NUnit.Framework;
using Moq;
using Tennis.FSharp.Logic;

namespace Tennis.FSharp.Play.Test
{
    [TestFixture()]
    public class PlaySetTest
    {
        IPlaySet target;
        Mock<IPlayGame> playGame;

        [SetUp]
        public void Setup()
        {
            playGame = new Mock<IPlayGame>();
            target = new PlaySet(playGame.Object);
        }

        [TearDown]
        public void TearDown()
        {
            playGame.Verify();
            target = null;
        }

        [Test()]
        public void Side_One_Wins()
        {
            //Assert
            playGame.Setup(g => g.Play()).Returns(Side.One);

            //Act
            var result = target.Play();

            //Assert
            Assert.AreEqual(Side.One, result);
            Assert.AreEqual(7, target.GetGameScores().Count());
        }

        [Test()]
        public void Side_Two_Wins()
        {
            //Assert
            playGame.Setup(g => g.Play()).Returns(Side.Two);

            //Act
            var result = target.Play();

            //Assert
            Assert.AreEqual(Side.Two, result);
            Assert.AreEqual(7, target.GetGameScores().Count());
        }
    }
}

