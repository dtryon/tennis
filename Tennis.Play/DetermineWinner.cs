using System;
using Tennis.Logic;

namespace Tennis.Play
{
	public class DetermineWinner : IDetermineWinner
	{
		private readonly Random random;
		private readonly ISide sideOne;
		private readonly ISide sideTwo;
		
		public DetermineWinner (ISide sideOne, ISide sideTwo)
		{
			random = new Random();
			
			this.sideOne = sideOne;
			this.sideTwo = sideTwo;
		}
		
		public Side ForPoint()
		{
			int randomNumber = random.Next(10000);
			int randomSplit = random.Next(85);
			int sideOneTotalStrength = sideOne.Strength;
			int sideTwoTotalStrength = sideTwo.Strength;
			
			// Add serving and returning bonus
			if (sideOne.IsServing)
			{
				sideOneTotalStrength = sideOneTotalStrength + sideOne.ServingStrength;
				sideTwoTotalStrength = sideTwoTotalStrength + sideTwo.ReturningStrength;
			} 
			else if (sideTwo.IsServing)
			{
				sideTwoTotalStrength = sideTwoTotalStrength + sideTwo.ServingStrength;
				sideOneTotalStrength = sideOneTotalStrength + sideOne.ReturningStrength;
			}
			
			if (randomNumber % 2 == 1)
			{
				if (sideOneTotalStrength + randomSplit + sideOne.ServingStrength > sideTwoTotalStrength)
				{
					return Side.One;
				}
				return Side.Two;
			}
			else
			{
				if (sideTwoTotalStrength + randomSplit + sideTwo.ServingStrength > sideOneTotalStrength)
				{
					return Side.Two;
				}
				return Side.One;
			}
		}
	}
}

