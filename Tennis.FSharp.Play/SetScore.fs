namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type SetScore() =
        let mutable score = ""
        let mutable gameScores : seq<GameScore> = Seq.empty

        member this.Score
            with get () = score
            and set (value) = score <- value

        member this.GameScores
            with get () = gameScores
            and set (value) = gameScores <- value
