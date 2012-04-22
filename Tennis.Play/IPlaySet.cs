using System;
using Tennis.Logic;
using System.Collections.Generic;

namespace Tennis.Play
{
	public interface IPlaySet
	{
		Side Play();
		
		List<GameScore> GetGameScores();
	}
}

