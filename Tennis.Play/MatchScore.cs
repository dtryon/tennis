using System;
using System.Collections.Generic;

namespace Tennis.Play
{
	public class MatchScore
	{
		public List<SetScore> SetScores { get; set; }
		
		public ISide SideOne { get; set; }
		
		public ISide SideTwo { get; set; }
	}
}