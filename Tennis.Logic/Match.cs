using System;

namespace Tennis.Logic
{
	public class Match
	{
		int sideOneScore;
		int sideTwoScore;
		MatchState state;
		
		public Match()
		{
			state = MatchState.Playing;
		}
		
		public void WinGame(Func<Side, Side> scoring)
		{
			var scoringSide = scoring(Side.None);
			AdvanceState(scoringSide);
		}
		
		public int SideOneScore
		{
			get { return sideOneScore; }
		}
		
		public int SideTwoScore
		{
			get { return sideTwoScore; }
		}
		
		public MatchState State 
		{ 
			get { return state; }
		}
		
		public string PrintScore()
		{
			return sideOneScore.ToString() + " - " + sideTwoScore.ToString();
		}
		
		private void AdvanceState(Side side)
		{
			if (side == Side.One)
			{
				sideOneScore++;
			}
			
			if (side == Side.Two)
			{
				sideTwoScore++;
			}
			
			if (sideOneScore >= 3)
			{
				state = MatchState.MatchWonBySideOne;
			}
			
			if (sideTwoScore >= 3)
			{
				state = MatchState.MatchWonBySideTwo;
			}
		}
	}
}

