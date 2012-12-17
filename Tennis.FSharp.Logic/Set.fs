namespace Tennis.FSharp.Logic

    open System
    open FSharpx

    type Set() =
        let mutable setState = SetState.Playing
        let mutable sideOneScore = 0
        let mutable sideTwoScore = 0

        member this.SideOneScore
            with get () = sideOneScore

        member this.SideTwoScore
            with get () = sideTwoScore

        member this.State
            with get () = setState

        member private this.IsWinByTwo =
            if sideOneScore >= 5 && sideTwoScore >= 5 then
                true
            else
                false

        member private this.DetermineWinner =
            if this.IsWinByTwo then
                if sideOneScore > sideTwoScore && sideOneScore - sideTwoScore >= 2 then
                    setState <- SetState.SetWonBySideOne
                elif sideTwoScore > sideOneScore && sideTwoScore - sideOneScore >= 2 then
                    setState <- SetState.SetWonBySideTwo
            else
                if sideOneScore >= 6 then
                    setState <- SetState.SetWonBySideOne
                elif sideTwoScore >= 6 then
                    setState <- SetState.SetWonBySideTwo

        member this.WinGame(f: Func<_,_>) =
            let func = FSharpFunc.FromFunc f
            let winner = func Side.None
            
            setState <- SetState.Playing
            
            if winner = Side.One then
                sideOneScore <- sideOneScore + 1
            else
                sideTwoScore <- sideTwoScore + 1

            this.DetermineWinner

        member this.PrintScore() =
            sideOneScore.ToString() + " - " + sideTwoScore.ToString()