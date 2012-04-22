using System;

namespace Tennis.Play
{
	public interface ISide
	{
		string TeamName { get; set; }
		
		int Strength { get; set; }
		
		bool IsServing { get; set; }
		
		int ServingStrength { get; set; }
		
		int ReturningStrength { get; set; }
	}
}

