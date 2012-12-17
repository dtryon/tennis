namespace Tennis.FSharp.Logic

    open System
    open FSharpx

    type Match() =
        let mutable matchState = MatchState.Playing
        let mutable sideOneScore = 0
        let mutable sideTwoScore = 0

        member this.SideOneScore
            with get () = sideOneScore

        member this.SideTwoScore
            with get () = sideTwoScore

        member this.State
            with get () = matchState

        member private this.IsWinByTwo =
            if sideOneScore >= 5 && sideTwoScore >= 5 then
                true
            else
                false

        member private this.DetermineWinner =
            if this.IsWinByTwo then
                if sideOneScore > sideTwoScore && sideOneScore - sideTwoScore >= 2 then
                    matchState <- MatchState.MatchWonBySideOne
                elif sideTwoScore > sideOneScore && sideTwoScore - sideOneScore >= 2 then
                    matchState <- MatchState.MatchWonBySideTwo
            else
                if sideOneScore >= 2 then
                    matchState <- MatchState.MatchWonBySideOne
                elif sideTwoScore >= 2 then
                    matchState <- MatchState.MatchWonBySideTwo

        member this.WinSet(f: Func<_,_>) =
            let func = FSharpFunc.FromFunc f
            let winner = func Side.None
            
            matchState <- MatchState.Playing
            
            if winner = Side.One then
                sideOneScore <- sideOneScore + 1
            else
                sideTwoScore <- sideTwoScore + 1

            this.DetermineWinner

        member this.PrintScore() =
            sideOneScore.ToString() + " - " + sideTwoScore.ToString()
