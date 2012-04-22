using System;
using System.Collections.Generic;

namespace Tennis.Play
{
	public interface IPlayTournament
	{
		ISide Play();	
		List<MatchScore> GetMatchScores();
	}
}

