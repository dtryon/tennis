using System;
using Tennis.Logic;
using System.Collections.Generic;

namespace Tennis.Play
{
	public class PlayGame : IPlayGame
	{
		private readonly IDetermineWinner determineWinner;
		private Game game;
		private List<string> pointScores;
		
		public PlayGame (IDetermineWinner determineWinner)
		{
			if (determineWinner == null)
			{
				throw new ArgumentNullException("determineWinner");
			}
			
			this.determineWinner = determineWinner;
		}
		
		public PlayGame(ISide sideOne, ISide sideTwo) : this(new DetermineWinner(sideOne, sideTwo))
		{
		}
		
		public Side Play()
		{
			game = new Game();
			pointScores = new List<string>();
			pointScores.Add(game.PrintScore());
			
			while(game.State != GameState.GameWonBySideOne && game.State != GameState.GameWonBySideTwo)
			{
				var side  = determineWinner.ForPoint();
				game.WinPoint(s => side);
				pointScores.Add(game.PrintScore());
			}
			
			if (game.State == GameState.GameWonBySideOne)
			{
				return Side.One;
			}
			else
			{
				return Side.Two;
			}
		}
		
		public List<string> GetPointScores()
		{
			return pointScores;
		}
		
		
	}
}

