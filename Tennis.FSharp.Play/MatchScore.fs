namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type MatchScore() =
        let mutable setScores : seq<SetScore> = Seq.empty

        member this.SetScores
            with get () = setScores
            and set (value) = setScores <- value
