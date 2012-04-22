using System;
using System.Collections.Generic;

namespace Tennis.Play
{
	public interface IPlayMatch
	{
		ISide Play();
		
		List<SetScore> GetSetScores();
		
		ISide SideOne { get; }
		ISide SideTwo { get; }
	}
}

