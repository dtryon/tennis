using System;

namespace Tennis.Logic
{
	public class Set
	{
		int sideOneScore;
		int sideTwoScore;
		SetState state;
		
		public Set()
		{
			state = SetState.Playing;
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
		
		public SetState State 
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
			
			var diff = sideOneScore - sideTwoScore;
			if (Math.Abs(diff) >= 2)
			{
				if (sideOneScore >= 6 || sideTwoScore >= 6)
				{
					if (diff > 0)
					{
						state = SetState.SetWonBySideOne;
					}
					else
					{
						state = SetState.SetWonBySideTwo;
					}
				}
			}
		}
	}
}

