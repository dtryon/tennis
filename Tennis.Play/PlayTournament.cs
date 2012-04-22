using System;
using Tennis.Logic;
using System.Collections.Generic;

namespace Tennis.Play
{
	public class PlayTournament : IPlayTournament
	{
		private readonly List<IPlayMatch> playMatches;
		private List<MatchScore> matchScores;
		
		public PlayTournament (List<IPlayMatch> playMatches)
		{
			if (playMatches == null)
			{
				throw new ArgumentNullException("sides");
			}
			
			if (playMatches.Count == 0)
			{
				throw new ArgumentException("sides");
			}
			
			this.playMatches = playMatches;
		}
		
		public ISide Play()
		{
			ISide champion = null;
			matchScores = new List<MatchScore>();
			
			PlayMatches(playMatches, ref champion);
			
			return champion;
		}
		
		public List<MatchScore> GetMatchScores()
		{
			return this.matchScores;
		}
		
		private void PlayMatches(List<IPlayMatch> matches, ref ISide champion)
		{
			var nextRound = new List<IPlayMatch>();
			var winningSides = new List<ISide>();
			
			if (matches.Count == 1)
			{
				champion = matches[0].Play();
				AddMatchScore(matches[0]);
				return;
			}
			
			foreach(var match in matches)
			{
				var winner = match.Play();
				AddMatchScore(match);
				winningSides.Add(winner);
			}
			
			for (int i = 0; i < winningSides.Count; i = i + 2) 
			{
				if (i == winningSides.Count - 1 && winningSides.Count % 2 == 1)
				{
					nextRound.Add(new PlayMatch(winningSides[i], winningSides[i]));
				}
				else
				{
					nextRound.Add(new PlayMatch(winningSides[i], winningSides[i+1]));
				}
			}
			
			PlayMatches(nextRound, ref champion);	
		}
		
		private void AddMatchScore(IPlayMatch playMatch)
		{
			matchScores.Add(new MatchScore() 
				{ SetScores = playMatch.GetSetScores(), 
					SideOne = playMatch.SideOne, 
					SideTwo = playMatch.SideTwo });
		}
	}
}

