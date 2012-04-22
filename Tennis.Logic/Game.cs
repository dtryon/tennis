using System;

namespace Tennis.Logic
{
	public class Game
	{		
		private PointState sideOnePoints;
		private PointState sideTwoPoints;
		private GameState state;
		
		public Game()
		{
			this.sideOnePoints = PointState.Love;
			this.sideTwoPoints = PointState.Love;
			
			this.state = GameState.PriorToDeuce;
			
		}
		
		public void WinPoint(Func<Side, Side> scoring)
		{
			var scoringSide = scoring(Side.None);
			if (scoringSide == Side.One)
			{
				AdvanceState(scoringSide);
			}
			else if (scoringSide == Side.Two)
			{
				AdvanceState(scoringSide);
			}
		}
		
		public PointState SideOneScore
		{
			get { return sideOnePoints; }
		}
		
		public PointState SideTwoScore
		{
			get { return sideTwoPoints; }
		}
		
		public GameState State
		{
			get { return state; }
		}
		
		private void AdvanceState(Side side)
		{
			if (StillPlaying())
			{	
				if (HasEnteredDeuce())
				{
					this.state = GameState.Deuce;
					
					if (GetPointState(side) == PointState.Advantage)
					{
						this.state = DetermineWinner(side);
						return;
					}
					
					if((sideOnePoints == PointState.Deuce && sideTwoPoints == PointState.Deuce)
						|| (sideOnePoints == PointState.Forty && sideTwoPoints == PointState.Forty))
					{
						SetPointState(side, PointState.Advantage);
						return;
					}
					
					sideOnePoints = PointState.Deuce;
					sideTwoPoints = PointState.Deuce;
					return;
				}
				
				RollPoints(side);
				
				if (((int)GetPointState(side)) > 4)
				{
					this.state = DetermineWinner(side);
				}
			}
		}
		
		private bool StillPlaying()
		{
			return ((this.State == GameState.Deuce) || (this.State == GameState.PriorToDeuce));
		}
		
		private GameState DetermineWinner(Side side)
		{
			if (side == Side.One)
			{
				return GameState.GameWonBySideOne;
			}
			else
			{
				return GameState.GameWonBySideTwo;
			}
		}
		
		private void RollPoints(Side side)
		{
			var pointState = GetPointState(side);
			switch(pointState)
			{
				case PointState.None:
					SetPointState(side, PointState.Love);
					break;
				case PointState.Love:
					SetPointState(side, PointState.Fifteen);
					break;
				case PointState.Fifteen:
					SetPointState(side, PointState.Thirty);
					break;
				case PointState.Thirty:
					SetPointState(side, PointState.Forty);
					break;
				case PointState.Forty:
					SetPointState(side, PointState.Deuce);
					break;
			}
		}

		private bool HasEnteredDeuce ()
		{
			if (((int)sideOnePoints) >= 3 && ((int)sideTwoPoints) >= 3)
			{
				return true;
			}			
			return false;
		}
		
		public string PrintScore()
		{
			if (this.State != GameState.PriorToDeuce)
			{
				return TranslateDeuceOrBetterScore();
			}
			
			string sideOne = TranslateScore(this.sideOnePoints);
			string sideTwo = TranslateScore(this.sideTwoPoints);
			return sideOne + " - " + sideTwo;
		}
		
		private string TranslateDeuceOrBetterScore()
		{
			if (this.State == GameState.GameWonBySideOne)
			{
				return "game - side one";
			}
			else if(this.State == GameState.GameWonBySideTwo)
			{
				return "game - side two";
			}
			
			if (sideOnePoints == PointState.Advantage)
			{
				return "advantage - side one"; 
			}
			else if (sideTwoPoints == PointState.Advantage)
			{
				return "advantage - side two";
			}
			
			return "deuce";
		}
		
		private string TranslateScore(PointState pointState)
		{
			switch(pointState)
			{
			case PointState.Love:
				return "love";
			case PointState.Fifteen:
				return "fifteen";
			case PointState.Thirty:
				return "thirty";
			case PointState.Forty:
				return "forty";
			default:
			    return String.Empty;
			}
		}
		
		private PointState GetPointState(Side side)
		{
			if (side == Side.One)
			{
				return sideOnePoints;
			}
			else
			{
				return sideTwoPoints;
			}
		}
		
		private void SetPointState(Side side, PointState state)
		{
			if (side == Side.One)
			{
				sideOnePoints = state;
			}
			else
			{
				sideTwoPoints = state;
			}
		}
	}
}

