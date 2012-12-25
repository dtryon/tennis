using System;
using System.Collections.Generic;
using System.Linq;
using Tennis.FSharp.Play;

namespace Tennis.FSharp.ConsoleUI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                playAgain = false;
                Console.WriteLine("Shall we play some Tennis?");
                Console.Write("How many sides: ");
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

                var matches = new List<IPlayMatch>();
                var sides = new List<ISide>();
                var random = new Random();

                for (int i = 0; i < number; i++)
                {
                    Console.Write("Team Name: ");
                    string name = Console.ReadLine();

                    ISide newSide = new PlayingSide();
                    newSide.TeamName = name;
                    newSide.Strength = random.Next(100);
                    newSide.ServingStrength = random.Next(50);
                    newSide.ReturningStrength = random.Next(25);

                    sides.Add(newSide);
                    Console.Write(" added with strength: {0}, serving: {1}, returning: {2}\n", newSide.Strength,
                                  newSide.ServingStrength, newSide.ReturningStrength);

                    if (i == number - 1)
                    {
                        if (number%2 == 1)
                        {
                            // let the last odd team play themselves
                            ISide anotherSide = new PlayingSide();
                            anotherSide.TeamName = name;
                            anotherSide.Strength = random.Next(100);
                            anotherSide.ServingStrength = random.Next(50);
                            anotherSide.ReturningStrength = random.Next(25);
                        }
                    }
                }

                Console.Write("To Start Tournament Press <Enter>");
                Console.ReadLine();

                for (int i = 0; i < sides.Count; i = i + 2)
                {
                    matches.Add(new PlayMatch(new PlaySet(new PlayGame(new DetermineWinner(sides[i], sides[i + 1]))), sides[i], sides[i + 1]));
                }

                IPlayTournament playTournament = new PlayTournament(matches);

                ISide champion = playTournament.Play();
                IEnumerable<MatchScore> matchScores = playTournament.GetMatchScores();

                PrintMatchScores(matchScores);

                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("And the champion is....{0}, strength: {1}, serving: {2}, returning: {3}",
                                  champion.TeamName, champion.Strength, champion.ServingStrength,
                                  champion.ReturningStrength);

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

        private static void PrintMatchScores(IEnumerable<MatchScore> matchScores)
        {
            foreach (var matchScore in matchScores)
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
                Console.WriteLine("Match between {0} ({1}:S{2}:R{3}) and {4} ({5}:S{6}:R{7})",
                                  matchScore.SideOne.TeamName, matchScore.SideOne.Strength,
                                  matchScore.SideOne.ServingStrength, matchScore.SideOne.ReturningStrength,
                                  matchScore.SideTwo.TeamName, matchScore.SideTwo.Strength,
                                  matchScore.SideTwo.ServingStrength, matchScore.SideTwo.ReturningStrength);
                Console.WriteLine("------------------------------------------------------------");

                var setScores = new List<SetScore>(matchScore.SetScores);
                for (int i = 0; i < setScores.Count; i++)
                {
                    if (setScores[i].GameScores != null)
                    {
                        Console.WriteLine("------------------------------------------------------------");

                        var gameScores = new List<GameScore>(setScores[i].GameScores);
                        for (int j = 0; j < gameScores.Count; j++)
                        {
                            if (gameScores[j].PointScores != null)
                            {
                                Console.WriteLine("------------------------------------------------------------");

                                foreach (var point in gameScores[j].PointScores)
                                {
                                    Console.WriteLine(point);
                                }

                                Console.WriteLine("------------------------------------------------------------");
                                Console.WriteLine("Game #{0}: {1}", j, gameScores[j].Score);
                            }
                        }

                        Console.WriteLine("------------------------------------------------------------");
                        Console.WriteLine("Set #{0}: {1}", i, setScores[i].Score);
                    }
                }

                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Match between {0} ({1}:S{2}:R{3}) and {4} ({5}:S{6}:R{7})",
                                  matchScore.SideOne.TeamName, matchScore.SideOne.Strength,
                                  matchScore.SideOne.ServingStrength, matchScore.SideOne.ReturningStrength,
                                  matchScore.SideTwo.TeamName, matchScore.SideTwo.Strength,
                                  matchScore.SideTwo.ServingStrength, matchScore.SideTwo.ReturningStrength);
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