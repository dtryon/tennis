using System;
using System.Linq;
using NUnit.Framework;
using Moq;
using Tennis.FSharp.Logic;

namespace Tennis.FSharp.Play.Test
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
        public void Side_One_Wins()
        {
            //Arrage
            playSet.Setup(s => s.Play()).Returns(Side.One);

            //Act
            var result = target.Play();

            //Assert
            Assert.AreEqual(sideOne.Object, result);
            Assert.AreEqual(3, target.GetSetScores().Count());
        }

        [Test()]
        public void Side_Two_Wins()
        {
            //Arrage
            playSet.Setup(s => s.Play()).Returns(Side.Two);

            //Act
            var result = target.Play();

            //Assert
            Assert.AreEqual(sideTwo.Object, result);
            Assert.AreEqual(3, target.GetSetScores().Count());
        }

        [Test()]
        public void Match_Integration()
        {
            //Arrange
            var one = new Mock<ISide>();
            one.Setup(s => s.Strength).Returns(40);
            var two = new Mock<ISide>();
            two.Setup(s => s.Strength).Returns(50);

            var set = new PlaySet(new PlayGame(new DetermineWinner(one.Object, two.Object)));

            target = new PlayMatch(set, one.Object, two.Object);

            //Act
            var result = target.Play();

            //Assert
            Assert.AreNotEqual(sideOne.Object, result);
        }
    }
}

