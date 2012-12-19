namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type GameScore() =
        let mutable score = ""
        let mutable pointScores : seq<string> = Seq.empty

        member this.Score
            with get () = score
            and set (value) = score <- value

        member this.PointScores
            with get () = pointScores
            and set (value) = pointScores <- value

