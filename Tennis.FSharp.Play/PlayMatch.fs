namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic
    open FSharpx

    type PlayMatch(playSet: IPlaySet, sideOne: ISide, sideTwo: ISide) =
        let mutable playSet = playSet
        let mutable sideOne = sideOne
        let mutable sideTwo = sideTwo
        let mutable Winner = sideOne
        let tennisMatch = new Match()
        let mutable setsList = []              

        interface IPlayMatch with
            member this.Play() =
                let mutable continueLooping = true
                let setScore = new SetScore()
                setScore.Score <- tennisMatch.PrintScore()
                setsList <- setScore :: setsList
                while continueLooping do    
                    let setWinner = playSet.Play() 
                    let gameWinner = tennisMatch.WinSet(fun x -> setWinner)
                    let activeSetScore = new SetScore()
                    activeSetScore.Score <- tennisMatch.PrintScore()
                    activeSetScore.GameScores <- playSet.GetGameScores()
                    setsList <- activeSetScore :: setsList
                         
                    if tennisMatch.State = MatchState.MatchWonBySideOne then
                        continueLooping <- false
                        Winner <- sideOne
                    elif tennisMatch.State = MatchState.MatchWonBySideTwo then
                        continueLooping <- false
                        Winner <- sideTwo
                Winner

            member this.GetSetScores() =
                List.rev setsList |> List.toSeq

            member this.SideOne
                with get () = sideOne

            member this.SideTwo
                with get () = sideTwo