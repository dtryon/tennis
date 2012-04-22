using System;
using Tennis.Logic;
using System.Collections.Generic;

namespace Tennis.Play
{
	public interface IPlayGame
	{
		Side Play();
		
		List<string> GetPointScores();
	}
}

