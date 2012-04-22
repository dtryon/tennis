using System;
using Tennis.Play;
using System.Collections.Generic;

namespace Tennis.ConsoleUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			bool playAgain = true;
			
			while(playAgain)
			{
				playAgain = false;
				Console.WriteLine ("Shall we play some Tennis?");
				Console.Write ("How many sides: ");
				string numberOfPlayers = Console.ReadLine();
				
				int number = 0;
				if (!Int32.TryParse(numberOfPlayers, out number))
				{
					Console.WriteLine("Sorry, the number of sides could not be understood. Please try again");
					return;
				}
				
				if (number <= 0)
				{
					Console.WriteLine("Sorry, the number of sides must be positive. Please try again");
					return;
				}
				
				List<IPlayMatch> matches = new List<IPlayMatch>();
				List<ISide> sides = new List<ISide>();
				Random random = new Random();
				
				for (int i = 0; i < number; i++) 
				{
					Console.Write("Team Name: ");
					string name = Console.ReadLine();
					
					var newSide = new PlayingSide() { TeamName = name, Strength = random.Next(100), ServingStrength = random.Next(50), ReturningStrength = random.Next(25) };
					sides.Add(newSide);
					Console.Write(" added with strength: {0}, serving: {1}, returning: {2}\n", newSide.Strength, newSide.ServingStrength, newSide.ReturningStrength);
					
					if (i == number - 1)
					{
						if (number % 2 == 1)
						{
							// let the last odd team play themselves
							sides.Add(new PlayingSide() { TeamName = name, Strength = random.Next(100), ServingStrength = random.Next(50), ReturningStrength = random.Next(25) });
						}
					}
				}
				
				Console.Write("To Start Tournament Press <Enter>");
				Console.ReadLine();
				
				for (int i = 0; i < sides.Count; i = i + 2) 
				{
					matches.Add(new PlayMatch(sides[i], sides[i+1]));
				}
				
				IPlayTournament playTournament = new PlayTournament(matches);
				
				var champion = playTournament.Play();
				var matchScores = playTournament.GetMatchScores();
				
				PrintMatchScores(matchScores);
				
				Console.WriteLine("----------------------------------------------------------------");
				Console.WriteLine("And the champion is....{0}, strength: {1}, serving: {2}, returning: {3}", champion.TeamName, champion.Strength, champion.ServingStrength, champion.ReturningStrength);
				
				Console.WriteLine("----------------------------------------------------------------");
				Console.WriteLine("----------------------------------------------------------------");
				Console.Write("Play Again? (Y or N): ");
				string again = Console.ReadLine();
				
				if (again == "Y" || again == "y")
				{
					playAgain = true;
				}
			}
			
		}
		
		private static void PrintMatchScores(List<MatchScore> matchScores)
		{
			foreach(var matchScore in matchScores)
			{
				if (matchScore.SideOne.TeamName == matchScore.SideTwo.TeamName)
				{
					Console.WriteLine("------------------------------------------------------------");
					Console.WriteLine("{0} Received Bye to Next Round", matchScore.SideOne.TeamName);
					Console.WriteLine("------------------------------------------------------------");
					ContinueToNextMatch();
					continue;
				}
				Console.WriteLine("------------------------------------------------------------");
				Console.WriteLine("------------------------------------------------------------");
				Console.WriteLine("Match between {0} ({1}:S{2}:R{3}) and {4} ({5}:S{6}:R{7})", matchScore.SideOne.TeamName,  matchScore.SideOne.Strength, matchScore.SideOne.ServingStrength, matchScore.SideOne.ReturningStrength, matchScore.SideTwo.TeamName, matchScore.SideTwo.Strength, matchScore.SideTwo.ServingStrength, matchScore.SideTwo.ReturningStrength);
				Console.WriteLine("------------------------------------------------------------");
				
				for (int i = 0; i < matchScore.SetScores.Count; i++) {
					
					if (matchScore.SetScores[i].GameScores != null)
					{
						Console.WriteLine("------------------------------------------------------------");
						
						for (int j = 0; j < matchScore.SetScores[i].GameScores.Count; j++) {
							
							if (matchScore.SetScores[i].GameScores[j].PointScores != null)
							{
								Console.WriteLine("------------------------------------------------------------");
								
								foreach(var point in matchScore.SetScores[i].GameScores[j].PointScores)
								{
									Console.WriteLine(point);
								}		
								
								Console.WriteLine("------------------------------------------------------------");
								Console.WriteLine("Game #{0}: {1}", j, matchScore.SetScores[i].GameScores[j].Score);
							}
						}
						
						Console.WriteLine("------------------------------------------------------------");
						Console.WriteLine("Set #{0}: {1}", i, matchScore.SetScores[i].Score);
					}
				}
				
				Console.WriteLine("------------------------------------------------------------");
				Console.WriteLine("Match between {0} ({1}:S{2}:R{3}) and {4} ({5}:S{6}:R{7})", matchScore.SideOne.TeamName,  matchScore.SideOne.Strength, matchScore.SideOne.ServingStrength, matchScore.SideOne.ReturningStrength, matchScore.SideTwo.TeamName, matchScore.SideTwo.Strength, matchScore.SideTwo.ServingStrength, matchScore.SideTwo.ReturningStrength);
				Console.WriteLine("------------------------------------------------------------");

				ContinueToNextMatch();
			}
		}
		
		private static void ContinueToNextMatch()
		{
			Console.WriteLine("Press <Enter> to Continue");
			Console.ReadLine();
		}
	}
}