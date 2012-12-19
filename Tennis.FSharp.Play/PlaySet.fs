namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic
    open FSharpx

    type PlaySet(playGame: IPlayGame) =
        
        let mutable playGame = playGame
        let mutable Winner = Side.None
        let set = new Set()
        let mutable gamesList = []

        interface IPlaySet with
            member this.Play() =
                let mutable continueLooping = true
                let gameScore = new GameScore()
                gameScore.Score <- set.PrintScore()
                gamesList <- gameScore :: gamesList
                while continueLooping do    
                    let gameWinner = playGame.Play() 
                    let pointWinner = set.WinGame(fun x -> gameWinner)
                    let activeGameScore = new GameScore()
                    activeGameScore.Score <- set.PrintScore()
                    activeGameScore.PointScores <- playGame.GetPointScores()
                    gamesList <- activeGameScore :: gamesList
                         
                    if set.State = SetState.SetWonBySideOne then
                        continueLooping <- false
                        Winner <- Side.One
                    elif set.State = SetState.SetWonBySideTwo then
                        continueLooping <- false
                        Winner <- Side.Two
                Winner

            member this.GetGameScores() =
                List.rev gamesList |> List.toSeq