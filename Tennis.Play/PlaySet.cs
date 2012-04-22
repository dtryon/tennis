using System;
using Tennis.Logic;
using System.Collections.Generic;

namespace Tennis.Play
{
	public class PlaySet : IPlaySet
	{
		private readonly IPlayGame playGame;
		private Set theSet;
		private List<GameScore> gameScores;
		private ISide sideOne;
		private ISide sideTwo;
		
		public PlaySet (IPlayGame playGame)
		{
			if (playGame == null)
			{
				throw new ArgumentNullException("playGame");
			}
			
			this.playGame = playGame;
		}
		
		public PlaySet(ISide sideOne, ISide sideTwo) : this(new PlayGame(sideOne, sideTwo))
		{
			this.sideOne = sideOne;
			this.sideTwo = sideTwo;
		}
		
		public Side Play()
		{
			theSet = new Set();
			gameScores = new List<GameScore>();
			gameScores.Add(new GameScore() { Score = theSet.PrintScore() });
			SetInitialServingSide();
			
			while(theSet.State != SetState.SetWonBySideOne && theSet.State != SetState.SetWonBySideTwo)
			{
				var gameWinner = playGame.Play();	
				theSet.WinGame(s => gameWinner);
				ToggleServingSide();
				
				gameScores.Add(new GameScore() { Score = theSet.PrintScore(), PointScores = playGame.GetPointScores() });
			}
			
			if (theSet.State == SetState.SetWonBySideOne)
			{
				return Side.One;
			}
			else
			{
				return Side.Two;
			}
		}
		
		
		private void ToggleServingSide()
		{
			if (sideOne != null && sideTwo != null)
			{
				if (sideOne.IsServing)
				{
					sideTwo.IsServing = true;
					sideOne.IsServing = false;
				}
				else
				{
					sideTwo.IsServing = false;
					sideOne.IsServing = true;
				}
			}
		}
		
		private void SetInitialServingSide()
		{
			if (sideOne != null && sideTwo != null)
			{
				Random random = new Random();
				var coinToss = random.Next(1000);
				
				if (coinToss % 2 == 0)
				{
					sideTwo.IsServing = true;
				}
				else
				{
					sideOne.IsServing = true;
				}
			}
		}
		
		public List<GameScore> GetGameScores()
		{
			return gameScores;
		}
	}
}

