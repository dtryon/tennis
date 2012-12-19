namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic
    open FSharpx

    type PlayGame(determinWinner: IDetermineWinner) =
        
        let mutable detWinner = determinWinner
        let mutable Winner = Side.None
        let game = new Game()
        let mutable pointsList = []

        interface IPlayGame with
            member this.Play() =
                let mutable continueLooping = true
                pointsList <- game.PrintScore() :: pointsList
                while continueLooping do     
                    let pointWinner = game.WinPoint(fun x -> detWinner.ForPoint())
                    pointsList <- game.PrintScore() :: pointsList
                         
                    if game.State = GameState.GameWonBySideOne then
                        continueLooping <- false
                        Winner <- Side.One
                    elif game.State = GameState.GameWonBySideTwo then
                        continueLooping <- false
                        Winner <- Side.Two
                Winner

            member this.GetPointScores() =
                List.rev pointsList |> List.toSeq
            




