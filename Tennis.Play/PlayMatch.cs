using System;
using Tennis.Logic;
using System.Collections.Generic;

namespace Tennis.Play
{
	public class PlayMatch : IPlayMatch
	{
		private readonly IPlaySet playSet;
		private readonly ISide sideOne;
		private readonly ISide sideTwo;
		private Match match;
		private List<SetScore> setScores;
		
		public PlayMatch (IPlaySet playSet, ISide sideOne, ISide sideTwo)
		{
			if (playSet == null)
			{
				throw new ArgumentNullException("playSet");
			}
			
			if (sideOne == null)
			{
				throw new ArgumentNullException("sideOne");
			}
			
			if (sideTwo == null)
			{
				throw new ArgumentNullException("sideTwo");
			}
			
			this.sideOne = sideOne;
			this.sideTwo = sideTwo;
			this.playSet = playSet;
		}
		
		public PlayMatch(ISide sideOne, ISide sideTwo) : this(new PlaySet(sideOne, sideTwo), sideOne, sideTwo)
		{
		}
		
		public ISide Play()
		{
			match = new Match();
			setScores = new List<SetScore>();
			setScores.Add(new SetScore() { Score = match.PrintScore() });
			
			while(match.State != MatchState.MatchWonBySideOne && match.State != MatchState.MatchWonBySideTwo)
			{
				var setWinner = playSet.Play();
				
				match.WinSet(s => setWinner);
				
				setScores.Add(new SetScore() { Score = match.PrintScore(), GameScores = playSet.GetGameScores() });
			}
			
			if (match.State == MatchState.MatchWonBySideOne)
			{
				return sideOne;
			}
			else
			{
				return sideTwo;
			}
		}
		
		public List<SetScore> GetSetScores()
		{
			return setScores;
		}
		
		public ISide SideOne
		{
			get { return this.sideOne; }
		}
		
		public ISide SideTwo
		{
			get { return this.sideTwo; }
		}
	}
}

