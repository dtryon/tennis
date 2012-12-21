namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic
    open FSharpx

    type PlayTournament(matches: seq<IPlayMatch>) =
        let mutable matches = matches
        let mutable matchList = []              

        member private this.PlayTheMatch (aMatch: IPlayMatch) =
            let winner = aMatch.Play()
            let matchScore = new MatchScore()
            matchScore.SetScores <- aMatch.GetSetScores()
            matchList <- matchScore :: matchList
            winner
        

        member private this.PlayRound matches =
            [
                for aMatch in matches do
                    yield this.PlayTheMatch aMatch
            ]
                

        member private this.MatchUp (sides: seq<ISide>) =
            let len = Seq.length sides
            let med = len / 2
            let firstHalf = Seq.take med sides
            let lastHalf = Seq.skip med sides

            Seq.zip firstHalf lastHalf
                |> Seq.map (fun (first, second) -> new PlayMatch(new PlaySet(new PlayGame(new DetermineWinner(first, second))), first, second))
                :?> seq<IPlayMatch>

        interface IPlayTournament with
            member this.Play() = 
                let mutable currentMatch = Seq.head matches;
                let mutable tournamentWinner = currentMatch.SideOne
                let mutable continueLooping = true
                let mutable roundMatches = matches

                while continueLooping do                  
                    if Seq.length roundMatches = 1 then
                        tournamentWinner <- this.PlayTheMatch (Seq.head roundMatches)
                        continueLooping <- false
                    else
                        let sides = this.PlayRound roundMatches
                        let newMatches = this.MatchUp sides
                        roundMatches <- newMatches
                tournamentWinner

            member this.GetMatchScores() =
                List.rev matchList |> List.toSeq
