namespace Tennis.FSharp.Logic

    open System
    open FSharpx

    type Game() =
        let mutable gameState = GameState.PriorToDeuce
        let mutable sideOnePointState = PointState.Love
        let mutable sideTwoPointState = PointState.Love

        let advancePointState sideWinnerPointState =
            match sideWinnerPointState with
            | PointState.Love -> PointState.Fifteen
            | PointState.Fifteen -> PointState.Thirty
            | PointState.Thirty -> PointState.Forty
            | PointState.Forty -> PointState.Deuce
            | PointState.Deuce -> PointState.Advantage
            | PointState.Advantage -> PointState.Win
            | _ -> PointState.Love

        let printScore pointState =
            match pointState with
            | PointState.Love -> "love"
            | PointState.Fifteen -> "fifteen"
            | PointState.Thirty -> "thirty"
            | PointState.Forty -> "forty"
            | PointState.Deuce -> "deuce"
            | PointState.Advantage -> "advantage"
            | _ -> "love"

        member private this.IsDeuce =
            if (int sideOnePointState) > 2 && (int sideTwoPointState) > 2 && sideOnePointState = sideTwoPointState then
                sideOnePointState <- PointState.Deuce
                sideTwoPointState <- PointState.Deuce
                gameState <- GameState.Deuce
                true
            else
                false

        member private this.IsAdvantageSideOne =
            if (int sideOnePointState) > 3 && (int sideTwoPointState) > 3 && sideOnePointState > sideTwoPointState && (int sideOnePointState) - (int sideTwoPointState) < 2 then
                sideOnePointState <- PointState.Advantage
                sideTwoPointState <- PointState.Deuce
                gameState <- GameState.Deuce
                true
            else
                false

        member private this.IsAdvantageSideTwo =
            if (int sideOnePointState) > 3 && (int sideTwoPointState) > 3 && sideTwoPointState > sideOnePointState && (int sideTwoPointState) - (int sideOnePointState) < 2 then
                sideOnePointState <- PointState.Deuce
                sideTwoPointState <- PointState.Advantage
                gameState <- GameState.Deuce
                true
            else
                false

        member private this.HasBeaten sideState sideToCompareState =
            if (int sideState) > 3 && sideState > sideToCompareState && (((int sideState) - (int sideToCompareState)) >= 2) then
                true
            else
                false
        
        member private this.DetermineWinner =
            if this.IsDeuce || this.IsAdvantageSideOne || this.IsAdvantageSideTwo then
                Side.None
            elif this.HasBeaten sideOnePointState sideTwoPointState then    
                gameState <- GameState.GameWonBySideOne
                Side.One
            elif this.HasBeaten sideTwoPointState sideOnePointState then
                gameState <- GameState.GameWonBySideTwo
                Side.Two
            else
                Side.None

        member this.SideOneScore
            with get () = sideOnePointState
            and set (value) = sideOnePointState <- value

        member this.SideTwoScore
            with get () = sideTwoPointState
            and set (value) = sideTwoPointState <- value

        member this.State
            with get () = gameState

        member this.PrintScore() =
            let winningSide = this.DetermineWinner

            if this.IsDeuce then
                "deuce"
            elif this.IsAdvantageSideOne then
                "advantage - side one"
            elif this.IsAdvantageSideTwo then
                "advantage - side two"
            elif winningSide = Side.None then
                let sideOneScore = printScore sideOnePointState
                let sideTwoScore = printScore sideTwoPointState
                sideOneScore + " - " + sideTwoScore
            else
                if winningSide = Side.One then
                    "game - side one"
                else
                    "game - side two"

        member this.WinPoint(f: Func<_,_>) =
            let func = FSharpFunc.FromFunc f
            let winner = func Side.None
            if winner = Side.One then
                sideOnePointState <- advancePointState sideOnePointState
                this.DetermineWinner
            else
                sideTwoPointState <- advancePointState sideTwoPointState
                this.DetermineWinner


