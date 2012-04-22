using System;

namespace Tennis.Play
{
	public class PlayingSide : ISide
	{
		public string TeamName { get; set; }
		
		public int Strength { get; set; }
		
		public bool IsServing { get; set; }
		
		public int ServingStrength { get; set; }
		
		public int ReturningStrength { get; set; }
	}
}

